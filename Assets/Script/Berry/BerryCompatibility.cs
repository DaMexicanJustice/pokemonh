using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCompatibility : MonoBehaviour {

	private Dictionary<SexPokemon.Taste, Dictionary<Berry.BerryName, int>> compatibilityDict = 
		new Dictionary<SexPokemon.Taste, Dictionary<Berry.BerryName, int>> ();

	private Dictionary<Berry.BerryName, int> subDict = new Dictionary<Berry.BerryName, int>();
	
	public BerryCompatibility() {
		// For bitter
		subDict.Add (Berry.BerryName.RAWST, 3);
		subDict.Add (Berry.BerryName.PECHA, -3);
		subDict.Add (Berry.BerryName.LEPPA, 1);
		subDict.Add (Berry.BerryName.ORAN, 1);
		subDict.Add (Berry.BerryName.SITRUS, 0);
		compatibilityDict.Add (SexPokemon.Taste.BITTER, subDict);

		subDict.Clear ();

		// For Salty
		subDict.Add (Berry.BerryName.RAWST, 1);
		subDict.Add (Berry.BerryName.PECHA, 1);
		subDict.Add (Berry.BerryName.LEPPA, 1);
		subDict.Add (Berry.BerryName.ORAN, 3);
		subDict.Add (Berry.BerryName.SITRUS, -3);
		compatibilityDict.Add (SexPokemon.Taste.SALTY, subDict);

		subDict.Clear ();

		// For Sweet
		subDict.Add (Berry.BerryName.RAWST, -3);
		subDict.Add (Berry.BerryName.PECHA, 3);
		subDict.Add (Berry.BerryName.LEPPA, -1);
		subDict.Add (Berry.BerryName.ORAN, 1);
		subDict.Add (Berry.BerryName.SITRUS, 1);
		compatibilityDict.Add (SexPokemon.Taste.SWEET, subDict);

		subDict.Clear ();

		// For Sour
		subDict.Add (Berry.BerryName.RAWST, 1);
		subDict.Add (Berry.BerryName.PECHA, 1);
		subDict.Add (Berry.BerryName.LEPPA, 0);
		subDict.Add (Berry.BerryName.ORAN, -3);
		subDict.Add (Berry.BerryName.SITRUS, 3);
		compatibilityDict.Add (SexPokemon.Taste.SOUR, subDict);

		subDict.Clear ();

		// For Spicy
		subDict.Add (Berry.BerryName.RAWST, -1);
		subDict.Add (Berry.BerryName.PECHA, -1);
		subDict.Add (Berry.BerryName.LEPPA, 3);
		subDict.Add (Berry.BerryName.ORAN, 1);
		subDict.Add (Berry.BerryName.SITRUS, 0);
		compatibilityDict.Add (SexPokemon.Taste.SPICY, subDict);
	}

	void Start() {
		TestPrint ();
	}

	public int GetAffectionChange(Berry.BerryName flavor, SexPokemon.Taste taste) {
		return compatibilityDict [taste] [flavor];
	}

	public void TestPrint() {
		foreach(SexPokemon.Taste taste in compatibilityDict.Keys) {
			Debug.Log (taste + ": ------------------------\n");
			foreach (Berry.BerryName flavor in compatibilityDict[taste].Keys) {
				Debug.Log (flavor + ": " + compatibilityDict[taste][flavor]);
			}
		}
	}

}