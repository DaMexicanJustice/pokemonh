using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingLight : MonoBehaviour {

	private Image img;
	public float speed;

	// Use this for initialization
	void Start () {
		img = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Color c = img.color;
		c.a = Mathf.Round( Mathf.PingPong(Time.time * speed, 1.0f) );
		img.color = c;

	}
}
