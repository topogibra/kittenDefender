using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour {
  public GameObject player;
  private StarterAssets.StarterAssetsInputs _inputs;
  // Start is called before the first frame update
  void Start() {
    if (player == null) {
      player = GameObject.FindGameObjectsWithTag("Player")[0];
    }


    _inputs = player.GetComponent("StarterAssetsInputs") as StarterAssets.StarterAssetsInputs;
  }

  // Update is called once per frame
  void Update() {

  }

  private void OnTriggerStay(Collider other) {
    if (other.tag == "Player") {
      if (other.gameObject == player) {
        if (_inputs.interactState) {
          Debug.Log("Interacted");
          _inputs.interactState = false;
        }
      }
    }
  }
}
