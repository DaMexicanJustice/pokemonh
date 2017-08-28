﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMaster : MonoBehaviour
{

	public static CombatMaster instance;

	public Slider enemyHealthSlider;
	public Slider playerHealthSlider;
	public Text enemyHPText;
	public Text playerHPText;
	public Text enemyPokemonNameText;
	public Text playerPokemonNameText;
	public Text combatText;
	public Image enemyPokemonImage;
	public Image playerPokemonImage;

	public Pokemon ePokemon;
	public Pokemon pPokemon;

	public Transform movesParent;
	public Button moveBtnPrefab;

	public bool isPlayersTurn = true;

	void Awake()
	{
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
		}
		Debug.Log ("CombatMaster: " + instance);
	}

	void Update ()
	{
		Debug.Log ("Enemy: " + ePokemon.curHP);
		Debug.Log ("Player: " + pPokemon.curHP);
	
	}

	public void UpdateCombatText (Pokemon p, Move m)
	{
		if (p.IsStrongAgainst (m)) {
			combatText.text = p.name + " uses " + m.name + "!\n " + m.name + " is not very effectice and deals " + m.damage / 2 + " damage";
		} else if (p.IsWeakAgainst (m)) {
			combatText.text = p.name + " uses " + m.name + "!\n " + m.name + " is super effective and deals " + m.damage * 2 + " damage"; 
		} else {
			combatText.text = p.name + " uses " + m.name + "!\n " + m.name + " deals " + m.damage + " damage"; 
		}
	}

	public void Init ()
	{
		ePokemon.curHP = ePokemon.maxHP;
		enemyPokemonImage.sprite = ePokemon.sprite;
		playerPokemonImage.sprite = pPokemon.sprite;
		enemyPokemonNameText.text = ePokemon.name + " " + GetGenderSymbol (ePokemon);
		playerPokemonNameText.text = pPokemon.name + " " + GetGenderSymbol (pPokemon);
		enemyPokemonImage.sprite = ePokemon.sprite;
		playerPokemonImage.GetComponent<Animator> ().runtimeAnimatorController = pPokemon.controller;
		enemyHPText.text = "HP: " + enemyHealthSlider.value + " / 100";
		playerHPText.text = "HP: " + playerHealthSlider.value + " / 100";
		SetupMoves (pPokemon);
	}

	private string GetGenderSymbol (Pokemon p)
	{
		if (p.gender == Pokemon.Gender.MALE) {
			return "♂";
		} else {
			return "♀";
		}
	}

	private void SetupMoves (Pokemon p)
	{
		List<Move> moves = p.moves;
		foreach (Move move in moves) {
			Button btn = Instantiate (moveBtnPrefab, movesParent);
			btn.GetComponentInChildren<Text> ().text = move.name + "\n (" + move.type + ")";
			btn.GetComponent<Button> ().onClick.AddListener (delegate {
				Attack (move, ePokemon);
			});
		}
	}

	public void Attack (Move move, Pokemon p)
	{
		if (isPlayersTurn) {
			UpdateCombatText (pPokemon, move);
			p.TakeDamage (move);
			StartCoroutine (DecreseSlider (enemyHealthSlider, p));
			if (p.IsKO ()) {
				//PlayKOAnimation (ePokemon);
				return;
			}
			Invoke ("EnemyTurn", 2f);
			isPlayersTurn = false;
			AllowInput ();
		} 
	}
		
	public void EnemyTurn() {
		Move move = ePokemon.UseRandomMove ();
		UpdateCombatText (ePokemon, move);
		pPokemon.TakeDamage (move);
		StartCoroutine (DecreseSlider (playerHealthSlider, pPokemon));
		if (pPokemon.IsKO ()) {
			//PlayKOAnimation (pPokemon, true);
			return;
		} else {
			isPlayersTurn = true;
			AllowInput ();
		}
	}

	public void PlayKOAnimation (Pokemon p, bool isPlayer=false)
	{
		Invoke ("ExitCombat", 3f);
		combatText.text = p.name + " has been defeated!";
		if (!isPlayer) {
			enemyPokemonImage.gameObject.AddComponent<FaintAnimation> ();
		} else {
			playerPokemonImage.gameObject.AddComponent<FaintAnimation> ();
		}
	}

	private void ExitCombat ()
	{
		CombatUI.instance.Hide ();
		if (isPlayersTurn) {
			DialogueMaster.instance.NextDialogueStep (1);
		} else {
			DialogueMaster.instance.NextDialogueStep (0);
		}
	}

	private void AllowInput() {
		if (!isPlayersTurn) {
			Button[] btns = GetComponentsInChildren<Button> ();
			foreach (Button btn in btns) {
				btn.interactable = false;
			}
		} else {
			Button[] btns = GetComponentsInChildren<Button> ();
			foreach (Button btn in btns) {
				btn.interactable = true;
			}
		}
	}

	IEnumerator DecreseSlider(Slider slider, Pokemon pokemon)
	{
		if (slider != null)
		{
			float timeSlice = (slider.value / 100);
			while (slider.value >= 0)
			{
				slider.value -= timeSlice;
				enemyHPText.text = "HP: " + enemyHealthSlider.value + " / 100";
				playerHPText.text = "HP: " + playerHealthSlider.value + " / 100";
				yield return new WaitForSeconds(0.02f);

				if (slider.value <= 0) {
					if (isPlayersTurn) {
						PlayKOAnimation (pokemon);
					} else {
						PlayKOAnimation (pokemon, true);
					}
				}

				if (slider.value <= pokemon.curHP) {
					break;
				}
			}
		}
		yield return null;
	}

}