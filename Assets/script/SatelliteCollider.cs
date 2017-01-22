using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collision2D other){

		this.gameObject.SetActive (false);


	}
}
