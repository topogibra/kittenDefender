using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

  public float damage = 5.0f;

  public Vector3 velocity;

  public float timeToLive = 2.5f;

  private float timePassed = 0.0f;
  void Update() {
    timePassed += Time.deltaTime;
    if(timePassed >= timeToLive){
      Destroy(gameObject);
    }
  }

  private void OnCollisionEnter(Collision other) {
    Target target = other.gameObject.GetComponent<Target>();
    if (target != null) {
      if(!target.friendly) target.inflictDamage(damage);
    }
    if (other.rigidbody != null) {
      other.rigidbody.velocity = Vector3.zero;
      other.rigidbody.angularVelocity = Vector3.zero;
    }
    Destroy(gameObject);
  }
}
