﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

public class DialogueStepConnector : IConnectionTypeDeclaration {

	#region IConnectionTypeDeclaration implementation
	public string Identifier {
		get {
			return "DialogueNode";
		}
	}
	public System.Type Type {
		get {
			return typeof(DialogueStepNode);
		}
	}
	public Color Color {
		get {
			return Color.cyan;
		}
	}
	public string InKnobTex {
		get {
			return "Textures/In_Knob.png";
		}
	}
	public string OutKnobTex {
		get {
			return "Textures/Out_Knob.png";
		}
	}
	#endregion
}
