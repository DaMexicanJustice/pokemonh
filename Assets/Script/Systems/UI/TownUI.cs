﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownUI : BaseUI {
	#region implemented abstract members of BaseUI

	public static TownUI instance;

	void Awake(){
		if (instance) {
			Destroy (this);
		} else {
			instance = this;
		}
		instance.Hide ();
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
