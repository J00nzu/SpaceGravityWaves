using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : DynamicObject {

	List<GameObject> explo = new List<GameObject> ();

	void Start(){

		for (int i = 0; i < 3; i++) {
			Transform ct = transform.Find ("ex" + i);
			if (ct != null) {
				explo.Add (ct.gameObject);
				ct.gameObject.SetActive (false);
			}
		}
	}

	protected override void PlayPressed(){
		
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			SatelliteCollider sc = FindObjectOfType<SatelliteCollider> ();
			if (sc != null) {
				sc.NotifySatelliteDead ();
			}

			Explode ();
		}
	}

	new void Explode(){
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

		List<GameObject> explo2 = new List<GameObject>();

		foreach (GameObject go in explo) {
			GameObject go2 = (GameObject)Instantiate (go);
			go2.transform.parent = go.transform.parent;
			go2.transform.position = go.transform.position;

			explo2.Add (go2);
		}


		foreach(GameObject go in explo){
			go.SetActive (true);
			Rigidbody2D goRig = go.GetComponent<Rigidbody2D> ();
			Vector2 dir = go.transform.localPosition;
			dir = dir.normalized;
			go.transform.parent = this.transform.parent;

			Vector3 randVec = (Random.insideUnitSphere * 0.2f);

			goRig.velocity = (dir * 0.5f) + new Vector2 (randVec.x, randVec.y);
			goRig.angularVelocity = Random.value * 180 - 90;
		}

		explo = explo2;

	}
}


