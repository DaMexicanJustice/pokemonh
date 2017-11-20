using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownMaster : MonoBehaviour {


	public static TownMaster instance;
	public Town town;
	private Spot spot;
	public Transform btnsParent;
	public GameObject btnPrefab;
	public Image background;
	public Text place;
	public Text description;

	void Awake() {
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
		}
	}

	public void SetupSpots() {
		ClearPrevious ();
		List<Spot> spots = town.spots;
		foreach (Spot spot in spots) {
			GameObject btn = Instantiate (btnPrefab, btnsParent);
			btn.GetComponentInChildren<Text> ().text = spot.spotName;
			btn.GetComponent<Button>().onClick.AddListener(delegate{GoToSpot(spot);});
		}

	}

	private void SetupCharacters(Spot spot) {
		List<BaseCharacter> characters = spot.characters;
		foreach (BaseCharacter bc in characters) {
			GameObject btn = Instantiate (btnPrefab, btnsParent);
			btn.GetComponentInChildren<Text> ().text = "Talk  to  " + bc.characterName;
			if (bc.dialogueTree.nodes.Count > 0) {
				btn.GetComponent<Button> ().onClick.AddListener (delegate {
					TalkToCharacter (bc);
				});
			} else {
				description.text = bc.characterName +  " Not yet implemented!";
			}
		}
	}

	public void Init() {
		background.sprite = town.background;
		place.text = town.townName;
		description.text = town.description;
		SetupSpots ();
	}

	public void InitSpot() {
		background.sprite = spot.background;
		place.text = spot.spotName;
		description.text = spot.description;
		SetupCharacters (spot);
	}

	private void ClearPrevious() {
		foreach (Transform child in btnsParent.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

	public void GoToSpot(Spot s) {
		ClearPrevious ();
		spot = s;
		InitSpot ();
	}

	public void TalkToCharacter(BaseCharacter bc) {
		GameMaster.instance.SetInteractingWithCharacter (bc);
	}


}
