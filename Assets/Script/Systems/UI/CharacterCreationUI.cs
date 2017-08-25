using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationUI : BaseUI {

	public static CharacterCreationUI instance;

	void Awake(){
		if (instance) {
			Destroy (this);
		} else {
			instance = this;
		}
		instance.Hide ();
	}

	#region implemented abstract members of BaseUI

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
