using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Town", menuName = "Pokemon_H/Location/Town", order=0)]
public class Town : ScriptableObject {

	public string townName;
	public string description;
	public Sprite background;
	public List<Spot> spots;

}
