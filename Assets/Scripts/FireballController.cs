using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

  public float damage = 5.0f;

  public Vector3 velocity;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    // transform.position += velocity;
  }

  private void OnCollisionEnter(Collision other) {
    Target target = other.gameObject.GetComponent<Target>();
    if (target != null) {
      target.inflictDamage(damage);
    }
    other.rigidbody.velocity = Vector3.zero;
    other.rigidbody.angularVelocity = Vector3.zero;
    Destroy(gameObject);
  }
}
