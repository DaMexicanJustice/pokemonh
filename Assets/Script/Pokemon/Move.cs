using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Move", menuName = "Pokemon_H/Pokemon/Move", order=2)]
public class Move : ScriptableObject {

	public string name;
	public int damage;
	public enum Type {
		NORMAL, FIRE, WATER, ELECTRIC, GRASS, 
		ICE, FIGHTING, POISON, GROUND, FLYING,
		PSYCHIC, BUG, ROCK, GHOST, DRAGON,
		DARK, STEEL, FAIRY
	}
	public Type type;

}
