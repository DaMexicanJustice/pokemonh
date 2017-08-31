using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Sex Pokemon", menuName = "Pokemon_H/Pokemon/Sex Pokemon", order=2)]
public class SexPokemon : BaseCharacter {
	[Range(-100,100)]
	public int affection;
	public enum Taste {
		BITTER, SWEET, SOUR, SALTY, SPICY
	}
	public Taste preferredTaste;

	private BerryCompatibility compatibility = new BerryCompatibility();

	public void Feed(Berry berry) {
		Berry.BerryName flavor = berry.berryName;
		affection += compatibility.GetAffectionChange (flavor, preferredTaste);
	}
}
