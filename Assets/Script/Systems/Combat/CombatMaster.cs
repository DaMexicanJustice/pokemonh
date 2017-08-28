using System.Collections;
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

	public PokemonInstance ePokemon;
	public PokemonInstance pPokemon;

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
		enemyHPText.text = "HP: " + enemyHealthSlider.value + " / 100";
		playerHPText.text = "HP: " + playerHealthSlider.value + " / 100";
	}

	public void UpdateCombatText (PokemonInstance p, Move m)
	{
		if (p.IsStrongAgainst (m)) {
			combatText.text = p.name + " uses " + m.name + "!\n " + m.name + " is not very effectice and deals " + m.damage / 2 + " damage";
		} else if (p.IsWeakAgainst (m)) {
			combatText.text = p.name + " uses " + m.name + "!\n " + m.name + " is super effective and deals " + m.damage * 2 + " damage"; 
		} else {
			combatText.text = p.name + " uses " + m.name + "!\n " + m.name + " deals " + m.damage + " damage"; 
		}
	}

	public void Init (PokemonInstance player, PokemonInstance enemy)
	{
		ePokemon = enemy;
		pPokemon = player;
		// ---------------------------------------------------------------------------------------- //
		enemyPokemonImage.sprite = ePokemon.sprite;
		enemyPokemonNameText.text = ePokemon.name + " " + GetGenderSymbol (ePokemon);
		playerPokemonNameText.text = pPokemon.name + " " + GetGenderSymbol (pPokemon);

		playerPokemonImage.GetComponent<Animator> ().runtimeAnimatorController = pPokemon.controller;

		enemyHPText.text = "HP: " + ePokemon.curHP + " / 100";
		playerHPText.text = "HP: " + pPokemon.curHP + " / 100";
		enemyHealthSlider.value = ePokemon.curHP;
		playerHealthSlider.value = pPokemon.curHP;
		SetupMoves (pPokemon);
	}

	private string GetGenderSymbol (PokemonInstance p)
	{
		if (p.gender == PokemonInstance.Gender.MALE) {
			return "♂";
		} else {
			return "♀";
		}
	}

	private void SetupMoves (PokemonInstance p)
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

	public void Attack (Move move, PokemonInstance p)
	{
		UpdateCombatText (pPokemon, move);
		p.TakeDamage (move);
		StartCoroutine (DecreaseSlider (enemyHealthSlider, p));
		if (!p.IsKO ()) {
			isPlayersTurn = !isPlayersTurn;
			AllowInput ();
			Invoke ("EnemyTurn", 2f);
		} 
	}
		
	public void EnemyTurn() {
		Move move = ePokemon.UseRandomMove ();
		UpdateCombatText (ePokemon, move);
		pPokemon.TakeDamage (move);
		StartCoroutine (DecreaseSlider (playerHealthSlider, pPokemon));
		if (!pPokemon.IsKO ()) {
			isPlayersTurn = !isPlayersTurn;
			AllowInput ();
		} 
	}

	public void PlayKOAnimation (PokemonInstance p, bool isPlayer=false)
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
		foreach(Transform t in movesParent.transform) {
			Destroy (t.gameObject);
		}
		CombatUI.instance.Hide ();
		if (isPlayersTurn) {
			DialogueMaster.instance.NextDialogueStep (1);
			enemyPokemonImage.gameObject.GetComponent<FaintAnimation> ().Expire ();
		} else {
			DialogueMaster.instance.NextDialogueStep (0);
			playerPokemonImage.gameObject.GetComponent<FaintAnimation> ().Expire ();
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

	IEnumerator DecreaseSlider(Slider slider, PokemonInstance pokemon)
	{
		float timeSlice = (slider.value / 100);
		while (true) {
			Debug.Log (pokemon.curHP);
			slider.value -= timeSlice;
			if ( (ePokemon.curHP <= 0 || pPokemon.curHP <= 0) && slider.value <= 0) {
				if (isPlayersTurn) {
					PlayKOAnimation (pokemon);
				} else {
					PlayKOAnimation (pokemon, true);
				}
			}

			if (slider.value <= pokemon.curHP) {
				break;
			}
			yield return new WaitForSeconds(0.02f);
		}
	}

}