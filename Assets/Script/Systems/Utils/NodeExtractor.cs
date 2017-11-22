using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeEditorFramework;

public class NodeExtractor : MonoBehaviour {

	public NodeCanvas nodeCanvas;
	List<DialogueStepNode> dialogueStepNodes = new List<DialogueStepNode> ();
	List<CombatStepNode> combatStepNodes = new List<CombatStepNode>();

	void Awake() {
		if (nodeCanvas != null) {
			foreach (Node n in nodeCanvas.nodes) {
				if (n as DialogueStepNode != null) {
					CreateAssetFromNode (n);
				} 
			}

			ConnectNodes ();

			AssetDatabase.SaveAssets ();
		}
	}

	public void Extract(NodeCanvas canvas) {
		nodeCanvas = canvas;
		if (nodeCanvas != null) {
			foreach (Node n in nodeCanvas.nodes) {
				if (n as DialogueStepNode != null) {
					CreateAssetFromNode (n);
				} 
				if (n as CombatStepNode != null) {
					CreateAssetFromNode (n);
				}
			}
			ConnectNodes ();
			AssetDatabase.SaveAssets ();
		}
	}

	void CreateAssetFromNode(Node n) {
		if (n as DialogueStepNode != null) {
			DialogueStepNode dStepNode = (DialogueStepNode)n;
			DialogueStepNode asset = ScriptableObject.CreateInstance<DialogueStepNode> ();
			asset.background = dStepNode.background;
			asset.characterPortrait = dStepNode.characterPortrait;
			asset.contextTag = dStepNode.contextTag;
			asset.criteria = dStepNode.criteria;
			asset.dialogueText = dStepNode.dialogueText;
			asset.person = dStepNode.person;
			asset.leftBranchTag = dStepNode.leftBranchTag;
			asset.middleBranchTag = dStepNode.middleBranchTag;
			asset.rightBranchTag = dStepNode.rightBranchTag;

			if (dStepNode.contextTag.Equals ("Start")) {
				AssetDatabase.CreateAsset (asset, "Assets/Scriptable Objects/Dialogue Steps/" + asset.person.characterName + "/" + asset.person.characterName + " Start Node.asset");
			} else {
				AssetDatabase.CreateAsset (asset, "Assets/Scriptable Objects/Dialogue Steps/" + asset.person.characterName + "/" + asset.person.characterName + " " + asset.contextTag + ".asset");
			}

			if (asset != null) {
				dialogueStepNodes.Add (asset);
			}
		} else if (n as CombatStepNode) {
			CombatStepNode cStepNode = (CombatStepNode)n;
			CombatStepNode asset = ScriptableObject.CreateInstance<CombatStepNode> ();
			asset.trainer = cStepNode.trainer;
			asset.winStepNode = cStepNode.winStepNode;
			asset.loseStepNode = cStepNode.loseStepNode;
			string characterName = asset.trainer.characterName;
			AssetDatabase.CreateAsset (asset, "Assets/Scriptable Objects/Dialogue Steps/" + characterName + "/" + characterName + " Combat Node.asset");
			combatStepNodes.Add (asset);
		}
	}

	void ConnectNodes() {
		int idx = 0;
		foreach (Node n in nodeCanvas.nodes) {
			if (n as DialogueStepNode != null) {
				// Left branch connection
				// Is there a connection on the left branch?
				if (n.Outputs [0].connections.Count > 0) {
					// Yes, save the node we are conneted to
					Node node = n.Outputs [0].connections [0].body;
					// Save the node's context tag
					if (node as DialogueStepNode != null) {
						ConnectDialogueNodeToDialogueNode (node, idx, 0);
					} else if (node as CombatStepNode != null) {
						ConnectDialogueNodeToCombatNode (node, idx, 0);
					}
				}
				if (n.Outputs [1].connections.Count > 0) {
					// Yes, save the node we are conneted to
					Node node = n.Outputs [1].connections [0].body;
					// Save the node's context tag
					if (node as DialogueStepNode != null) {
						ConnectDialogueNodeToDialogueNode (node, idx, 1);
					} else if (node as CombatStepNode != null) {
						ConnectDialogueNodeToCombatNode (node, idx, 1);
					}
				}
				if (n.Outputs [2].connections.Count > 0) {
					// Yes, save the node we are conneted to
					Node node = n.Outputs [2].connections [0].body;
					// Save the node's context tag
					if (node as DialogueStepNode != null) {
						ConnectDialogueNodeToDialogueNode (node, idx, 2);
					} else if (node as CombatStepNode != null) {
						ConnectDialogueNodeToCombatNode (node, idx, 2);
					}
				}
				idx++;
			} else if (n as CombatStepNode != null) {
				if (n.Outputs [0].connections.Count > 0) {
					Node node = n.Outputs [0].connections [0].body;
					ConnectCombatStepToDialogueStep (node, idx, 0);
				}

				if (n.Outputs [1].connections.Count > 0) {
					Node node = n.Outputs [1].connections [0].body;
					ConnectCombatStepToDialogueStep (node, idx, 1);
				}
			}
		}
	}

	void ConnectCombatStepToDialogueStep(Node node, int idx, int direction) {
		if (node as DialogueStepNode != null) {
			string contextTag = (node as DialogueStepNode).contextTag;
			foreach (DialogueStepNode dsn in dialogueStepNodes) {
				if (dsn.contextTag.Equals (contextTag)) {
					if (direction == 0) {
						combatStepNodes [0].winStepNode = dsn;
					} else if (direction == 1) {
						combatStepNodes [0].loseStepNode = dsn;
					}
				}
			}
		}
	}

	void ConnectDialogueNodeToCombatNode(Node node, int idx, int direction) {
		if (node as CombatStepNode != null) {
			string contextTag = (node as CombatStepNode).contextTag;
			foreach (CombatStepNode csn in combatStepNodes) {
				if (csn.contextTag.Equals (contextTag)) {
					foreach (DialogueStepNode dsn in dialogueStepNodes) {
						if (dsn.leftBranchTag.Equals ("Challenge") || dsn.middleBranchTag.Equals ("Challenge") || dsn.rightBranchTag.Equals ("Challenge")) {
							if (contextTag.Equals("Combat")) {
								switch (direction) {
								case 0:
									dialogueStepNodes [idx].leftNode = csn;
									break;
								case 1:
									dialogueStepNodes [idx].middleNode = csn;
									break;
								case 2:
									dialogueStepNodes [idx].rightNode = csn;
									break;
								}
							}
						}
					}
				}
			}
		}
	}

	void ConnectDialogueNodeToDialogueNode(Node node, int idx, int direction) {
		if (node as DialogueStepNode != null) {
			string contextTag = (node as DialogueStepNode).contextTag;
			// Iterate list of dialogueStepNodes to find one that has a matching context tag to list of nodes. 
			foreach (DialogueStepNode dsn in dialogueStepNodes) {
				if (dsn.contextTag.Equals (contextTag)) {
					// Assign dialogueStepNode at position idx to matching dsn
					switch (direction) {
						case 0:
							dialogueStepNodes [idx].leftNode = dsn;
							break;
						case 1:
							dialogueStepNodes [idx].middleNode = dsn;
							break;
						case 2:
							dialogueStepNodes [idx].rightNode = dsn;
							break;
					}
				}
			}
		} 
	}

}
