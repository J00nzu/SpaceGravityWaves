using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour {

	public int deadpoint = 0;
	public int earthpoint = 0;
	public float year = 2017;


	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad (this);
		// if scene already has one Statistics thingy
		if (FindObjectsOfType (GetType()).Length > 1) {
			Destroy (gameObject);
		}

		deadpoint = GameSettings.Get ().deadPoints;
		earthpoint = GameSettings.Get ().earthPoints;
		year = GameSettings.Get ().year;
	}
	
	// Update is called once per frame
	void Update(){
		
	}

	void ResetYear(){
		year = 2017;
	}

}
