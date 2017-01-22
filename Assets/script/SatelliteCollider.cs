using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCollider : MonoBehaviour {
	Satellite satelit;
	GameManager GM;
	// Use this for initialization
	void Start () {
		GM = FindObjectOfType<GameManager> ();
		satelit = GetComponent<Satellite> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter2D(Collision2D other){
		satelit.SatelitCount++;
		this.gameObject.SetActive (false);


	}
}
