using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeEditorFramework;

public class NodeExtractor : MonoBehaviour {

	public NodeCanvas nodeCanvas;
	List<DialogueStepNode> dialogueStepNodes = new List<DialogueStepNode> ();

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
			}
			ConnectNodes ();
			AssetDatabase.SaveAssets ();
		}
	}

	void CreateAssetFromNode(Node n) {
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

		if ( dStepNode.contextTag.Equals("Start") ) {
			AssetDatabase.CreateAsset (asset, "Assets/Scriptable Objects/Dialogue Steps/" + asset.person.characterName + "/" + asset.person.characterName + " Start Node.asset");
		} else {
			AssetDatabase.CreateAsset (asset, "Assets/Scriptable Objects/Dialogue Steps/" + asset.person.characterName + "/" + asset.person.characterName + " " + asset.contextTag + ".asset");
		}

		if (asset != null) {
			dialogueStepNodes.Add (asset);
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
					string contextTag = (node as DialogueStepNode).contextTag;
					// Iterate list of dialogueStepNodes to find one that has a matching context tag to list of nodes. 
					foreach (DialogueStepNode dsn in dialogueStepNodes) {
						if (dsn.contextTag.Equals (contextTag)) {
							// Assign dialogueStepNode at position idx to matching dsn
							dialogueStepNodes [idx].leftNode = dsn;
						}
					}
				}
				if (n.Outputs [1].connections.Count > 0) {
					// Yes, save the node we are conneted to
					Node node = n.Outputs [1].connections [0].body;
					// Save the node's context tag
					string contextTag = (node as DialogueStepNode).contextTag;
					// Iterate list of dialogueStepNodes to find one that has a matching context tag to list of nodes. 
					foreach (DialogueStepNode dsn in dialogueStepNodes) {
						if (dsn.contextTag.Equals (contextTag)) {
							// Assign dialogueStepNode at position idx to matching dsn
							dialogueStepNodes [idx].middleNode = dsn;
						}
					}
				}
				if (n.Outputs [2].connections.Count > 0) {
					// Yes, save the node we are conneted to
					Node node = n.Outputs [2].connections [0].body;
					// Save the node's context tag
					string contextTag = (node as DialogueStepNode).contextTag;
					// Iterate list of dialogueStepNodes to find one that has a matching context tag to list of nodes. 
					foreach (DialogueStepNode dsn in dialogueStepNodes) {
						if (dsn.contextTag.Equals (contextTag)) {
							// Assign dialogueStepNode at position idx to matching dsn
							dialogueStepNodes [idx].rightNode = dsn;
						}
					}
				}
				idx++;
			}
		}
	}

}
