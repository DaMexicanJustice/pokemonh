using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	public List<TM> tms = new List<TM>();

	public void SortAZ() {
		tms.Sort ();
	}
}
