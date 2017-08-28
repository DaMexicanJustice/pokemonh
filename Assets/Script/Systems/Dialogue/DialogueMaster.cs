﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMaster : MonoBehaviour {

	public static DialogueMaster instance;

	public BaseCharacter bc;
	public DialogueStep currentStep;
	public GameObject btnPrefab;
	public Transform btnsParent;

	public Image background;
	public Image character;
	public Text characterName;
	public Text characterDialogue;

	void Awake() {
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
		}
		Debug.Log ("DialogueMaster: " + instance);
	}

	void Update() {
		
	}

	public void Init() {
		SetupPaths ();
		SetupDetails ();
	}

	public void NextDialogueStep(int idx) {
		currentStep = currentStep.connectedSteps [idx];
		ClearPrevious ();
		if (currentStep as CombatStep != null) {
			CombatMaster.instance.ePokemon = (bc as Trainer).pokemon [0];
			// Player Pokemon
			CombatMaster.instance.pPokemon = Player.instance.pokemon;
			// Start Combat
			CombatMaster.instance.Init ();
			CombatUI.instance.Show ();
		} 
			
		SetupDetails ();
		SetupPaths ();
		CheckSpecialCase ();
	}

	public void SetupDetails() {
		background.sprite = currentStep.background;
		character.sprite = currentStep.character;
		characterName.text = bc.name;
		characterDialogue.text = currentStep.dialogueText;
	}

	private void ClearPrevious() {
		foreach (Transform child in btnsParent.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

	public void SetupPaths() {
		if (currentStep.leftBranchTag.Length > 0) {
			Debug.Log (currentStep.leftBranchTag);
			GameObject btn = Instantiate (btnPrefab, btnsParent);
			btn.GetComponentInChildren<Text> ().text = currentStep.leftBranchTag;
			btn.GetComponent<Button>().onClick.AddListener(delegate{NextDialogueStep(0);});
		} 
		if (currentStep.middleBranchTag.Length > 0) {
			Debug.Log (currentStep.middleBranchTag);
			GameObject btn = Instantiate (btnPrefab, btnsParent);
			btn.GetComponentInChildren<Text> ().text = currentStep.middleBranchTag;
			btn.GetComponent<Button>().onClick.AddListener(delegate{NextDialogueStep(1);});
		}
		if (currentStep.rightBranchTag.Length > 0) {
			Debug.Log (currentStep.rightBranchTag);
			GameObject btn = Instantiate (btnPrefab, btnsParent);
			btn.GetComponentInChildren<Text> ().text = currentStep.rightBranchTag;
			btn.GetComponent<Button>().onClick.AddListener(delegate{NextDialogueStep(2);});
		}

		if (currentStep.connectedSteps.Count <= 0) {
			characterDialogue.text = "Branch under development. Returning to Town";
			Invoke ("ExitConversation", 2f);
		}

	}

	private void CheckSpecialCase() {
		switch (currentStep.contextTag.ToLower()) {
		case "end":
			ConversationUI.instance.Hide ();
			TownUI.instance.Show ();
			break;
		case "badge":
			if (bc as FemaleTrainer != null) {
				Pokemon.Type pt = (bc as FemaleTrainer).pokemon [0].type;
				Debug.Log ("Claiming badge: " + pt.ToString());
				GameMaster.instance.GiveBadgeToPlayer (pt.ToString ());
			}
			break;
		}
	}

	private void ExitConversation() {
		ConversationUI.instance.Hide ();
		TownUI.instance.Show ();
	}

}
