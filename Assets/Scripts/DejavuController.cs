using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.UI;

public class DejavuController : MonoBehaviour {

  public DialogueContainer DialogueContainer;
  private DTree DialogueTree;

  private DNode currentDialogue;

  public GameObject _combatUI;
  public Text text;

  public Text options;

  public EnemySpawnerController spawnerController;

  public StarterAssetsInputs Inputs;
  // Start is called before the first frame update

  public Vector3 buffMultipliers = new Vector3(0.5f, 1, 2);

  public float chanceForEnemyGains = 0.9f;
  private int _buffSize = 0;
  private int _buffType = -1;
  private float _applyToEnemies = -1;

  private float deltaTime = 0.0f;

  private float readTime = 1.0f;

  private bool _finish = false;


  private void reset() {
    _buffSize = 0;
    _buffType = -1;
    _applyToEnemies = -1;
    deltaTime = 0;
    _finish = false;
    currentDialogue = DialogueTree.root;
    updateText(currentDialogue.DialogueText);
  }
  void Start() {
    DialogueTree = new DTree(DialogueContainer);
    currentDialogue = DialogueTree.root;
    updateText(currentDialogue.DialogueText);
    _combatUI.SetActive(false);
  }

  // Update is called once per frame
  void Update() {
    deltaTime += Time.deltaTime;
    if (deltaTime < readTime) {
      Inputs.Dialogue1Input(false);
      Inputs.Dialogue2Input(false);
      Inputs.Dialogue3Input(false);
      Inputs.InteractiveButtonInput(false);
      return;
    }

    int choice = -1;
    if (Inputs.dialogue1) {
      choice = 0;
    } else if (Inputs.dialogue2) {
      choice = 1;
    } else if (Inputs.dialogue3) {
      choice = 2;
    }


    if (_finish && Inputs.interactState) {
      _combatUI.SetActive(true);
      gameObject.SetActive(false);
      // buff enemies
      spawnerController.applyBuffs(_buffSize, _buffType, _applyToEnemies > 0);
      spawnerController.waves = 1;
      this.reset();
      Inputs.InteractiveButtonInput(false);
    } else if (choice >= 0) {
      deltaTime = 0;
      updateChoice(choice);
      Inputs.Dialogue1Input(false);
      Inputs.Dialogue2Input(false);
      Inputs.Dialogue3Input(false);
    }
  }

  private void updateChoice(int choice) {
    DNode nextNode = (currentDialogue.Next.Count >= 0) ? currentDialogue.Next[choice].Target : null;
    if (_buffSize == 0) { // choosing buff size
      _buffSize = choice;
      currentDialogue = nextNode;
      updateText(nextNode.DialogueText);
      return;
    } else if (_buffType == -1) { // choosing type of buff
      _buffType = choice;
      currentDialogue = nextNode;
      _finish = _buffSize < 2;
      updateText(nextNode.DialogueText + " " + String.Format("{0:0.0}", _buffSize) + "x");
      return;
    } else if (_applyToEnemies == -1 && _buffSize == 2) { // roll chanche to apply buff to enemies as well
      float rando = UnityEngine.Random.Range(0.0f, 1.0f);
      _applyToEnemies = (rando <= chanceForEnemyGains) ? 1 : 0;
      currentDialogue = currentDialogue.Next[0].Target;
      _finish = !(_applyToEnemies < 0);
      updateText(currentDialogue.DialogueText);
      return;
    }
    _finish = true;
  }

  private void updateText(string newText) {
    text.text = newText;
    options.text = "";
    if (!_finish)
      for (int i = 0; i < currentDialogue.Next.Count; i++) {
        options.text += (i + 1) + ". " + currentDialogue.Next[i].Name + " ";
      }
    else
      options.text = "Press E to continue";
  }
}
