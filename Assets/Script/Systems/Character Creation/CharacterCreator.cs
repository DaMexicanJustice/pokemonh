using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour {

	public Text blueText; 
	public List<string> tutorialText;
	public Button btn;
	public Text pName;
	public Text pAge;
	public Text pGender;

	int idx = 0;
	int selector = 0;
	public float textSpeed;

	public Pokemon flareon;
	public Pokemon vaporeon;
	public Pokemon jolteon;
	public StarterSelector starterIdx;

	// Use this for initialization
	void Awake () {
		Init ();
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
			btn.onClick.AddListener ( () =>  Submit() );
		}
	}

	public void Init() {
		tutorialText = new List<string> ();
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
		InvokeRepeating ("TypeText", 0, textSpeed);
	}

	public void Submit() {
		if (!fieldsAreEmpty ()) {
			CharacterCreationUI.instance.Hide ();
			OverworldUI.instance.Show ();
			Player player = GameObject.FindGameObjectWithTag ("Player").AddComponent<Player> ();
			player.name = pName.text;
			player.age = pAge.text;
			player.gender = pGender.text;
			int idx = starterIdx.selection;
			switch (idx) {
			case 0:
				player.pokemon = flareon;
				break;
			case 1:
				player.pokemon = vaporeon;
				break;
			case 2:
				player.pokemon = jolteon;
				break;
			}

		} else {
			blueText.text = "Thank you! Huh, wait. You seem to have sent me empty data. Please try again ---->";
		}
	}

	private bool fieldsAreEmpty() {
		return (pName.text == "Name" || pAge.text == "Age" || pGender.text == "Gender");
	}

}
