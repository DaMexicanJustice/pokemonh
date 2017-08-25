using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeCollection {

	private bool fireBadge;
	private bool iceBadge;
	private bool rockBadge;
	private bool metalBadge;
	private bool waterBadge;
	private bool grassBadge;
	private bool flyingBadge;
	private bool electricBadge;

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
		switch (str) {
		case "grass":
			return 0;
			break;
		case "electric":
			return 1;
			break;
		case "rock":
			return 2;
			break;
		case "flying":
			return 3;
			break;
		case "water":
			return 4;
			break;
		case "fire":
			return 5;
			break;
		case "ice":
			return 6;
			break;
		case "metal":
			return 7;
			break;
		default:
			return -1;
		}
	}

}
