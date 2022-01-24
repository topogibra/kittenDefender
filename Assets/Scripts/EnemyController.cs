using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour {

  private Animator anim;
  private CharacterController controller;
  public float speed = 6.0f;
  public float turnSpeed = 60.0f;

  public float damage = 30.0f;

  public float secondsBtwAttacks = 3;

  private bool canAttack = false;

  public float gravity = 20.0f;
  private Vector3 moveDirection = Vector3.zero;

  public GameObject player;
  public GameObject kitty;

  private SphereCollider attackCollider;
  internal bool showCanvas;

  public GameObject canvas;
  public bool canMove { get; private set; }
  public float lastTimeSinceAttack { get; private set; }
  private float lastTimeSinceCanvas = 0.0f;
  public float timeForCanvas = 4f;



  // Use this for initialization
  void Start() {
    anim = GetComponent<Animator>();
    controller = GetComponent<CharacterController>();
    if (player == null) {
      player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    if (kitty == null) {
      kitty = GameObject.FindGameObjectsWithTag("Kitty")[0];
    }

    canMove = true;
    showCanvas = false;
    attackCollider = gameObject.GetComponent<SphereCollider>();
  }
  // Start is called before the first frame update

  // Update is called once per frame
  void Update() {
    GameObject target = closer();
    rotateCanvas();
    if (showCanvas) {
      lastTimeSinceCanvas += Time.deltaTime;
      if (lastTimeSinceCanvas >= timeForCanvas) {
        showCanvas = false;
        lastTimeSinceCanvas = 0;
      }
    }
    canvas.SetActive(showCanvas);
    move(target);
    attack(target);
  }

  void rotateCanvas() {
    canvas.transform.rotation = player.transform.rotation;
  }

  private GameObject closer() {
    bool isPlayerCloser = Vector3.Distance(player.transform.position, transform.position) < Vector3.Distance(kitty.transform.position, transform.position);
    return isPlayerCloser ? player : kitty;
  }

  private void move(GameObject targetObj) {
    Rigidbody rigidbody = gameObject.GetComponent("Rigidbody") as Rigidbody;
    Vector3 targetDir = targetObj.transform.position - transform.position;
    Quaternion rotation = Quaternion.LookRotation(targetDir);
    // transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    rigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, rotation, turnSpeed * Time.deltaTime));
    if (Vector3.Distance(targetObj.transform.position, transform.position) >= 3) {

      //   transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, speed * Time.deltaTime);
      rigidbody.MovePosition(Vector3.MoveTowards(rigidbody.position, targetObj.transform.position, speed * Time.deltaTime));
      if(!canAttack) anim.Play("RunForwardBattle");
    } else {
      canAttack = true;
    }
  }

  private void attack(GameObject target) {
    lastTimeSinceAttack += Time.deltaTime;
    if (canAttack) {
      if (lastTimeSinceAttack >= secondsBtwAttacks) {
        lastTimeSinceAttack = 0;
        target.GetComponent<Target>().inflictDamage(damage);
        anim.Play("Attack01");
      }
      canAttack = false;
    }
  }

  private void OnCollisionEnter(Collision other) {
    if (other.collider.GetType() == this.attackCollider.GetType()) {
      if (other.transform.GetComponent<FireballController>() != null)
        showCanvas = true;
    }
  }

  public void getHit(){
    anim.Play("GetHit");
  }
}
