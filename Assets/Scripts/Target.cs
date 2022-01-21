using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour {
  public float health = 100f;
  public bool friendly = false;
  public UnityEvent<float> updateHealthBar;

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
