using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableToInstance {

	public Pokemon flareon;
	public Pokemon vaporeon;
	public Pokemon jolteon;

	public PokemonInstance GetInstanceOfScriptableObject(Pokemon pokemon) {
		PokemonInstance instance = new PokemonInstance (pokemon);
		return instance;
	}
}
