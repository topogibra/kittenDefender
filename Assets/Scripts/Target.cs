using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Target : MonoBehaviour {

  public float maxHealth = 300f;
  public float health = 300f;
  public bool friendly = false;
  public UnityEvent<float> updateHealthBar, updateMaxHealth;
  public UnityEvent dieCallBack;

  public bool teleportable = true;

  public Image markerImage;

  public UnityEvent getHitAnimation, dieAnimation;


  private void Start() {
    if (updateMaxHealth != null) updateMaxHealth.Invoke(maxHealth);
    health = maxHealth;
    if (updateHealthBar != null) updateHealthBar.Invoke(health);
    setMarker(false);
  }

  public float inflictDamage(float damage) {
    health -= damage;
    if (updateHealthBar != null) {
      updateHealthBar.Invoke(health);
      dieAnimation.Invoke();
    } else {
      getHitAnimation.Invoke();
    }
    return health;
  }

  private void Update() {
    if (health <= 0) {
      die();
    }
  }

  public void setMarker(bool marked) {
    if (markerImage != null) markerImage.enabled = marked;
  }

  private void die() {
    dieCallBack.Invoke();
    Destroy(gameObject);
  }

}
