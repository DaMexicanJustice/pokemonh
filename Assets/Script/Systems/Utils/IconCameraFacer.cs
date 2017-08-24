using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconCameraFacer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt(Camera.main.transform.position, Vector3.up);
	}

	public void SelectBall() {
		gameObject.transform.localScale = new Vector3 (1f,1f,1f);
	}

	public void UnselectBall() {
		gameObject.transform.localScale = new Vector3 (0.7f,0.7f,0.7f);
	}
}
