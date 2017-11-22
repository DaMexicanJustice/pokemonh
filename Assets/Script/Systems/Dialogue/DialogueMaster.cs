using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeEditorFramework;

public class DialogueMaster : MonoBehaviour
{

	public static DialogueMaster instance;
	private ScriptableToInstance scriptableToInstance;

	private BaseCharacter bc;
	public Node currentStep;
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

	void Update() {
		
	}

	public void Init (BaseCharacter bc)
	{
		this.bc = bc;
		currentStep = bc.startNode;
		SetupPaths ();
		SetupDetails ();
		if (IsDialogueStep(currentStep)) {
			if ( (currentStep as DialogueStepNode).dialogueText.Count > 1) {
				nextText.interactable = true;
			}
		}
	}

	public void NextDialogueStep (int idx)
	{
		DialogueStepNode temp = null;
		// In case something goes wrong, we can revert to this previous step (i.e failed check)
		if (IsDialogueStep (currentStep)) {
			temp = (DialogueStepNode)currentStep;
		}
			switch (idx) {
		case 0:
			if (IsDialogueStep (currentStep)) {
				if (IsDialogueStep ((currentStep as DialogueStepNode).leftNode)) {
					currentStep = (DialogueStepNode)(currentStep as DialogueStepNode).leftNode;
				} else {
					currentStep = (CombatStepNode)(currentStep as DialogueStepNode).leftNode;
				}
			} else {
				currentStep = (DialogueStepNode)(currentStep as CombatStepNode).winStepNode;
			}
				break;
		case 1:
			if (IsDialogueStep (currentStep)) {
				if (IsDialogueStep ((currentStep as DialogueStepNode).middleNode)) {
					currentStep = (DialogueStepNode)(currentStep as DialogueStepNode).middleNode;
				} else {
					currentStep = (CombatStepNode)(currentStep as DialogueStepNode).middleNode;
				}
			} else {
				currentStep = (DialogueStepNode)(currentStep as CombatStepNode).loseStepNode;
			}
				break;
			case 2:
				if (IsDialogueStep ((currentStep as DialogueStepNode).rightNode)) {
					currentStep = (DialogueStepNode)(currentStep as DialogueStepNode).rightNode;
				} else {
					currentStep = (CombatStepNode)(currentStep as DialogueStepNode).rightNode;
				}
				break;
			default:
				currentStep = temp;
				break;
			}

		Debug.Log ("Current step: " + currentStep);

		ClearPrevious ();

		if (!CheckCriteria ()) {
			if (IsDialogueStep (currentStep)) {
				characterDialogue.text = "Requirement not met: " + (currentStep as DialogueStepNode).criteria.reqFailedText;
				GameMaster.instance.spinner.SetActive (true);
				Invoke ("ExitConversation", 3f);
			}
		} else {
			
			if (currentStep as CombatStepNode != null) {
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

			if (currentStep == null) {
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
		if (IsDialogueStep (currentStep)) {
			Criteria c = (currentStep as DialogueStepNode).criteria;

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
		} else {
			return true;
		}
	}

	public void SetupDetails ()
	{
		if (IsDialogueStep (currentStep)) {
			background.sprite = (currentStep as DialogueStepNode).background;
			character.sprite = (currentStep as DialogueStepNode).characterPortrait;
			characterName.text = (currentStep as DialogueStepNode).person.characterName;
			characterDialogue.text = (currentStep as DialogueStepNode).dialogueText [0];
		}
	}

	private void ClearPrevious ()
	{
		foreach (Transform child in btnsParent.transform) {
			GameObject.Destroy (child.gameObject);
		}
	}

	public void SetupPaths ()
	{
		if (IsDialogueStep (currentStep)) {
			if (textIndex >= (currentStep as DialogueStepNode).dialogueText.Count) {
			
				if ((currentStep as DialogueStepNode).leftBranchTag.Length > 0) {
					GameObject btn = Instantiate (btnPrefab, btnsParent);
					btn.GetComponentInChildren<Text> ().text = (currentStep as DialogueStepNode).leftBranchTag;
					btn.GetComponent<Button> ().onClick.AddListener (delegate {
						NextDialogueStep (0);
					});
				} 
				if ((currentStep as DialogueStepNode).middleBranchTag.Length > 0) {
					GameObject btn = Instantiate (btnPrefab, btnsParent);
					btn.GetComponentInChildren<Text> ().text = (currentStep as DialogueStepNode).middleBranchTag;
					btn.GetComponent<Button> ().onClick.AddListener (delegate {
						NextDialogueStep (1);
					});
				}
				if ((currentStep as DialogueStepNode).rightBranchTag.Length > 0) {
					GameObject btn = Instantiate (btnPrefab, btnsParent);
					btn.GetComponentInChildren<Text> ().text = (currentStep as DialogueStepNode).rightBranchTag;
					btn.GetComponent<Button> ().onClick.AddListener (delegate {
						NextDialogueStep (2);
					});
				}
			}
		}

	}

	public void NextText ()
	{
		if (textIndex < (currentStep as DialogueStepNode).dialogueText.Count) {
			characterDialogue.text = (currentStep as DialogueStepNode).dialogueText [textIndex];
			textIndex++;
		} 

		if (textIndex >= (currentStep as DialogueStepNode).dialogueText.Count) {
			nextText.interactable = false;
			SetupPaths ();
		}
	}

	private void CheckSpecialCase ()
	{
		if (IsDialogueStep (currentStep)) {
			Debug.Log ( (currentStep as DialogueStepNode).contextTag.ToLower() );
			switch ((currentStep as DialogueStepNode).contextTag.ToLower ()) {
			case "end":
				ExitConversation ();
				break;
			case "player won":
				if (bc as FemaleTrainer != null) {
					PokemonInstance.Type pt = scriptableToInstance.GetInstanceOfScriptableObject ((bc as FemaleTrainer).pokemon [0]).type;
					Debug.Log ("Claiming badge: " + pt.ToString ());
					GameMaster.instance.GiveBadgeToPlayer (pt.ToString ());
				}
				break;
			}
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

	public BaseCharacter TestGetBaseCharacter() {
		return bc;
	}

	public bool IsDialogueStep(Node node) {
		return node as DialogueStepNode != null;
	}

}
