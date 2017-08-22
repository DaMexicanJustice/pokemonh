using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMaster : MonoBehaviour {

	public List<Town> towns;
	public OverworldUI ui;

	public void Init() {
		ui = GetComponent<OverworldUI> ();
	}

}
