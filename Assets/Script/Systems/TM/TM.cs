using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New TM", menuName = "Pokemon_H/TM/TM", order=0)]
public class TM : ScriptableObject {
	[Range(0,100)]
	public int number;
	public Move move;

	public void TeachPlayerPokemonMove(Move toOverride) {
		Player.instance.pokemon.moves [Player.instance.pokemon.moves.IndexOf (toOverride)] = move;
	}

	public bool MoveAllowed(PokemonInstance pokemon) {
		// Already knows move?
		foreach(Move m in pokemon.moves) {
			if (m.moveName.Equals (move.moveName)) {
				return false;
			}
		}
		// Criteria if can learn
		if (pokemon.IsStrongAgainst (move)) {
			return false;
		} else if (pokemon.IsWeakAgainst (move)) {
			return false;
		} else if (pokemon.type.ToString ().Equals (move.type.ToString ())) {
			return true;
		} else {
			return true;
		}
	}
}
