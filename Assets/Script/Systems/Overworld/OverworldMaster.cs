using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMaster : MonoBehaviour {

	public static OverworldMaster instance;
	public List<Town> towns;

	void Awake() {
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
		}
	}

	public void Init() {
		
	}

}
