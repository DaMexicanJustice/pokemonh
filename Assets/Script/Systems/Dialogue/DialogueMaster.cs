using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMaster : MonoBehaviour {

	public BaseCharacter bc;
	public DialogueStep currentStep;
	public GameObject btnPrefab;
	public Transform btnsParent;

	public Image background;
	public Image character;
	public Text characterName;
	public Text characterDialogue;

	public CombatMaster cm = null;

	void Awake() {
		
	}

	void Update() {
		//Debug.Log (currentStep.contextTag);
	}

	public void Init() {
		
		SetupPaths ();
		SetupDetails ();
	}

	public void NextDialogueStep(int idx) {
		currentStep = currentStep.connectedSteps [idx];
		ClearPrevious ();
		if (currentStep as CombatStep != null) {
			List<Pokemon> pokemon = (currentStep as CombatStep).ft.pokemon;
			cm.ePokemon = pokemon [0];
			cm.pPokemon = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().pokemon;
			cm.Init ();
			CombatUI.instance.Show ();
		} 

		SetupPaths ();
		SetupDetails ();
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
	}

}
