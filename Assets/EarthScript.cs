using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour {
	GameManager gm;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag == "Player"){
			Debug.Log ("Osu Maahan");
			gm.Victory ();

		}
	}
}
