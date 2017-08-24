﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMaster : MonoBehaviour
{
	public DialogueMaster dm;

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

	void Awake ()
	{
		enemyPokemonNameText.text = ePokemon.name + " " + GetGenderSymbol (ePokemon);
		playerPokemonNameText.text = pPokemon.name + " " + GetGenderSymbol (pPokemon);
		SetupMoves (pPokemon);
		Init ();
	}

	void Update ()
	{
		enemyHPText.text = "HP: " + enemyHealthSlider.value + " / 100";
		playerHPText.text = "HP: " + playerHealthSlider.value + " / 100";
		enemyPokemonImage.sprite = ePokemon.sprite;
		playerPokemonImage.sprite = pPokemon.sprite;
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
		pPokemon.curHP = pPokemon.maxHP;
		enemyPokemonImage.sprite = ePokemon.sprite;
		playerPokemonImage.sprite = pPokemon.sprite;
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
			p.TakeDamage (move);
			enemyHealthSlider.value = ePokemon.curHP;
			playerHealthSlider.value = pPokemon.curHP;
			UpdateCombatText (p, move);
			if (p.IsKO ()) {
				PlayKOAnimation (p);
				return;
			}
			Invoke ("EnemyTurn", 2f);
			isPlayersTurn = false;
			AllowInput ();
		}
	}

	public void EnemyTurn() {
		Move move = ePokemon.UseRandomMove ();
		pPokemon.TakeDamage (move);
		enemyHealthSlider.value = ePokemon.curHP;
		playerHealthSlider.value = pPokemon.curHP;
		UpdateCombatText (pPokemon, move);
		if (pPokemon.IsKO ()) {
			PlayKOAnimation (pPokemon, true);
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
		GetComponent<CombatUI> ().Hide ();
		if (isPlayersTurn) {
			dm.NextDialogueStep (1);
		} else {
			dm.NextDialogueStep (0);
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

}