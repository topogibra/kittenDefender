using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour {
  public GameObject dejavuCanvas;

  public DejavuController dejavuController;
  public GameObject combatUI;
  public StarterAssets.StarterAssetsInputs _inputs;
  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {

  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      _inputs.interactState = false;
    }
  }

  private void OnTriggerStay(Collider other) {
    if (other.tag == "Player") {
      if (_inputs.interactState && !dejavuCanvas.activeInHierarchy) {
        dejavuCanvas.SetActive(true);
        dejavuController.enabled = true;
        combatUI.SetActive(false);
        _inputs.interactState = false;
      }
    }
  }
}
