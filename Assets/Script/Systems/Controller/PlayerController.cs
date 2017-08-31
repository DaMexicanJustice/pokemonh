using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject playerDetails;
	private bool isOn;
	public Text textPrefab;
	public Image starterPrefab;
	public Image flareon;
	public Image jolteon;
	public Image vaporeon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			ClearUI ();
			Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
			playerDetails.SetActive (isOn);
			Text name = Instantiate (textPrefab, playerDetails.transform);
			name.text = "Name: " + player.playerName;
			Text age = Instantiate (textPrefab, playerDetails.transform);
			age.text = "Age: " + player.age;
			Text gender = Instantiate (textPrefab, playerDetails.transform);
			gender.text = "Gender: " +  player.gender;
			Text starter = Instantiate (textPrefab, playerDetails.transform);
			starter.text = "Starter: ";
			SetStarter (player);
			isOn = !isOn;
		}
	}

	private void ClearUI() {
		foreach(Transform child in playerDetails.transform) {
			Destroy(child.gameObject);
		}
	}

	private void SetStarter(Player player) {
		switch (player.pokemon.pokemonName) {
		case "Flareon":
			Instantiate (flareon, playerDetails.transform);
			break;
		case "Jolteon":
			Instantiate (jolteon, playerDetails.transform);
			break;
		case "Vaporeon":
			Instantiate (vaporeon, playerDetails.transform);
			break;
		}
	}
}
