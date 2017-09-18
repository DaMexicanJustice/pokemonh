using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData : MonoBehaviour {

	public static ProgressData instance;

	void Awake() {

		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}

	}

	public List<DialogueStep> finishedTasks;

}
