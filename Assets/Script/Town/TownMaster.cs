using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownMaster : MonoBehaviour {

	public Town town;
	private Spot spot;
	public Transform btnsParent;
	public GameObject btnPrefab;
	public Image background;
	public Text place;
	public Text description;

	public GameMaster gm;

	public void SetupSpots() {
		ClearPrevious ();
		int idx = 0;
		List<Spot> spots = town.spots;
		foreach (Spot spot in spots) {
			GameObject btn = Instantiate (btnPrefab, btnsParent);
			btn.GetComponentInChildren<Text> ().text = spot.name;
			btn.GetComponent<Button>().onClick.AddListener(delegate{GoToSpot(spot);});
		}

	}

	private void SetupCharacters(Spot spot) {
		int idx = 0;
		List<BaseCharacter> characters = spot.characters;
		foreach (BaseCharacter bs in characters) {
			GameObject btn = Instantiate (btnPrefab, btnsParent);
			btn.GetComponentInChildren<Text> ().text = "Talk  to  " + bs.name;
			if (bs.ds.connectedSteps.Count > 0) {
				btn.GetComponent<Button> ().onClick.AddListener (delegate {
					TalkToCharacter (bs);
				});
			} else {
				description.text = bs.name +  " Not yet implemented!";
			}
			idx++;

		}
	}

	public void Init() {
		background.sprite = town.background;
		place.text = town.name;
		description.text = town.description;
		SetupSpots ();
	}

	public void InitSpot() {
		background.sprite = spot.background;
		place.text = spot.name;
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
		gm.SetInteractingWithCharacter (bc);
	}


}
