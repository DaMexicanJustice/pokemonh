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
	//public string contextTag;
	public string leftBranchTag;
	public string middleBranchTag;
	public string rightBranchTag;
	public DialogueStepNode leftNode;
	public DialogueStepNode middleNode;
	public DialogueStepNode rightNode;
	public List<string> dialogueText;
	public Sprite background;
	public Sprite characterPortrait;
	public Criteria criteria;

	Vector2 guiPos;
	string dialogueTextArea = "Separate into new speech bubble by using *\n" +
		"Speech bubble 1\n*Speech bubble 2\n*Speech bubble 3\n*Speech bubble 4\n*Speech bubble 5\n*Speech bubble 6";
	DialogueStepConnector stepConnector;

	#region implemented abstract members of Node

	public override Node Create (Vector2 pos)
	{
		guiPos = pos;
		DialogueStepNode node = CreateInstance <DialogueStepNode> ();

		node.name = "Dialogue Node";
		node.rect = new Rect (pos.x, pos.y, 300f, 480f);


		NodeInput.Create (node, "Connection", "DialogueNode");

		NodeOutput.Create (node, "Connection", "DialogueNode");
		NodeOutput.Create (node, "Value", "DialogueNode");
		NodeOutput.Create (node, "Value", "DialogueNode");

		EditorStyles.label.normal.textColor = Color.black;

		return node;
	}

	protected override void NodeGUI ()
	{
		EditorGUILayout.BeginVertical("Box");
		GUILayout.BeginHorizontal();

		// UI components
		characterPortrait = (Sprite) RTEditorGUI.ObjectField("Character Sprite", characterPortrait, false);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		background = (Sprite) RTEditorGUI.ObjectField("Background Sprite", background, false);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		person = (BaseCharacter)RTEditorGUI.ObjectField ("BaseCharacter Object", person, false);
		//characterPortrait = (Sprite)EditorGUILayout.ObjectField(characterPortrait, typeof(Sprite), false, GUILayout.Width(50f), GUILayout.Height(50f));
		//background = (Sprite)EditorGUILayout.ObjectField(background, typeof(Sprite), false, GUILayout.Width(50f), GUILayout.Height(50f));
		//person = (BaseCharacter)EditorGUILayout.ObjectField (person, typeof (BaseCharacter), false, GUILayout.Width (170f), GUILayout.Height (15f));
		GUILayout.EndHorizontal();

		GUILayout.Space(15);
		GUILayout.BeginHorizontal();
		leftBranchTag = (string) RTEditorGUI.TextField(new GUIContent("1st Node Action Name", "The name of the left most branch"), leftBranchTag, GUIStyle.none);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		middleBranchTag = (string) RTEditorGUI.TextField(new GUIContent("2nd Node Action Name", "The name of the left most branch"), middleBranchTag, GUIStyle.none);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		rightBranchTag = (string) RTEditorGUI.TextField(new GUIContent("3rd Node Action Name", "The name of the left most branch"), rightBranchTag, GUIStyle.none);
		GUILayout.EndHorizontal ();

		GUILayout.Space(15);
		GUILayout.BeginHorizontal();
		leftNode = (DialogueStepNode) EditorGUILayout.ObjectField(leftNode, typeof(DialogueStepNode), false, GUILayout.Width(200f), GUILayout.Height(15f));
		OutputKnob (0);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		middleNode = (DialogueStepNode) EditorGUILayout.ObjectField(middleNode, typeof(DialogueStepNode), false, GUILayout.Width(200f), GUILayout.Height(15f));
		OutputKnob (1);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		rightNode = (DialogueStepNode) EditorGUILayout.ObjectField(rightNode, typeof(DialogueStepNode), false, GUILayout.Width(200f), GUILayout.Height(15f));
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

	#endregion

	void TextAreaToList() {
		string[] splitString = dialogueTextArea.Split(new string[] { "*", "*" }, StringSplitOptions.None);
		dialogueText = splitString.OfType<string>().ToList();
		Debug.Log (dialogueText.Count);
	}

}
