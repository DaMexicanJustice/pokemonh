using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontAdjuster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Text> ().text.ToUpper ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
