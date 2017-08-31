using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : MonoBehaviour {

	public bool IsPersuadableIntoSex(FemaleTrainer.Personality personality) {
		return (personality == FemaleTrainer.Personality.NAUGHTY || personality == FemaleTrainer.Personality.TIMID);
	}

	public bool IsEasyToAnger(FemaleTrainer.Personality personality) {
		return personality == FemaleTrainer.Personality.NOBULLSHIT;
	}

	public bool IsUnresponsiveToFlirt(FemaleTrainer.Personality personality) {
		return personality == FemaleTrainer.Personality.INNOCENT;
	}

	public bool IsPlayerOutOfPokemon() {
		return Player.instance.pokemon.IsKO();
	}

	public bool IsBadgeObtained(string type) {
		int idx = Player.instance.bCollection.FindBadgeIndexByBadgeType (type);
		return idx > 0;
	}

	public bool IsGameReadyToEnd() {
		int badgeCount = 0;
		foreach (bool b in Player.instance.bCollection.badges) {
			if (b) {
				badgeCount++;
			}
		}
		return badgeCount >= 8;
	}

	public bool HasPlayerBeenWithCharacter(FemaleTrainer ft) {
		return ft.hasBeenDisgraced;
	}

	public bool IsPokemonWillingToBreed(SexPokemon sp) {
		return sp.affection >= 100;
	} 
}
