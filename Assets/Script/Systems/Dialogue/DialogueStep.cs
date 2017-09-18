using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable, CreateAssetMenu(fileName="New Dialogue Step", menuName = "Pokemon_H/Dialogue/Dialogue Step", order=0)]
public class DialogueStep : ScriptableObject {

	public BaseCharacter person;
	public List<DialogueStep> connectedSteps;
	public string contextTag;
	public string leftBranchTag;
	public string middleBranchTag;
	public string rightBranchTag;
	public List<string> dialogueText;
	public Sprite background;
	public Sprite character;
	public Criteria criteria;

}
