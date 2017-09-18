using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	public List<TM> tms = new List<TM>();
	public Dictionary<Berry, int> berries = new Dictionary<Berry, int>();

	public void SortAZ(List<Object> l) {
		l.Sort ();
	}
}
