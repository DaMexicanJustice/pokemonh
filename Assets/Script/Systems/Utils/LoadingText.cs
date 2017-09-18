using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour {

	private Text text;
	public float interval;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		InvokeRepeating ("TextAnim",0f,interval);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TextAnim() {
		if (!text.text.Equals ("...")) {
			text.text += ".";
		} else {
			text.text = "";
		}
	}
}
