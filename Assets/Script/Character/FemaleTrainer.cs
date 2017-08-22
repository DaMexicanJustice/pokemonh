using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Female Trainer", menuName = "Pokemon_H/Trainer/Female Trainer", order=0)]
public class FemaleTrainer : Trainer {

	public enum Personality {
		INNOCENT, NAUGHTY, TESTY
	}
	public Personality personality;

}
