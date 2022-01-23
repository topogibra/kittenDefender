


using System.Collections.Generic;
using Subtegral.DialogueSystem.DataContainers;

public class DLink {
  public DNode Target;
  public DNode Base;

  public string Name;

}

public class DNode {
  public List<DLink> Next = new List<DLink>();
  public List<DLink> Previous = new List<DLink>();

  public string DialogueText;

  public string id;

}


public class DTree {
  public Dictionary<string, DNode> nodes = new Dictionary<string, DNode>();

  public DNode root;

  public DTree(DialogueContainer dialogueContainer) {
    // Add root
    DialogueNodeData first = dialogueContainer.DialogueNodeData[0];
    root = new DNode {
      id = first.NodeGUID,
      DialogueText = first.DialogueText
    };
    nodes.Add(root.id, root);

    // Add all nodes
    foreach (var node in dialogueContainer.DialogueNodeData) {
      if (node == first) continue;
      nodes.Add(node.NodeGUID,
      new DNode {
        id = node.NodeGUID,
        DialogueText = node.DialogueText
      });
    }

    // Add all links 
    foreach (var link in dialogueContainer.NodeLinks) {
      if (link == dialogueContainer.NodeLinks[0]) continue;
      DNode baseNode = nodes[link.BaseNodeGUID];
      DNode targetNode = nodes[link.TargetNodeGUID];
      DLink dLink = new DLink {
        Name = link.PortName,
        Target = targetNode,
        Base = baseNode
      };

      baseNode.Next.Add(dLink);
      targetNode.Previous.Add(dLink);
    }

  }

}
