using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour {

  public float maxHealth = 300f;
  public float health = 300f;
  public bool friendly = false;
  public UnityEvent<float> updateHealthBar,updateMaxHealth;

  private void Start() {
    if (updateMaxHealth != null) updateMaxHealth.Invoke(maxHealth);
    health = maxHealth;
    if (updateHealthBar != null) updateHealthBar.Invoke(health);

  }

  public float inflictDamage(float damage) {
    health -= damage;
    if(updateHealthBar != null) updateHealthBar.Invoke(health);
    return health;
  }

  private void Update() {
    if (health <= 0) {
      die();
    }
  }

  private void die(){
    Destroy(gameObject);
  }

}
