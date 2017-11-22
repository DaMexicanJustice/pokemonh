using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NodeEditorFramework;

public class EntityTest {

	[Test]
	public void EntityTestPokemonCreate() {
		Pokemon pokemon = ScriptableObject.CreateInstance<Pokemon> ();
		Assert.NotNull (pokemon);
	}

	[Test]
	public void EntityTestMoveCreateAndAdjust() {
		Move move =  ScriptableObject.CreateInstance<Move> ();
		move.damage = 10;
		move.moveName = "Test Move";
		move.type = Move.Type.DRAGON;
		string str = move.damage + move.moveName + move.type;
		int expectedResult = 17;
		Assert.True (str.Length == expectedResult);
	}

	[Test]
	public void EntityTestMoveCreateBaseCharacter() {
		BaseCharacter bc = ScriptableObject.CreateInstance<BaseCharacter> ();
		Assert.NotNull (bc);
	}

	[Test]
	public void EntityTestMoveCreateBaseCharacterAndAdjust() {
		BaseCharacter bc = ScriptableObject.CreateInstance<BaseCharacter> ();
		bc.gender = BaseCharacter.Gender.FEMALE;
		Assert.NotNull (bc.gender);
	}

	[Test]
	public void EntityTestCompareEntities() {
		BaseCharacter bc = ScriptableObject.CreateInstance<BaseCharacter> ();
		bc.characterName = "Test Character One";
		BaseCharacter bc2 = ScriptableObject.CreateInstance<BaseCharacter>();
		bc2.characterName = "Test Character Two";
		Assert.AreNotEqual (bc, bc2);
	}

}
