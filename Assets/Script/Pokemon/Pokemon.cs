﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Pokémon", menuName = "Pokemon_H/Pokemon/Pokemon", order=0)]
public class Pokemon : ScriptableObject {

	public string name;
	public enum Gender {
		MALE, FEMALE
	}
	public Gender gender;

	public enum Type {
		NORMAL, FIRE, WATER, ELECTRIC, GRASS, 
		ICE, FIGHTING, POISON, GROUND, FLYING,
		PSYCHIC, BUG, ROCK, GHOST, DRAGON,
		DARK, STEEL, FAIRY
	}
	public Type type;

	public List<Move> moves;

	private PokemonTypeMaster ptm;

	public int maxHP;
	private int curHP;

	public Pokemon() {
		ptm = new PokemonTypeMaster ();
		curHP = maxHP;
	}

	public bool IsStrongAgainst(Move move) {
		// If my Eletric is strong against your Water, I take half damage
		return ptm.IsStrongAgainst (type.ToString(), move.type.ToString ());
	}

		// If my Water is weak against your electric, I take double damage
	public bool IsWeakAgainst(Move move) {
		return ptm.IsStrongAgainst (move.type.ToString (), type.ToString ());
	}

	public void TakeDamage(int value, Move move) {
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