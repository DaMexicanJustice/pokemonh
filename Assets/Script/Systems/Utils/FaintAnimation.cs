using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaintAnimation : MonoBehaviour
{

	public Image image;

	// Use this for initialization
	void Awake ()
	{
		image = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update ()
	{

		image.color = new Color (image.color.r, image.color.g, image.color.b, image.color.a - 0.01f);
		transform.position = new Vector3 (transform.position.x-1, transform.position.y-1, transform.position.z);

	}
}
