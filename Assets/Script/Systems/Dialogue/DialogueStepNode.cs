using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEditor;
using System;
using System.Linq;

[System.Serializable]
[Node (false, "Dialogue Node")]
public class DialogueStepNode : Node {

	public const string ID = "dialogueNode";

	public BaseCharacter person;
	public string contextTag;
	public string leftBranchTag;
	public string middleBranchTag;
	public string rightBranchTag;
	public Node leftNode;
	public Node middleNode;
	public Node rightNode;
	public List<string> dialogueText;
	public Sprite background;
	public Sprite characterPortrait;
	public Criteria criteria;

	Vector2 guiPos;
	string dialogueTextArea;
	DialogueStepConnector stepConnector;
	int nodeCount;
	bool hasLoadedText;

	#region implemented abstract members of Node

	public override Node Create (Vector2 pos)
	{
		
		guiPos = pos;
		DialogueStepNode node = CreateInstance <DialogueStepNode> ();

		node.name = "Dialogue Node";
		node.rect = new Rect (pos.x, pos.y, 300f, 420f);

		//Debug chan

		NodeInput.Create (node, "Connection i1", "DialogueNode");

		NodeOutput.Create (node, "Connection o1", "DialogueNode");
		NodeOutput.Create (node, "Connection o2", "DialogueNode");
		NodeOutput.Create (node, "Connection o3", "DialogueNode");

		return node;
	}

	protected override void NodeGUI ()
	{
		
		EditorGUILayout.BeginVertical("Box");

		// UI components
		GUILayout.BeginHorizontal();
		characterPortrait = (Sprite) RTEditorGUI.ObjectField("Character Sprite", characterPortrait, false);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		background = (Sprite) RTEditorGUI.ObjectField("Background Sprite", background, false);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		person = (BaseCharacter)RTEditorGUI.ObjectField ("BaseCharacter Object", person, false);
		GUILayout.EndHorizontal();

		GUILayout.Space(15);
		GUILayout.BeginHorizontal();
		contextTag = (string) RTEditorGUI.TextField(new GUIContent("Context Tag", "Context of the dialogue step. I.e BATTLE, END, BADGE"), contextTag, GUIStyle.none);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		leftBranchTag = (string) RTEditorGUI.TextField(new GUIContent("1st Node Action Name", "The name of the left most branch"), leftBranchTag, GUIStyle.none);
		OutputKnob (0);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		middleBranchTag = (string) RTEditorGUI.TextField(new GUIContent("2nd Node Action Name", "The name of the left most branch"), middleBranchTag, GUIStyle.none);
		OutputKnob (1);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		rightBranchTag = (string) RTEditorGUI.TextField(new GUIContent("3rd Node Action Name", "The name of the left most branch"), rightBranchTag, GUIStyle.none);
		OutputKnob (2);
		GUILayout.EndHorizontal ();




		GUILayout.Space(15);
		GUILayout.BeginHorizontal ();
		criteria = (Criteria) EditorGUILayout.ObjectField(criteria, typeof(Criteria), false, GUILayout.Width(200f), GUILayout.Height(15f));
		GUILayout.EndHorizontal ();

		GUILayout.Space(15);
		GUILayout.BeginHorizontal ();
		EditorGUILayout.BeginScrollView (guiPos, GUILayout.Height (100));
		EditorStyles.textField.wordWrap = true;
		dialogueTextArea = EditorGUILayout.TextArea(dialogueTextArea, GUILayout.ExpandHeight(true));
		EditorStyles.textField.wordWrap = false;
		EditorGUILayout.EndScrollView();
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();

		/* Causes issues under editing and debugging circumstances !!!
		if (dialogueText.Count > 0 && !hasLoadedText) {
			foreach (string str in dialogueText) {
				dialogueTextArea += str + "*";
			}
			dialogueTextArea.TrimEnd ('*');
			hasLoadedText = true;
		} */

		if (GUI.changed) {
			NodeEditor.RecalculateFrom (this);
			TextAreaToList ();
		}

	}

	public override string GetID {
		get {
			return ID;
		}
	}

	public override bool Calculate() {
		if (Outputs [0].connections.Count > 0) {
			if (Outputs [0].connections [0].body as DialogueStepNode != null) {
				leftNode = (DialogueStepNode)Outputs [0].connections [0].body;
			}
			if (Outputs [0].connections [0].body as CombatStepNode != null) {
				leftNode = (CombatStepNode)Outputs [0].connections [0].body;
			}
		} else {
			leftNode = null;
		}
		if (Outputs [1].connections.Count > 0) {
			if (Outputs [1].connections [0].body as DialogueStepNode != null) {
				middleNode = (DialogueStepNode)Outputs [1].connections [0].body;
			}
			if (Outputs [1].connections [0].body as CombatStepNode != null) {
				middleNode = (CombatStepNode)Outputs [1].connections [0].body;
			}
		} else {
			middleNode = null;
		}
		if (Outputs [2].connections.Count > 0) {
			if (Outputs [2].connections [0].body as DialogueStepNode != null) {
				rightNode = (DialogueStepNode)Outputs [2].connections [0].body;
			}
			if (Outputs [2].connections [0].body as CombatStepNode != null) {
				rightNode = (CombatStepNode)Outputs [2].connections [0].body;
			}
		} else {
			rightNode = null;
		}
		return true;
	}

	#endregion

	void TextAreaToList() {
		if (dialogueTextArea.Length > 1) {
			string[] splitString = dialogueTextArea.Split (new string[] { "*", "*" }, StringSplitOptions.None);
			dialogueText = splitString.OfType<string> ().ToList ();
			Debug.Log (dialogueText.Count);
		}
	}

	public int GetNodeCount() {
		if (leftNode != null) {
			nodeCount++;
		} 
		if (middleNode != null) {
			nodeCount++;
		}
		if (rightNode != null) {
			nodeCount++;
		}
		return nodeCount;
	}

}