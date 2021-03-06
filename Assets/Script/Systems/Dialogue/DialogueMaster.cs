﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMaster : MonoBehaviour
{

	public static DialogueMaster instance;
	private ScriptableToInstance scriptableToInstance;

	private BaseCharacter bc;
	public DialogueStep currentStep;
	public GameObject btnPrefab;
	public Transform btnsParent;

	public Image background;
	public Image character;
	public Text characterName;
	public Text characterDialogue;

	public Button nextText;
	private int textIndex = 1;

	void Awake ()
	{
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
		}
		scriptableToInstance = new ScriptableToInstance ();
	}

	void Update ()
	{

	}

	public void Init (BaseCharacter bc)
	{
		this.bc = bc;
		currentStep = bc.startNode;
		currentStep.person = bc;
		SetupPaths ();
		SetupDetails ();

		if (currentStep.dialogueText.Count > 1) {
			nextText.interactable = true;
		}
	}

	public void NextDialogueStep (int idx)
	{
		// In case something goes wrong, we can revert to this previous step (i.e failed check)
		DialogueStep temp = currentStep;
		currentStep = currentStep.connectedSteps [idx];
		ClearPrevious ();

		if (!CheckCriteria ()) {
			characterDialogue.text = "Requirement not met: " + currentStep.criteria.reqFailedText;
			GameMaster.instance.spinner.SetActive (true);
			Invoke ("ExitConversation", 3f);
		} else {
			if (currentStep as CombatStep != null) {
				if (Player.instance.pokemon.curHP > 0) {
					PokemonInstance pInstance = scriptableToInstance.GetInstanceOfScriptableObject ((bc as Trainer).pokemon [0]);
					CombatMaster.instance.Init (Player.instance.pokemon, pInstance);
					CombatUI.instance.Show ();
				} else {
					temp.dialogueText [0] = "Your Pokémon has fainted. You can't do battle!";
					currentStep = temp;
				}
			} 

			SetupDetails ();

			if (currentStep.connectedSteps.Count <= 0) {
				characterDialogue.text = "Branch under development. Returning to Town";
				Invoke ("ExitConversation", 2f);
			} else {
				SetupPaths ();
				CheckSpecialCase ();
			}
		}
	}

	private bool CheckCriteria ()
	{
		Criteria c = currentStep.criteria;

		if (c == null) {
			return true;
		} else {

			// Check if there is a criteria, if so, check if requirement is met and if not return false. Otherwise return true

			//Berry criteria - RETURN TRUE IF: BERRY IS IN PLAYER INVENTORY AND PLAYER HAS AN AMOUNT EQUAL TO REQUIRED AMOUNT OR MORE
			if (c.berryInContainer != null) {
				foreach (Berry b in Player.instance.inventory.berries.Keys) {
					if (b == c.berryInContainer) {
						return Player.instance.inventory.berries [b] >= c.berryQuantityReq;
					}
				}
			} 
		// Character criteria
		else if (c.characterMetReq != null) {
			
				// Pokemon affection criteria
			} else if (c.pokemonAffectionReq > 0) {
				if (currentStep.person as SexPokemon != null) {
					SexPokemon sp = currentStep.person as SexPokemon;
					if (sp.affection >= c.pokemonAffectionReq) {
						return true;
					}
				}
			}
		// Pokemon gender criteria
		else if (c.starterGenderReq != Criteria.PokemonGender.NONE) {
				return Player.instance.pokemon.gender.ToString ().Equals (c.starterGenderReq.ToString ());
			} 
		// Pokemon type criteria
		else if (c.starterTypeReq != Criteria.PokemonType.NONE) {
				return Player.instance.pokemon.type.ToString ().Equals (c.starterTypeReq.ToString ());
			}
		// TM criteria
		else if (c.tmInInventory != null) {
				foreach (TM tm in Player.instance.inventory.tms) {
					return tm == c.tmInInventory;
				}
			}
		// Quest criteria
		else if (c.taskDoneReq != null) {
				return ProgressData.instance.finishedTasks.Contains (c.taskDoneReq);
			} 

			return false;
		}
	}

	public void SetupDetails ()
	{
		background.sprite = currentStep.background;
		character.sprite = currentStep.character;
		characterName.text = bc.characterName;
		characterDialogue.text = currentStep.dialogueText [0];
	}

	private void ClearPrevious ()
	{
		foreach (Transform child in btnsParent.transform) {
			GameObject.Destroy (child.gameObject);
		}
	}

	public void SetupPaths ()
	{

		if (textIndex >= currentStep.dialogueText.Count) {

			if (currentStep.leftBranchTag.Length > 0) {
				Debug.Log (currentStep.leftBranchTag);
				GameObject btn = Instantiate (btnPrefab, btnsParent);
				btn.GetComponentInChildren<Text> ().text = currentStep.leftBranchTag;
				btn.GetComponent<Button> ().onClick.AddListener (delegate {
					NextDialogueStep (0);
				});
			} 
			if (currentStep.middleBranchTag.Length > 0) {
				Debug.Log (currentStep.middleBranchTag);
				GameObject btn = Instantiate (btnPrefab, btnsParent);
				btn.GetComponentInChildren<Text> ().text = currentStep.middleBranchTag;
				btn.GetComponent<Button> ().onClick.AddListener (delegate {
					NextDialogueStep (1);
				});
			}
			if (currentStep.rightBranchTag.Length > 0) {
				Debug.Log (currentStep.rightBranchTag);
				GameObject btn = Instantiate (btnPrefab, btnsParent);
				btn.GetComponentInChildren<Text> ().text = currentStep.rightBranchTag;
				btn.GetComponent<Button> ().onClick.AddListener (delegate {
					NextDialogueStep (2);
				});
			}
		}

	}

	public void NextText ()
	{
		if (textIndex < currentStep.dialogueText.Count) {
			characterDialogue.text = currentStep.dialogueText [textIndex];
			textIndex++;
		} 

		if (textIndex >= currentStep.dialogueText.Count) {
			nextText.interactable = false;
			SetupPaths ();
		}
	}

	private void CheckSpecialCase ()
	{
		switch (currentStep.contextTag.ToLower ()) {
		case "end":
			ExitConversation ();
			break;
		case "badge":
			if (bc as FemaleTrainer != null) {
				PokemonInstance.Type pt = scriptableToInstance.GetInstanceOfScriptableObject ((bc as FemaleTrainer).pokemon [0]).type;
				Debug.Log ("Claiming badge: " + pt.ToString ());
				GameMaster.instance.GiveBadgeToPlayer (pt.ToString ());
			}
			break;
		}
	}

	private void ExitConversation ()
	{
		ConversationUI.instance.Hide ();
		TownUI.instance.Show ();
		currentStep = null;
		bc = null;
		GameMaster.instance.spinner.SetActive (false);
		textIndex = 0;
		nextText.interactable = false;
	}

}
