
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

public class ExtractorNodeConnector : IConnectionTypeDeclaration {

	#region IConnectionTypeDeclaration implementation
	public string Identifier {
		get {
			return "ExtractorNode";
		}
	}
	public System.Type Type {
		get {
			return typeof(ExtractorNode);
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
