using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleFrom : MonoBehaviour {
	public BlackHoleTo To;
	// Use this for initialization
	void Start () {
		//To = FindObjectOfType<BlackHoleTo> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			MeteorScript met = other.transform.GetComponent<MeteorScript> ();
			if (met!=null) {
				met.transform.position = To.transform.position;
				JukeboxScript.PlayBlackHole ();
			}
		}	



	}
}
