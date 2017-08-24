using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PokemonTypeMaster {

	public bool IsStrongAgainst(string type, string otherType) {
		type = type.ToLower ();
		otherType = otherType.ToLower ();
		if (type.Equals ("fire") && otherType.Equals ("ice")) {
			return true;
		} else if (type.Equals ("fire") && otherType.Equals ("grass")) {
			return true;
		} else if (type.Equals ("fire") && otherType.Equals ("bug")) {
			return true;
		} else if (type.Equals ("fire") && otherType.Equals ("steel")) {
			return true;
		}  else if (type.Equals ("water") && otherType.Equals ("fire")) {
			return true;
		} else if (type.Equals ("water") && otherType.Equals ("rock")) {
			return true;
		} else if (type.Equals ("water") && otherType.Equals ("ground")) {
			return true;
		} else if (type.Equals ("electric") && otherType.Equals ("water")) {
			return true;
		} else if (type.Equals ("electric") && otherType.Equals ("flying")) {
			return true;
		} else if (type.Equals ("grass") && otherType.Equals ("ground")) {
			return true;
		} else if (type.Equals ("grass") && otherType.Equals ("water")) {
			return true;
		} else if (type.Equals ("ice") && otherType.Equals ("grass")) {
			return true;
		} else if (type.Equals ("ice") && otherType.Equals ("ground")) {
			return true;
		} else if (type.Equals ("ice") && otherType.Equals ("flying")) {
			return true;
		} else if (type.Equals ("ice") && otherType.Equals ("dragon")) {
			return true;
		} else if (type.Equals ("fighting") && otherType.Equals ("normal")) {
			return true;
		} else if (type.Equals ("fighting") && otherType.Equals ("ice")) {
			return true;
		} else if (type.Equals ("fighting") && otherType.Equals ("ground")) {
			return true;
		} else if (type.Equals ("flying") && otherType.Equals ("grass")) {
			return true;
		} else if (type.Equals ("flying") && otherType.Equals ("fighting")) {
			return true;
		} else if (type.Equals ("flying") && otherType.Equals ("bug")) {
			return true;
		} else if (type.Equals ("psychic") && otherType.Equals ("poison")) {
			return true;
		} else if (type.Equals ("psychic") && otherType.Equals ("fighting")) {
			return true;
		} else if (type.Equals ("bug") && otherType.Equals ("psychic")) {
			return true;
		} else if (type.Equals ("bug") && otherType.Equals ("dark")) {
			return true;
		} else if (type.Equals ("bug") && otherType.Equals ("grass")) {
			return true;
		} else if (type.Equals ("rock") && otherType.Equals ("fire")) {
			return true;
		} else if (type.Equals ("rock") && otherType.Equals ("ice")) {
			return true;
		} else if (type.Equals ("rock") && otherType.Equals ("ground")) {
			return true;
		} else if (type.Equals ("rock") && otherType.Equals ("bug")) {
			return true;
		} else if (type.Equals ("ghost") && otherType.Equals ("psychic")) {
			return true;
		} else if (type.Equals ("ghost") && otherType.Equals ("ghost")) {
			return true;
		} else if (type.Equals ("dragon") && otherType.Equals ("dragon")) {
			return true;
		} else if (type.Equals ("dark") && otherType.Equals ("psychic")) {
			return true;
		} else if (type.Equals ("dark") && otherType.Equals ("ghost")) {
			return true;
		} else if (type.Equals ("steel") && otherType.Equals ("ice")) {
			return true;
		} else if (type.Equals ("steel") && otherType.Equals ("rock")) {
			return true;
		} else if (type.Equals ("steel") && otherType.Equals ("fairy")) {
			return true;
		} else if (type.Equals ("fairy") && otherType.Equals ("fighting")) {
			return true;
		} else if (type.Equals ("fairy") && otherType.Equals ("dragon")) {
			return true;
		} else if (type.Equals ("fairy") && otherType.Equals ("dark")) {
			return true;
		} else {
			return false;
		}
	}

}
