using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

public class DialogueGraphView : GraphView {
  public readonly Vector2 nodeSize = new Vector2(150, 200);
  public DialogueGraphView() {
    styleSheets.Add(Resources.Load<StyleSheet>("GraphViewStyle"));
    SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

    this.AddManipulator(new ContentDragger());
    this.AddManipulator(new SelectionDragger());
    this.AddManipulator(new RectangleSelector());

    //Add backgorund grid
    var grid = new GridBackground();
    Insert(0, grid);
    grid.StretchToParentSize();

    AddElement(GenerateEntryPointNode());
  }

  private Port generatePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single) {
    return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
  }

  private DialogueNode GenerateEntryPointNode() {
    var node = new DialogueNode {
      title = "Start",
      dialogueText = "Start",
      GUID = Guid.NewGuid().ToString(),
      entryPoint = true
    };

    Port port = generatePort(node, Direction.Output);
    port.portName = "Next";

    node.outputContainer.Add(port);

    node.RefreshExpandedState();
    node.RefreshPorts();

    node.SetPosition(new Rect(100, 200, 100, 150));
    return node;
  }

  public DialogueNode createDialogueNode(string nodeName) {
    var node = new DialogueNode {
      title = nodeName,
      dialogueText = nodeName,
      GUID = Guid.NewGuid().ToString(),
      entryPoint = false
    };

    Port inputPort = generatePort(node, Direction.Input, Port.Capacity.Multi);
    inputPort.portName = "Input";

    node.inputContainer.Add(inputPort);

    // Add create new Choice button

    Button button = new Button(() => {
      addChoicePort(node);
    });
    button.text = "Add Choice";

    node.titleContainer.Add(button);

    node.RefreshExpandedState();
    node.RefreshPorts();

    node.SetPosition(new Rect(Vector2.zero, nodeSize));
    return node;
  }

  public void createNode(string nodeName) {
    AddElement(createDialogueNode(nodeName));
  }

  public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
    var compatiblePorts = new List<Port>();

    ports.ForEach((port) => {
      if (startPort != port && startPort.node != port.node && startPort.direction != port.direction) {
        compatiblePorts.Add(port);
      }
    });

    return compatiblePorts;
  }

  public void addChoicePort(DialogueNode node) {
    Port newPort = generatePort(node, Direction.Output);

    int outputCount = node.outputContainer.Query("connector").ToList().Count;
    newPort.portName = $"Choice {outputCount}";

    Button removeButton = new Button(() => {
      removeChoicePort(node, newPort);
    });
    removeButton.text = "X";


    node.outputContainer.Add(newPort);
    node.RefreshExpandedState();
    node.RefreshPorts();


  }

  public void removeChoicePort(DialogueNode node, Port port) {
    node.outputContainer.Remove(port);

    int i = 1;
    foreach (var outChild in node.outputContainer.Children()) {
      if (outChild.GetType() == typeof(Port)) {
        Port outPort = outChild as Port;
        outPort.portName = $"Choice {i}";
        i++;
      }
    }
  }
}
