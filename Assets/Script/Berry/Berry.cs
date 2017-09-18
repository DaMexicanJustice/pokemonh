using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Berry", menuName = "Pokemon_H/Inventory/Berry", order=1)]
public class Berry : ScriptableObject {

	public enum BerryName {
		SITRUS, PECHA, ORAN, LEPPA, RAWST
	}
	public BerryName berryName;

}
