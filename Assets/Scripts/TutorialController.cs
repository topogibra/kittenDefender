using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
  public DialogueContainer DialogueContainer;
  private DTree DialogueTree;

  private DNode currentDialogue;

  public GameObject _combatUI;
  public Text text;

  public StarterAssetsInputs Inputs;
  // Start is called before the first frame update
  void Start() {
    DialogueTree = new DTree(DialogueContainer);
    currentDialogue = DialogueTree.root;
    text.text = currentDialogue.DialogueText;
    _combatUI.SetActive(false);
  }

  // Update is called once per frame
  void Update() {
    if(Inputs.interactState){
      if(currentDialogue.Next.Count == 0){
        _combatUI.SetActive(true);
        gameObject.SetActive(false);
      } else{
        currentDialogue = currentDialogue.Next[0].Target;
        text.text = currentDialogue.DialogueText;
      }
      Inputs.InteractiveButtonInput(false);
    }
  }

}

