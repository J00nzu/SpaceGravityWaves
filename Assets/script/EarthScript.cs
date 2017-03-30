using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour {
	GameManager gm;


	List<GameObject> explo = new List<GameObject> ();
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameManager> ();
	
		for (int i = 0; i < 6; i++) {
			Transform ct = transform.Find ("ex" + i);
			if (ct != null) {
				explo.Add (ct.gameObject);
				ct.gameObject.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag == "Player"){
			MeteorScript met = other.transform.GetComponent<MeteorScript> ();
			if (met!=null) {
				met.Explode ();
				this.Explode ();
				gm.Victory ();
			}
		}
	}

	public void Explode(){
		ExplosionSound ();

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

		foreach(GameObject go in explo){
			go.SetActive (true);
			Rigidbody2D goRig = go.GetComponent<Rigidbody2D> ();
			Vector2 dir = go.transform.localPosition;
			dir = dir.normalized;
			go.transform.parent = this.transform.parent;


			goRig.velocity =  (dir * 0.5f);
			goRig.angularVelocity = Random.value * 180 - 90;
		}
	}

	new protected void ExplosionSound(){
		JukeboxScript.PlayExplosion1 ();
	}
}
