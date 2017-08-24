using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterSelector : MonoBehaviour {

	public GameObject img1;
	public GameObject img2;
	public GameObject img3;
	public int selection = 0;

	public float smooth = 1f;
	private Quaternion targetRotation;

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectRight() {
		switch (selection) {
		case 0:
			img3.GetComponent<IconCameraFacer> ().SelectBall ();
			img1.GetComponent<IconCameraFacer> ().UnselectBall ();
			img2.GetComponent<IconCameraFacer> ().UnselectBall ();
			selection = 2;
			break;
		case 1:
			img2.GetComponent<IconCameraFacer> ().UnselectBall ();
			img1.GetComponent<IconCameraFacer> ().SelectBall ();
			img3.GetComponent<IconCameraFacer> ().UnselectBall ();
			selection = 0;
			break;
		case 2:
			img2.GetComponent<IconCameraFacer> ().SelectBall ();
			img1.GetComponent<IconCameraFacer> ().UnselectBall ();
			img3.GetComponent<IconCameraFacer> ().UnselectBall ();
			selection = 1;
			break;
		}
	}

	public void SelectLeft() {
		switch (selection) {
		case 0:
			img2.GetComponent<IconCameraFacer> ().SelectBall ();
			img1.GetComponent<IconCameraFacer> ().UnselectBall ();
			img3.GetComponent<IconCameraFacer> ().UnselectBall ();
			selection = 1;
			break;
		case 1:
			img2.GetComponent<IconCameraFacer> ().UnselectBall ();
			img1.GetComponent<IconCameraFacer> ().UnselectBall ();
			img3.GetComponent<IconCameraFacer> ().SelectBall ();
			selection = 2;
			break;
		case 2:
			img1.GetComponent<IconCameraFacer> ().SelectBall ();
			img2.GetComponent<IconCameraFacer> ().UnselectBall ();
			img3.GetComponent<IconCameraFacer> ().UnselectBall ();
			selection = 0;
			break;
		}

	}
}
