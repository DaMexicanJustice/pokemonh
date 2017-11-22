using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

public class CombatStepConnector : IConnectionTypeDeclaration {
	
	#region IConnectionTypeDeclaration implementation
	public string Identifier {
		get {
			return "CombatNode";
		}
	}
	public System.Type Type {
		get {
			return typeof(CombatStepNode);
		}
	}
	public Color Color {
		get {
			return Color.red;
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
