using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidTo : MonoBehaviour {
	AsteroidFrom AstFrom;

	void Start () {
		AstFrom = FindObjectOfType<AsteroidFrom> ();
	}
	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Finish") {

			Debug.Log ("asteroidi kosketti");
			AstFrom.MoveAsteroidBack ();
		}
		}
}
