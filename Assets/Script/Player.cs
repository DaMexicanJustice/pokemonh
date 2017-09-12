using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance;

	public PokemonInstance pokemon;
	public string playerName;
	public string age;
	public string gender;
	private Pokemon p;
	public Inventory inventory = new Inventory ();
	public List<TM> tms = new List<TM>();
	public bool doRun = false;

	public BadgeCollection bCollection = new BadgeCollection();

	void Awake() {
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
		}
	}

	void Update() {
		if (doRun) {
			DoStuff ();
			doRun = false;
		}
	}

	public void DoStuff() {
		inventory.tms = tms;
	}

}
