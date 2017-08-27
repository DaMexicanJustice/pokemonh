using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster instance;

	public Player player;

	public DialogueMaster dm;
	public TownMaster tm;
	public OverworldMaster om;
	public CharacterCreator cc;
	public List<Town> towns;

	public BaseCharacter testChar;
	public Town testTown;

	public enum GameState {
		CONVERSATION, COMBAT, OVERWORLD, TOWN
	}
	public GameState state;
	public BadgeHandler bHandler;

	void Awake() {

		if (instance) {
			Destroy (this);
		} else {
			instance = this;
		}

		state = GameState.OVERWORLD;
		towns = om.towns;

	}

	void Start() {
		HideUI ();
		cc.Init ();
		CharacterCreationUI.instance.Show ();
	}

	void Update() {
		
	}

	public void HideUI() {
		ConversationUI.instance.Hide ();
		TownUI.instance.Hide ();
		OverworldUI.instance.Hide ();
	}

	public void SetInteractingWithCharacter(BaseCharacter bc) {
		dm.bc = bc;
		dm.currentStep = bc.ds;
		dm.Init ();
		HideUI ();
		ConversationUI.instance.Show ();
	}

	public void DoneInteractingWithCharacter() {
		dm.bc = null;
		HideUI ();
	}

	public void MoveToCity() {
		OverworldUI.instance.Hide ();
		tm.town = towns [1];
		tm.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToBeach() {
		OverworldUI.instance.Hide ();
		tm.town = towns [0];
		tm.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToMountain() {
		OverworldUI.instance.Hide ();
		tm.town = towns [6];
		tm.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToIslands() {
		OverworldUI.instance.Hide ();
		tm.town = towns [5];
		tm.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToGlacier() {
		OverworldUI.instance.Hide ();
		tm.town = towns [3];
		tm.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToForest() {
		OverworldUI.instance.Hide ();
		tm.town = towns [2];
		tm.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToTrainStation() {
		OverworldUI.instance.Hide ();
		tm.town = towns [7];
		tm.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToHotSprings() {
		OverworldUI.instance.Hide ();
		tm.town = towns [4];
		tm.Init ();
		TownUI.instance.Show ();
	}

	public void GiveBadgeToPlayer(string badge) {
		int idx = player.bCollection.FindBadgeIndexByBadgeType (badge);
		player.bCollection.badges [idx] = true;
		Debug.Log (player.bCollection.badges [idx]);
		CheckBadges (player.bCollection.badges);
	}

	public void CheckBadges(List<bool> badges) {
		foreach (bool b in badges) {
			if (b) {
				bHandler.EnableBadge ( badges.IndexOf(b) + 1 );
			}
		}
	}

}
