using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour {




	GameManager GM;

	void Start(){
		GM = FindObjectOfType<GameManager> ();
	}
		

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag == "Player"){
			MeteorScript met = other.transform.GetComponent<MeteorScript> ();
			if (met!=null) {
				met.Explode ();
				GM.Dead ();
			}
		}
	}
}
