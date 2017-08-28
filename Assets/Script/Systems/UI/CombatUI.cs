using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : BaseUI {
	#region implemented abstract members of BaseUI

	public static CombatUI instance;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public override void Hide ()
	{
		gameObject.SetActive (false);
	}

	public override void Show ()
	{
		gameObject.SetActive (true);
	}

	#endregion

}
