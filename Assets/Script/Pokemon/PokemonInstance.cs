using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonInstance {

	public string name;
	public enum Gender {
		MALE, FEMALE
	}
	public Gender gender;
	public Sprite sprite;
	public RuntimeAnimatorController controller;
	public enum Type {
		NORMAL, FIRE, WATER, ELECTRIC, GRASS, 
		ICE, FIGHTING, POISON, GROUND, FLYING,
		PSYCHIC, BUG, ROCK, GHOST, DRAGON,
		DARK, STEEL, FAIRY
	}
	public Type type;

	public List<Move> moves;

	private PokemonTypeMaster ptm = new PokemonTypeMaster();

	public int maxHP;
	public int curHP;

	public PokemonInstance(Pokemon pokemon) {
		name = pokemon.name;
		gender = (Gender) pokemon.gender;
		sprite = pokemon.sprite;
		controller = pokemon.controller;
		type = (Type) pokemon.type;
		moves = pokemon.moves;
		maxHP = pokemon.maxHP;
		curHP = pokemon.curHP;
	}

	public bool IsStrongAgainst(Move move) {
		// If my Eletric is strong against your Water, I take half damage
		return ptm.IsStrongAgainst (type.ToString(), move.type.ToString ());
	}

	// If my Water is weak against your electric, I take double damage
	public bool IsWeakAgainst(Move move) {
		return ptm.IsStrongAgainst (move.type.ToString (), type.ToString ());
	}

	public void TakeDamage(Move move) {
		int value = move.damage;
		if (IsStrongAgainst (move)) {
			value = Mathf.RoundToInt(value / 2);
		} else if (IsWeakAgainst(move)) {
			value *= 2;
		}
		curHP -= value;
	}

	public bool IsKO() {
		return curHP <= 0;
	}

	public Move UseMove(int index) {
		return moves [index];
	}

	public Move UseRandomMove() {
		return moves[Random.Range(0,3)];
	}

}
