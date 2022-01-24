using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleballController : MonoBehaviour {
  GameObject player;

  static GameObject targetTele;


  public float timeToLive = 2.5f;

  private float timePassed = 0.0f;
  void Start() {
    if (player == null) {
      player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
  }

  // Update is called once per frame
  void Update() {
    timePassed += Time.deltaTime;
    if (timePassed >= timeToLive) {
      Destroy(gameObject);
    }
  }

  private void teleport(Vector3 location) {

    location.y = 1;

    if (targetTele == player) {
      CharacterController cc = player.GetComponent<CharacterController>();

      cc.enabled = false;
      player.transform.position = location;
      cc.enabled = true;
      targetTele = null;
    } else {
      Target target = targetTele.GetComponent<Target>();
      if (target != null) {
        target.setMarker(false);
      }
      targetTele.transform.position = location;
    }
    // player.GetComponent<Rigidbody>().posi tion = location;
    targetTele = null;
  }

  private void OnCollisionEnter(Collision other) {
    Target target = other.gameObject.GetComponent<Target>();
    Vector3 point = other.GetContact(0).point;
    if (target != null) { // did it hit an entity?
      if (!target.teleportable) {
        Destroy(gameObject);
        return;
      }
      if (targetTele == null) { // select entity
        targetTele = other.gameObject;
        target.setMarker(true);
      } else if (targetTele == other.gameObject) { // reset selection
        targetTele = null;
        target.setMarker(false);
      } else {
        teleport(point);
      }
    } else { // hit wall 
      if (targetTele == null)
        targetTele = player;
      teleport(point);
    }
    if (other.rigidbody != null) {
      other.rigidbody.velocity = Vector3.zero;
      other.rigidbody.angularVelocity = Vector3.zero;
    }
    Destroy(gameObject);
  }
}
