using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster instance;

	public Player player;

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
			Destroy (instance);
		} else {
			instance = this;
		}
	}

	void Start() {
		HideUI ();
		cc.Init ();
		CharacterCreationUI.instance.Show ();
		towns = OverworldMaster.instance.towns;
	}

	void Update() {
		
	}

	public void HideUI() {
		CombatUI.instance.Hide ();
		ConversationUI.instance.Hide ();
		TownUI.instance.Hide ();
		OverworldUI.instance.Hide ();
	}

	public void SetInteractingWithCharacter(BaseCharacter bc) {
		DialogueMaster.instance.Init (bc);
		DialogueMaster.instance.currentStep = bc.startNode;
		HideUI ();
		ConversationUI.instance.Show ();
	}

	public void DoneInteractingWithCharacter() {
		HideUI ();
	}

	public void MoveToCity() {
		OverworldUI.instance.Hide ();
		TownMaster.instance.town = towns [1];
		TownMaster.instance.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToBeach() {
		OverworldUI.instance.Hide ();
		TownMaster.instance.town = towns [0];
		TownMaster.instance.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToMountain() {
		OverworldUI.instance.Hide ();
		TownMaster.instance.town = towns [6];
		TownMaster.instance.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToIslands() {
		OverworldUI.instance.Hide ();
		TownMaster.instance.town = towns [5];
		TownMaster.instance.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToGlacier() {
		OverworldUI.instance.Hide ();
		TownMaster.instance.town = towns [3];
		TownMaster.instance.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToForest() {
		OverworldUI.instance.Hide ();
		TownMaster.instance.town = towns [2];
		TownMaster.instance.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToTrainStation() {
		OverworldUI.instance.Hide ();
		TownMaster.instance.town = towns [7];
		TownMaster.instance.Init ();
		TownUI.instance.Show ();
	}

	public void MoveToHotSprings() {
		OverworldUI.instance.Hide ();
		TownMaster.instance.town = towns [4];
		TownMaster.instance.Init ();
		TownUI.instance.Show ();
	}

	public void GiveBadgeToPlayer(string badge) {
		Player player = Player.instance;
		int idx = player.bCollection.FindBadgeIndexByBadgeType (badge);
		player.bCollection.badges [idx] = true;
		UnlockBadgesInUI (player.bCollection.badges);
	}

	public void UnlockBadgesInUI(List<bool> badges) {
		foreach (bool b in badges) {
			if (b) {
				bHandler.EnableBadge ( badges.IndexOf(b) + 1 );
			}
		}
	}

}
