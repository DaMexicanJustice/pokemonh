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
		Object[] nodes = Resources.LoadAll<DialogueStepNode>("NUnit");
		Assert.True (nodes.Length > 0);
	}

	[Test]
	public void DialogueStepTestLillieNodes() {
		// Use the Assert class to test conditions.
		string expectedResult = "Lillie";
		Object[] nodes = Resources.LoadAll<DialogueStepNode>("NUnit");
		foreach (Node n in nodes) {
			Assert.True ((n as DialogueStepNode).person.characterName.Equals (expectedResult));
		}
	}

	[Test]
	public void DialogueStepTestConnections() {
		// Use the Assert class to test conditions.
		int expectedResult = 1;
		Object[] nodes = Resources.LoadAll<DialogueStepNode>("NUnit");
		foreach (Node n in nodes) {
			int result = 0;
			DialogueStepNode dsn = n as DialogueStepNode;

			if (!dsn.name.Equals ("Lillie End")) {
				if (dsn.leftNode != null)
					result++;
				if (dsn.middleNode != null)
					result++;
				if (dsn.rightNode != null)
					result++;
				if (result < expectedResult) {
					Assert.Fail ();
				}
			}
		}
	}

}
