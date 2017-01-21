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

	public void Explode(){
		ParticleSystem exp = GetComponentInChildren<ParticleSystem> ();
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		CircleCollider2D coll = GetComponent<CircleCollider2D> ();


		if (exp != null) {
			exp.Play();
		}

		if (sprite != null) {
			sprite.enabled = false;
		}

		if (coll != null) {
			coll.enabled = false;
		}
	}
}
