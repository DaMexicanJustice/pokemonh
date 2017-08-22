using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour {

	public Pokemon pok1;
	public Pokemon pok2;

	void Start() {
		Move move = pok2.UseMove (3);
		Debug.Log ("The Move of type: " + move.type.ToString() + " is strong against: " + pok1.type.ToString() + "\n --------------------------");
		Debug.Log ( pok1.IsWeakAgainst(move) );
	}

	void Update() {
		
	}
}
