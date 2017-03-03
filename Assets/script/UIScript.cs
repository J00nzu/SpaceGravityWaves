using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	Image left, right;
	Image tutorial, levelName, victory, introImg;
	InputHandler input;
	GameManager GM;

	int LRmaxAlpha = 150;
	int LRcurrAlpha = 0;
	int AlphaDecrease = 10;

	float waitTime = 1.0f/30;

	public Button spacebar;

	IntroScript intro;

	// Use this for initialization
	void Start () {
		left = transform.Find ("Left").GetComponent<Image>();
		right = transform.Find ("Right").GetComponent<Image>();

		tutorial = transform.Find ("Tutorial").GetComponent<Image>();
		levelName = transform.Find ("LevelName").GetComponent<Image>();
		victory = transform.Find ("LevelVictory").GetComponent<Image>();


		left.color = new Color (0, 0, 0, (float)LRcurrAlpha/255.0f);
		right.color = new Color (0, 0, 0, (float)LRcurrAlpha/255.0f);



		input = FindObjectOfType<InputHandler> ();
		GM = FindObjectOfType<GameManager> ();
		intro = FindObjectOfType<IntroScript> ();

		if (intro != null) {
			introImg = transform.Find ("IntroImage").GetComponent<Image>();
			StartCoroutine ("IntroStart");
		} else {
			StartCoroutine ("FadeLevelName");
		}

		StartCoroutine ("FadeSides");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HideTutorial(){
		StartCoroutine ("FadeTutorial");
	}

	public bool HasTutorial(){
		return tutorial != null;
	}

	public void ShowVictory(){
		StartCoroutine ("FadeVictory");
	}


	public void NotifyIntroEnd(){
		StartCoroutine ("IntroWait");
	}

	public void NotifyIntroPress(){
		StartCoroutine ("IntroEnd");
	}


	IEnumerator FadeLevelName(){
		input.DeactivateInput ();
		levelName.color = new Color (1, 1, 1, 1);

		yield return new WaitForSeconds (2);
		float a = 1;

		while (a > 0) {
			levelName.color = new Color (1, 1, 1, a);
			a -= 0.01f;
			yield return null;
		}
		levelName.enabled = false;
		input.ActivateInput ();
	}

	IEnumerator FadeTutorial(){
		float a = 1;

		while (a > 0) {
			tutorial.color = new Color (1, 1, 1, a);
			a -= 0.05f;
			yield return null;
		}

		tutorial.enabled = false;

	}

	IEnumerator FadeVictory(){
		float a = 0;

		yield return new WaitForSeconds (2);

		while (a < 1) {
			victory.color = new Color (1, 1, 1, a);
			a += 0.02f;
			if (a > 1)
				a = 1;
			yield return null;
		}
	}


	IEnumerator FadeSides(){
		while (true) {
			if (input.IsPlanetDragged ()) {
				if (LRcurrAlpha < LRmaxAlpha) {
					LRcurrAlpha+=AlphaDecrease;
					left.color = new Color (0, 0, 0, (float)LRcurrAlpha/255.0f);
					right.color = new Color (0, 0, 0, (float)LRcurrAlpha/255.0f);
				}
			} else {
				if (LRcurrAlpha > 0) {
					LRcurrAlpha-=AlphaDecrease;
					left.color = new Color (0, 0, 0, (float)LRcurrAlpha/255.0f);
					right.color = new Color (0, 0, 0, (float)LRcurrAlpha/255.0f);
				}
			}

			yield return null;
		}
	}

	IEnumerator IntroStart(){
		levelName.enabled = false;
		tutorial.enabled = false;
		introImg.enabled = false;
		input.DeactivateInput ();

		return null;
	}

	IEnumerator IntroWait(){

		yield return new WaitForSeconds (2);


		float a = 0;
		introImg.enabled = true;


		while (a < 1) {
			introImg.color = new Color (1, 1, 1, a);
			a += 0.02f;
			if (a > 1)
				a = 1;
			yield return null;
		}
	}

	IEnumerator IntroEnd(){

		levelName.enabled = true;

		float a = 1;
		while (a > 0) {
			introImg.color = new Color (1, 1, 1, a);
			levelName.color = new Color (1, 1, 1, 1-a);

			a -= 0.02f;
			if (a < 0)
				a = 0;
			yield return null;
		}
		introImg.enabled = false;

		StartCoroutine ("FadeLevelName");
		tutorial.enabled = true;
	}

}
