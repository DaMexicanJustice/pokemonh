using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour {

	private List<string> tutorialText;
	public Text blueText; 
	public Button btn;
	int idx = 0;
	int selector = 0;

	// Use this for initialization
	void Awake () {
		tutorialText = new List<string> ();
		Init ();
		InvokeRepeating ("TypeText", 0, 0.07f);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void TypeText() {
		if (idx < tutorialText [selector].Length) {
			blueText.text += tutorialText [selector] [idx];
			idx++;
		} 
	}

	public void NextText() {
		blueText.text = "";
		idx = 0;
		if (selector < tutorialText.Count - 1) {
			selector++;
		} else {
			CancelInvoke ();
			btn.GetComponentInChildren<Text>().text = "Send";
		}
	}

	public void Init() {
		tutorialText.Add ("Hello! Sorry to keep you waiting! Welcome to the world of Pokémon!");
		tutorialText.Add ("My name is DMJ. People call me the Pokémon pimp");
		tutorialText.Add ("You are about to step into the world of Pokémon H.");
		tutorialText.Add ("A world the very way you want it. Where you can battle Pokémon and win more than just Pokédollars");
		tutorialText.Add ("In this world, you can make all the trainers reward you how you want!");
		tutorialText.Add ("Oh-- How I remember defeating Clair and wanting to put her in her place... Well, now I can and so can you!");
		tutorialText.Add ("This world has trainers of all ages for you to 'discipline'");
		tutorialText.Add ("Yes, even the kids. I decided to stay true to the original characters in terms of appearance.");
		tutorialText.Add ("That is a Loli-Warning btw. So if you don't stick your dick in 18 and below, time to abandon ship!");
		tutorialText.Add ("Of course, I trust you are here to become the new Pokémon champion!");
		tutorialText.Add ("-- And yes, you can breed Pokémon too ;D");
		tutorialText.Add ("Well what are you waiting for? Gotta fuck em' all!");
		tutorialText.Add ("Ahem. So, are you a boy or a girl? See the green screen on your right? Send me your info will you?");
	}
}
