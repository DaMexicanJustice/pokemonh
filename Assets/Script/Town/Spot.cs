using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Spot", menuName = "Pokemon_H/Location/Spot", order=1)]
public class Spot : ScriptableObject {

	public string spotName;
	public string description;
	public Sprite background;
	public List<BaseCharacter> characters;

}
