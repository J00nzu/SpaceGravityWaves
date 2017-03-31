using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCollider : MonoBehaviour {
	int satellitesLeft;
	int initialSatellites;
	GameManager GM;
	SpriteRenderer sprite;
	CircleCollider2D coll;

	public bool isAlive = true;

	// Use this for initialization
	void Start () {
		GM = FindObjectOfType<GameManager> ();
		satellitesLeft = FindObjectsOfType<Satellite> ().Length;
		initialSatellites = satellitesLeft;
		sprite = GetComponent<SpriteRenderer> ();
		coll = GetComponent<CircleCollider2D> ();

		isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NotifySatelliteDead(){
		Debug.Log (satellitesLeft);
		satellitesLeft--;
		if (satellitesLeft == 0) {
			sprite.enabled = false;
			coll.enabled = false;
			isAlive = false;
		}
	}

	public void Reset(){
		satellitesLeft = initialSatellites;
		sprite.enabled = true;
		coll.enabled = true;
		isAlive = true;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player")) {
			MeteorScript met = other.transform.GetComponent<MeteorScript> ();
			if (met!=null) {
				met.Explode ();
				GM.Dead ();
			}
		}
	}
}
