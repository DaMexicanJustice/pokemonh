
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node (false, "Extractor Node")]
public class ExtractorNode : Node {

	public const string ID = "extractorNode";
	public NodeCanvas nodeCanvas;

	public override Node Create (Vector2 pos)
	{
		ExtractorNode node = CreateInstance <ExtractorNode> ();

		node.name = "Extractor Node";
		node.rect = new Rect (pos.x, pos.y, 350f, 100f);

		return node;
	}

	protected override void NodeGUI ()
	{
		
		EditorGUILayout.BeginVertical("Box");

		// UI components
		GUILayout.BeginHorizontal();
		nodeCanvas = (NodeCanvas) RTEditorGUI.ObjectField("Dialogue Tree Canvas", nodeCanvas, false);
		GUILayout.EndHorizontal();

		GUILayout.Space(15);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Create Scenario")) {
			Extract ();
		}
		GUILayout.EndHorizontal();

		GUILayout.EndVertical ();

	}

	public override string GetID {
		get {
			return ID;
		}
	}

	void Extract() {
		if (nodeCanvas != null) {
			NodeExtractor extractor = new NodeExtractor ();
			extractor.Extract (nodeCanvas);
		}
	}

}

