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

	private PokemonInstance ePokemon;
	private PokemonInstance pPokemon;

	public Transform movesParent;
	public Button moveBtnPrefab;

	private bool isInteractable;
	public float exitCombatDelay;
	private float playKOAnimationDelay;
	public float healthUpdateDelay;
	[Range(0, 0.02f)]
	public float textSpeed;
	private int textIndex = 0;

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

	public void UpdateCombatText (PokemonInstance pokemon, Move move, string effective)
	{
		combatText.text = "";
		string toPrint = pokemon.pokemonName + " uses " + move.moveName + ". " + effective;
		textIndex = 0;
		StartCoroutine (TypeText(toPrint, textSpeed));
	}

	public void Init (PokemonInstance player, PokemonInstance enemy)
	{
		ePokemon = enemy;
		pPokemon = player;
		// ---------------------------------------------------------------------------------------- //
		enemyPokemonImage.sprite = ePokemon.sprite;
		enemyPokemonNameText.text = ePokemon.pokemonName + " " + GetGenderSymbol (ePokemon);
		playerPokemonNameText.text = pPokemon.pokemonName + " " + GetGenderSymbol (pPokemon);

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
			btn.GetComponentInChildren<Text> ().text = move.moveName + "\n (" + move.type + ")";
			btn.GetComponent<Button> ().onClick.AddListener (delegate {
				PlayerTurn (move);
			});
		}
	}

	public void PlayerTurn (Move move)
	{
		ePokemon.TakeDamage (move);
		UpdateCombatText(pPokemon, move, GetEffectivenessOfMove(move, ePokemon));
		playKOAnimationDelay = enemyHealthSlider.value * healthUpdateDelay + 1f;
		StartCoroutine(DecreaseSlider(enemyHealthSlider, ePokemon));
		if (!ePokemon.IsKO ()) {
			AllowInput ();
			Invoke ("EnemyTurn", 2f);
		} else {
			StartCoroutine(PlayKOAnimation(ePokemon, playKOAnimationDelay));
			StartCoroutine(ExitCombat(true, exitCombatDelay));
		}
	}

	public void EnemyTurn() {
		Move move = ePokemon.UseRandomMove ();
		pPokemon.TakeDamage (move);
		UpdateCombatText(ePokemon, move, GetEffectivenessOfMove(move, pPokemon));
		playKOAnimationDelay = playerHealthSlider.value * healthUpdateDelay + 1f;
		StartCoroutine (DecreaseSlider (playerHealthSlider, pPokemon));
		if (!pPokemon.IsKO ()) {
			AllowInput ();
		} else {
			StartCoroutine(PlayKOAnimation(pPokemon, playKOAnimationDelay));
			StartCoroutine(ExitCombat(false, exitCombatDelay));
		}
	}

	IEnumerator PlayKOAnimation (PokemonInstance p, float playDelay) {
		yield return new WaitForSeconds(playDelay);
		combatText.text = p.pokemonName + " has been defeated!";

		if (p == ePokemon) {
			enemyPokemonImage.gameObject.AddComponent<FaintAnimation> ();
		} else {
			playerPokemonImage.gameObject.AddComponent<FaintAnimation> ();
		}

	}

	IEnumerator ExitCombat (bool playerWon, float delayTime)
	{
		yield return new WaitForSeconds(delayTime + playKOAnimationDelay);
		foreach (Transform t in movesParent.transform) {
			Destroy (t.gameObject);
		}
		CombatUI.instance.Hide ();
		if (playerWon) {
			DialogueMaster.instance.NextDialogueStep (1);
			enemyPokemonImage.gameObject.GetComponent<FaintAnimation> ().Expire ();
		} else {
			DialogueMaster.instance.NextDialogueStep (0);
			playerPokemonImage.gameObject.GetComponent<FaintAnimation> ().Expire ();
		}
	}

	private void AllowInput() {
		Button[] btns = GetComponentsInChildren<Button> ();
		foreach (Button btn in btns) {
			btn.interactable = isInteractable;
		}
		isInteractable = !isInteractable;
	}

	IEnumerator DecreaseSlider(Slider slider, PokemonInstance pokemon)
	{
		float timeSlice = (slider.maxValue / 100);
		while (true) {
			slider.value -= timeSlice;
			if (slider.value <= pokemon.curHP) {
				break;
			}
			yield return new WaitForSeconds(healthUpdateDelay);
		}
	}

	private string GetEffectivenessOfMove(Move move, PokemonInstance defender) {
		if (defender.IsStrongAgainst(move))
		{
			return "It's not very effective";
		}
		else if (defender.IsWeakAgainst(move))
		{
			return "It's super effective";
		}
		else {
			return "";
		}
	}

	IEnumerator TypeText(string toWrite, float textSpeed) {
		while (textIndex < toWrite.Length) {
			combatText.text += toWrite [textIndex];
			textIndex++;
			yield return new WaitForSeconds (textSpeed);
		}
	}

}