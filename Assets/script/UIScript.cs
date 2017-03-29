using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	Image left, right;
	Image tutorial, levelName, victory, introImg;
	InputHandler input;
	GameManager GM;
	MenuPhone menp;

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
		menp = FindObjectOfType<MenuPhone> ();
		GameObject introImgObject = GameObject.Find ("IntroImage");
		if (introImgObject != null) {
			introImg = introImgObject.GetComponent<Image> ();
		}

		if (intro != null) {
			StartCoroutine ("IntroStart");
		} else {
			
			if(introImg!=null){
				introImg.enabled = false;
			}

			StartCoroutine ("FadeLevelName");
		}

		StartCoroutine ("FadeSides");

		if (!tutorial.gameObject.activeInHierarchy) {
			input.firstPress = false;
		}

		if (left != null && right != null) {
			ScaleLeftAndRight ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		ScaleLeftAndRight ();
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
		JukeboxScript.PlayClick ();
		GM.Introing = false;
	}

	public void ScaleLeftAndRight(){
		if (left == null || right == null || GM == null
			|| left.color.a == 0 || right.color.a == 0) {
			return;
		}

		left.rectTransform.anchorMin = new Vector2 (0, 0);
		left.rectTransform.anchorMax = new Vector2 (0, 0);

		right.rectTransform.anchorMin = new Vector2 (0, 0);
		right.rectTransform.anchorMax = new Vector2 (0, 0);

		left.rectTransform.offsetMin = new Vector2 (-10, 0);
		left.rectTransform.offsetMax = new Vector2(
			(Camera.main.WorldToScreenPoint(new Vector3(GM.LevelLeftBound,0,0)).x/Screen.width)*getWidth(),
			getHeight()
		);

		float orthoWidth = Camera.main.orthographicSize * getWidth() /getHeight();


		right.rectTransform.offsetMin = new Vector2 (
			(Camera.main.WorldToScreenPoint(new Vector3(GM.LevelRightBound,0,0)).x/Screen.width)*getWidth(),
			0
		);
		right.rectTransform.offsetMax = new Vector2(
			getWidth(),
			getHeight()
		);

	}


	IEnumerator FadeLevelName(){
		if (levelName != null) {
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
		} else {
			StartCoroutine ("FadeInTutorial");
		}
		input.ActivateInput ();
		yield return null;
	}

	IEnumerator FadeInTutorial(){
		if (tutorial != null) {
			float a = 0;
			tutorial.enabled = true;

			while (a < 1) {
				tutorial.color = new Color (1, 1, 1, a);
				a += 0.02f;
				if (a > 1)
					a = 1;
				yield return null;
			}
		}

		yield return null;
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
		tutorial.enabled = false;
		introImg.enabled = false;
		if (menp != null) {
			menp.HideMenuControls ();
		}
		input.DeactivateInput ();

		float a = 1;

		while (a > 0) {
			levelName.color = new Color (1, 1, 1, a);
			a -= 0.01f;
			yield return null;
		}
		levelName.enabled = false;

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
		if (menp != null) {
			menp.ShowMenuControls ();
		}
		tutorial.enabled = true;

		float a = 1;
		while (a > 0) {
			introImg.color = new Color (1, 1, 1, a);
			tutorial.color = new Color (1, 1, 1, 1-a);

			a -= 0.02f;
			if (a < 0)
				a = 0;
			yield return null;
		}

		introImg.enabled = false;
		tutorial.enabled = true;

		input.ActivateInput ();
	}




	//Width & Height

	Vector2 WidthHeight;
	RectTransform recto;

	//A bit inefficient i'd say..
	public Vector2 getWidthHeight(){
		if (recto==null) {
			recto = GetComponent<RectTransform> ();
		}

		WidthHeight = new Vector2 (recto.rect.width, recto.rect.height);
		return WidthHeight;
	}

	public float getWidth(){
		return getWidthHeight ().x;
	}

	public float getHeight(){
		return getWidthHeight ().y;
	}

}
