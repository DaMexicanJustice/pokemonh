using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using UnityEditor;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node (false, "Combat Node")]
public class CombatStepNode : Node  {

	public const string ID = "Combat Node";
	bool playerWonCombat;
	public Trainer trainer;
	public DialogueStepNode winStepNode;
	public DialogueStepNode loseStepNode;
	public string contextTag = "Combat";

	#region implemented abstract members of Node

	public override Node Create (Vector2 pos)
	{
		CombatStepNode node = CreateInstance <CombatStepNode> ();

		node.name = "Combat Node";
		node.rect = new Rect (pos.x, pos.y, 300f, 100f);

		NodeInput.Create (node, "Connection i1", "DialogueNode");

		NodeOutput.Create (node, "Connection o1", "DialogueNode");
		NodeOutput.Create (node, "Connection o2", "DialogueNode");

		return node;
	}

	protected override void NodeGUI ()
	{
		EditorGUILayout.BeginVertical("Box");

		GUILayout.BeginHorizontal();
		trainer = (Trainer)RTEditorGUI.ObjectField ("Trainer Object", trainer, false);
		GUILayout.EndHorizontal();

		GUILayout.Space(15);
		GUILayout.BeginHorizontal();
		GUILayout.Label ("Player Won");
		OutputKnob (0);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		GUILayout.Label ("Player Lost");
		OutputKnob (1);
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();
	}

	public override bool Calculate() {
		if (Outputs [0].connections.Count > 0) {
			winStepNode = (DialogueStepNode)Outputs [0].connections [0].body;
		} else {
			winStepNode = null;
		}
		if (Outputs [1].connections.Count > 0) {
			loseStepNode = (DialogueStepNode)Outputs [1].connections [0].body;
		} else {
			loseStepNode = null;
		}
		return true;
	}

	public override string GetID {
		get {
			return ID;
		}
	}

	#endregion

}
