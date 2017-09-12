using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public GameObject overlay;
	public GameObject playerDetails;
	public bool isOn;
	public Text textPrefab;
	public Image starterPrefab;
	public Image flareon;
	public Image jolteon;
	public Image vaporeon;
	public GameObject tmContainer;
	public Button btnPrefab;
	public Slider healthSlider;
	public Button optionBtnPrefab;
	public Transform optionsContainer;

	private TM tm;

	public GameObject dialogueBox;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			overlay.SetActive (isOn);
			ReloadUI ();
			isOn = !isOn;
		}
	}

	private void ReloadUI() {
		ClearUI ();
		Text name = Instantiate (textPrefab, playerDetails.transform);
		name.text = AutoSpace("Name:") + Player.instance.playerName;
		Text age = Instantiate (textPrefab, playerDetails.transform);
		age.text = AutoSpace("Age:") + Player.instance.age;
		Text gender = Instantiate (textPrefab, playerDetails.transform);
		gender.text = AutoSpace("Gender:") + Player.instance.gender;
		Text starter = Instantiate (textPrefab, playerDetails.transform);
		starter.text = "Starter";
		SetStarter ();

		foreach (TM tm in Player.instance.inventory.tms) {
			Button btn = Instantiate (btnPrefab, tmContainer.transform);
			btn.GetComponentInChildren<Text> ().text = "TM" + tm.number;
			btn.onClick.AddListener(delegate{PromptAlert(tm);});
		}
	}

	private void PromptAlert(TM tm) {
		
		this.tm = tm;
		dialogueBox.SetActive (true);

		if (!tm.MoveAllowed(Player.instance.pokemon)) {
			GameObject.Find ("Alert Text").GetComponent<Text> ().text = "Teach " + Player.instance.pokemon.pokemonName + " cannot learn " + tm.move.moveName;
			Button btn = Instantiate (optionBtnPrefab, optionsContainer);
			btn.GetComponentInChildren<Text> ().text = "OK";
		} else {

		GameObject.Find ("Alert Text").GetComponent<Text> ().text = "Teach " + Player.instance.pokemon.pokemonName + " TM" + tm.number + " and forget:";
		int idx = 0;
		foreach (Move move in Player.instance.pokemon.moves) {
			Button btn = Instantiate (optionBtnPrefab, optionsContainer);
			btn.GetComponentInChildren<Text> ().text = move.moveName;
			btn.onClick.AddListener(delegate{AcceptPrompt(btn.GetComponentInChildren<Text> ().text);});
			idx++;
		}
		Button cancelBtn = Instantiate (optionBtnPrefab, optionsContainer);
		cancelBtn.GetComponentInChildren<Text> ().text = "Abort";
		cancelBtn.onClick.AddListener(delegate{DeclinePrompt();});
		}
	}

	public void AcceptPrompt(string moveName) {
		ClearDialogueOptions ();
		foreach (Move move in Player.instance.pokemon.moves) {
			string moveToLower = move.moveName.ToLower();
			string otherMoveToLower = moveName.ToLower();
			if (moveToLower.Equals (otherMoveToLower)) {
				tm.TeachPlayerPokemonMove (move);
			}
		}
		dialogueBox.SetActive (false);
		ReloadUI ();
	}

	public void DeclinePrompt() {
		dialogueBox.SetActive (false);
		tm = null;
		ClearDialogueOptions ();
	}

	private void ClearDialogueOptions() {
		foreach (Transform child in optionsContainer.transform) {
			Destroy (child.gameObject);
		}
	}

	private void ClearUI ()
	{
		foreach (Transform child in playerDetails.transform) {
			Destroy (child.gameObject);
		}
		foreach (Transform child in tmContainer.transform) {
			Destroy (child.gameObject);
		}
	}

	public string AutoSpace(string str) {
		int spaces = 3;
		string tmp = "";
		for (int i = 0; i < spaces; i++) {
			tmp += " ";
		}
		str += tmp;
		return str;
	}

	private void SetStarter ()
	{
		switch (Player.instance.pokemon.pokemonName) {
		case "Flareon":
			Instantiate (flareon, playerDetails.transform);
			break;
		case "Jolteon":
			Instantiate (jolteon, playerDetails.transform);
			break;
		case "Vaporeon":
			Instantiate (vaporeon, playerDetails.transform);
			break;
		}
		Slider health = Instantiate (healthSlider, playerDetails.transform);
		health.maxValue = Player.instance.pokemon.maxHP;
		health.minValue = 0;
		health.value = Player.instance.pokemon.curHP;
		Text hp = Instantiate (textPrefab, playerDetails.transform);
		hp.text = Player.instance.pokemon.curHP + " / " + Player.instance.pokemon.maxHP;
		Text type = Instantiate (textPrefab, playerDetails.transform);
		type.text = Player.instance.pokemon.type.ToString ();
		Text moves = Instantiate (textPrefab, playerDetails.transform);
		int i = 1;
		foreach (Move move in Player.instance.pokemon.moves) {
			moves.text += move.moveName + "      ";
			if (i == 2) {
				moves.text += "\n";
			}
			i++;
		}
		moves.text.Remove(moves.text.Length - 1);
	}
}
