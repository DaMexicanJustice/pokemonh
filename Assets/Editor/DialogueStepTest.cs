using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NodeEditorFramework;

public class DialogueStepTest {

	[Test]
	public void DialogueStepTestLookUpNodes() {
		// Use the Assert class to test conditions.
		Object[] nodes = Resources.LoadAll<DialogueStep>("NUnit");
		Assert.True (nodes.Length > 0);
	}

	[Test]
	public void DialogueStepTestLillieNodes() {
		// Use the Assert class to test conditions.
		string expectedResult = "Lillie";
		Object[] steps = Resources.LoadAll<DialogueStep>("NUnit");
		foreach (DialogueStep ds in steps) {
			Assert.True (ds.person.characterName.Equals (expectedResult));
		}
	}

	[Test]
	public void DialogueStepTestConnections() {
		// Use the Assert class to test conditions.
		int expectedResult = 1;
		Object[] steps = Resources.LoadAll<DialogueStep>("NUnit");
		foreach (DialogueStep ds in steps) {
			int result = 0;

			if (!ds.name.Equals ("Lillie End")) {
				if (ds.leftNode != null)
					result++;
				if (ds.middleNode != null)
					result++;
				if (ds.rightNode != null)
					result++;
				if (result < expectedResult) {
					Assert.Fail ();
				}
			}
		}
	}

}
