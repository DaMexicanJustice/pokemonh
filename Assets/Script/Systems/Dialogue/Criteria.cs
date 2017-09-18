using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Criteria", menuName = "Pokemon_H/Dialogue/Criteria", order=2)]
public class Criteria : ScriptableObject {

	public string reqFailedText;

	public enum PokemonType
	{
		NONE, FIRE ,  WATER ,  ELECTRIC , ICE , DRAGON , DARK , FLYING , FIGHTING ,
		PSYCHIC , STEEL , BUG , GRASS , FAIRY , ROCK , GROUND , POISON , NORMAL 
	}
	public PokemonType starterTypeReq;

	public enum PokemonGender
	{
		NONE, MALE, FEMALE
	}
	public PokemonGender starterGenderReq;

	public TM tmInInventory;

	public Berry berryInContainer;
	public int berryQuantityReq;

	[Range(0,50)]
	public int pokemonAffectionReq;

	public BaseCharacter characterMetReq;

	public DialogueStep taskDoneReq;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
