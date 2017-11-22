using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NodeEditorFramework;

public class CombatTest : MonoBehaviour {

	[Test]
	public void CombatTestPokemonAdjustHealth() {
		Move move = ScriptableObject.CreateInstance<Move> ();
		move.damage = 10;
		Pokemon pokemon = ScriptableObject.CreateInstance<Pokemon> ();
		pokemon.maxHP = 100;
		pokemon.curHP = pokemon.maxHP;
		pokemon.TakeDamage (move);
		int expectedResult = 90;
		int result = pokemon.curHP;
		Assert.True (expectedResult == result);
	}

	[Test]
	public void CombatTestPokemonIsKO() {
		Move move = ScriptableObject.CreateInstance<Move> ();
		move.damage = 100;
		Pokemon pokemon = ScriptableObject.CreateInstance<Pokemon> ();
		pokemon.maxHP = 100;
		pokemon.curHP = pokemon.maxHP;
		pokemon.TakeDamage (move);
		Assert.True (pokemon.IsKO());
	}

	[Test]
	public void CombatTestPokemonWeakness() {
		Pokemon pokemon = ScriptableObject.CreateInstance<Pokemon> ();
		pokemon.type = Pokemon.Type.DRAGON;
		Move iceMove = ScriptableObject.CreateInstance<Move> ();
		iceMove.moveName = "Ice Move";
		iceMove.type = Move.Type.ICE;
		Assert.True (pokemon.IsWeakAgainst (iceMove));
	} 

	[Test]
	public void CombatTestPokemonStrong() {
		Pokemon pokemon = ScriptableObject.CreateInstance<Pokemon> ();
		pokemon.type = Pokemon.Type.FIRE;
		Move iceMove = ScriptableObject.CreateInstance<Move> ();
		iceMove.moveName = "Ice Move";
		iceMove.type = Move.Type.ICE;
		Assert.True (pokemon.IsStrongAgainst(iceMove));
	} 

	[Test]
	public void CombatTestUseMove() {
		Pokemon pokemon = ScriptableObject.CreateInstance<Pokemon> ();

		Move iceMove = ScriptableObject.CreateInstance<Move> ();
		iceMove.type = Move.Type.ICE;
		Move fireMove = ScriptableObject.CreateInstance<Move> ();
		fireMove.type = Move.Type.FIRE;
		Move normalMove = ScriptableObject.CreateInstance<Move> ();
		normalMove.type = Move.Type.NORMAL;
		Move darkMove = ScriptableObject.CreateInstance<Move> ();
		darkMove.type = Move.Type.DARK;

		pokemon.moves.Add (iceMove);
		pokemon.moves.Add (fireMove);
		pokemon.moves.Add (normalMove);
		pokemon.moves.Add (darkMove);

		Move expectedResult = normalMove;
		Move result = pokemon.UseMove (2);
		Assert.AreSame (expectedResult, result);

	} 

	[Test]
	public void CombatTestUseRandomMove() {
		Pokemon pokemon = ScriptableObject.CreateInstance<Pokemon> ();

		Move iceMove = ScriptableObject.CreateInstance<Move> ();
		iceMove.type = Move.Type.ICE;
		Move fireMove = ScriptableObject.CreateInstance<Move> ();
		fireMove.type = Move.Type.FIRE;
		Move normalMove = ScriptableObject.CreateInstance<Move> ();
		normalMove.type = Move.Type.NORMAL;
		Move darkMove = ScriptableObject.CreateInstance<Move> ();
		darkMove.type = Move.Type.DARK;

		pokemon.moves.Add (iceMove);
		pokemon.moves.Add (fireMove);
		pokemon.moves.Add (normalMove);
		pokemon.moves.Add (darkMove);

		Move result = pokemon.UseRandomMove ();

		Assert.Contains (result, pokemon.moves);

	} 

}
