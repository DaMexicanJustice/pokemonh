using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

[System.Serializable, CreateAssetMenu(fileName="New NPC", menuName = "Pokemon_H/NPC/NPC", order=0)]
public class BaseCharacter : ScriptableObject {

	public string characterName;
	[Range(5,50)]
	public int age;
	public enum Gender {
		MALE, FEMALE, INTERSEX
	}
	public Gender gender;
	public NodeCanvas dialogueTree;

}
