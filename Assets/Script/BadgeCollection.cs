using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeCollection {

	private bool fireBadge = false;
	private bool iceBadge = false;
	private bool rockBadge = false;
	private bool metalBadge = false;
	private bool waterBadge = false;
	private bool grassBadge = false;
	private bool flyingBadge = false;
	private bool electricBadge = false;

	public List<bool> badges = new List<bool>();

	public BadgeCollection() {
		badges.Add (grassBadge);
		badges.Add (electricBadge);
		badges.Add (rockBadge);
		badges.Add (flyingBadge);
		badges.Add (waterBadge);
		badges.Add (fireBadge);
		badges.Add (iceBadge);
		badges.Add (metalBadge);
	}

	public int FindBadgeIndexByBadgeType(string str) {
		switch (str.ToLower()) {
		case "grass":
			return 0;
		case "electric":
			return 1;
		case "rock":
			return 2;
		case "flying":
			return 3;
		case "water":
			return 4;
		case "fire":
			return 5;
		case "ice":
			return 6;
		case "steel":
			return 7;
		default:
			return -1;
		}
	}

}
