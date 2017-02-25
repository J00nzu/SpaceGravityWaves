using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistic : MonoBehaviour {

	public int deadpoint = 0;
	public int earthpoint = 0;
	public float year = 2017;

	GameManager GM;
	Text yearCounter;

	float yearsPerSecond = 2f;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		// if scene already has one Statistics thingy
		if (FindObjectsOfType (GetType()).Length > 1) {
			Destroy (gameObject);
		}

		GM = FindObjectOfType<GameManager> ();
		GameObject GO = GameObject.Find ("YearCounter");
		if (GO != null) {
			yearCounter = GO.GetComponent<Text> ();
		}
		deadpoint = GameSettings.Get ().deadPoints;
		earthpoint = GameSettings.Get ().earthPoints;
	}
	
	// Update is called once per frame
	void Update(){
		if (GM.playing && !GM.NextLevel) {
			year += Time.deltaTime * yearsPerSecond;
		}
		if (yearCounter != null) {
			yearCounter.text = "" + ((int)year);
		} else {
			GameObject GO = GameObject.Find ("YearCounter");
			if (GO != null) {
				yearCounter = GO.GetComponent<Text> ();
			}
		}
	}

	public void ResetYear(){
		year = 2017;
	}

}
