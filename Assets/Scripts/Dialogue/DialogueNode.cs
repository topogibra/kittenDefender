using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class DialogueNode : Node {
  public string GUID; // identifier
  public string dialogueText;
  public bool entryPoint = false;
}
