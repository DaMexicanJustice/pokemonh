using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Trainer", menuName = "Pokemon_H/Trainer/Trainer", order=1)]
public class Trainer : BaseCharacter {

	public List<Pokemon> pokemon;

}
