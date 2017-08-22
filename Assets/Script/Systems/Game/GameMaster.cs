using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public DialogueMaster dm;
	public TownMaster tm;
	public OverworldMaster om;
	public List<Town> towns;

	public BaseCharacter testChar;
	public Town testTown;

	public enum GameState {
		CONVERSATION, COMBAT, OVERWORLD, TOWN
	}
	public GameState state;

	void Awake() {
		state = GameState.OVERWORLD;
		towns = om.towns;
	}

	void Start() {
		/*
		tm.town = testTown;
		tm.SetupSpots ();
		tm.Init ();
		tm.ui.Show ();
		*/
		om.Init ();
		om.ui.Show ();

	}

	void Update() {

	}

	public void SetInteractingWithCharacter(BaseCharacter bc) {
		dm.bc = testChar;
		dm.Init ();
		dm.gameObject.GetComponent<ConversationUI> ().Show ();
	}

	public void DoneInteractingWithCharacter() {
		dm.bc = null;
		dm.gameObject.GetComponent<ConversationUI> ().Hide ();
	}

	public void MoveToCity() {
		om.ui.Hide ();
		tm.town = towns [1];
		tm.Init ();
		tm.ui.Show ();
	}

	public void MoveToBeach() {
		om.ui.Hide ();
		tm.town = towns [0];
		tm.Init ();
		tm.ui.Show ();
	}

	public void MoveToMountain() {
		om.ui.Hide ();
		tm.town = towns [6];
		tm.Init ();
		tm.ui.Show ();
	}

	public void MoveToIslands() {
		om.ui.Hide ();
		tm.town = towns [5];
		tm.Init ();
		tm.ui.Show ();
	}

	public void MoveToGlacier() {
		om.ui.Hide ();
		tm.town = towns [3];
		tm.Init ();
		tm.ui.Show ();
	}

	public void MoveToForest() {
		om.ui.Hide ();
		tm.town = towns [2];
		tm.Init ();
		tm.ui.Show ();
	}

	public void MoveToTrainStation() {
		om.ui.Hide ();
		tm.town = towns [7];
		tm.Init ();
		tm.ui.Show ();
	}

	public void MoveToHotSprings() {
		om.ui.Hide ();
		tm.town = towns [4];
		tm.Init ();
		tm.ui.Show ();
	}

}
