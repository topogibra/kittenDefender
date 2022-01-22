
using System;
using UnityEngine;

public class Weapon : MonoBehaviour {
  public float damage = 10f;
  public float range = 100f;

  public float shootSpeed = 200f;

  public Vector3 _attackPosition;
  public GameObject projectile;

  internal void Shoot(Transform hit, Vector3 rayDirection) {
    if (projectile == null) {
      Target target = hit.transform.GetComponent("Target") as Target;
      if (target != null) {
        if (!target.friendly) target.inflictDamage(this.damage);
      }
    } else {
      Vector3 attackPosition = gameObject.transform.position + gameObject.transform.TransformVector(_attackPosition);

      // Instantiate bullet
      GameObject currentBullet = Instantiate(projectile, attackPosition, Quaternion.identity);
      FireballController fbC = currentBullet.GetComponent("FireballController") as FireballController;
      fbC.damage = damage;

      // Add direction and forces to bullet
      Rigidbody rigidBody = currentBullet.GetComponent<Rigidbody>();

      if (rigidBody != null) {
        rigidBody.AddForce(rayDirection.normalized * shootSpeed, ForceMode.Impulse);
      }

    }
  }
}
