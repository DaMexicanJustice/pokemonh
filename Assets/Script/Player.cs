using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance;

	public PokemonInstance pokemon;
	public string name;
	public string age;
	public string gender;

	public BadgeCollection bCollection = new BadgeCollection();

	void Awake() {
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
		}
	}

}
