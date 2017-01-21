using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	Image left, right;
	InputHandler input;
	GameManager GM;

	int LRmaxAlpha = 150;
	int LRcurrAlpha = 150;
	int AlphaDecrease = 10;

	float waitTime = 1.0f/30;



	// Use this for initialization
	void Start () {
		left = transform.Find ("Left").GetComponent<Image>();
		right = transform.Find ("Right").GetComponent<Image>();

		left.color = new Color (0, 0, 0, (float)LRcurrAlpha/255.0f);
		right.color = new Color (0, 0, 0, (float)LRcurrAlpha/255.0f);

		input = FindObjectOfType<InputHandler> ();
		GM = FindObjectOfType<GameManager> ();

		StartCoroutine ("FadeColor");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FadeColor(){
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
}
