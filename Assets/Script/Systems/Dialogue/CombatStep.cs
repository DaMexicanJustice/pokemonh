using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Combat Step", menuName = "Pokemon_H/Dialogue/Combat Step", order=1)]
public class CombatStep : DialogueStep {

	public bool playerWonCombat;
	public FemaleTrainer ft;

}
