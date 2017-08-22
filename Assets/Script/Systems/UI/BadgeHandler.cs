using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeHandler : MonoBehaviour {

	Image[] badges;

	void Awake() {
		badges = GetComponentsInChildren<Image> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnableBadge(int idx) {
		badges [idx].color = new Color32 (255,255,255,255);
	}
}
