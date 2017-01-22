using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleFrom : MonoBehaviour {
	BlackHoleTo To;
	MeteorScript meteorit;
	// Use this for initialization
	void Start () {
		To = FindObjectOfType<BlackHoleTo> ();
		meteorit = FindObjectOfType<MeteorScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag == "Player"){
			Debug.Log ("Osu Planettaan");
			meteorit.transform.position = To.transform.position;

		}	



	}
}
