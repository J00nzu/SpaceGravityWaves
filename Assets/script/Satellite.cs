using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : DynamicObject {

	SatelliteCollider shield;
	List<GameObject> explo = new List<GameObject> ();
	SpriteRenderer biimiSprite;
	GameObject biimi;

	static float angleOffset = 32f;
	static float thisRadius = 0.7f;
	static float shieldRadius = 0.9f;
	static float beamSpriteSizeXUnits = 2f;

	void Start(){

		for (int i = 0; i < 3; i++) {
			Transform ct = transform.Find ("ex" + i);
			if (ct != null) {
				explo.Add (ct.gameObject);
				ct.gameObject.SetActive (false);
			}
		}

		shield = FindObjectOfType<SatelliteCollider> ();

		Transform bgo = transform.Find ("beam");
		if(bgo != null){
			biimi = bgo.gameObject;
			biimiSprite = bgo.GetComponent<SpriteRenderer> ();
		}

		UpdateBeam ();
	}

	void Update(){
		if(biimiSprite != null){
			biimiSprite.color = new Color (1, 1, 1, 0.25f + Mathf.PingPong (Time.realtimeSinceStartup/3.0f, 0.75f));
		}
		
	}

	protected override void PlayPressed(){
		
	}

	void UpdateBeam(){
		if (biimi == null || biimiSprite == null || shield == null) {
			return;
		}
		Vector3 distVec = (shield.transform.position - this.transform.position);
		Vector3 dirVec = distVec.normalized;
		float fullDistance = distVec.magnitude;

		this.transform.right = dirVec;
		this.transform.Rotate (new Vector3(0,0,angleOffset));
		this.transform.rotation = Quaternion.Euler (new Vector3 (0,0,
			this.transform.rotation.eulerAngles.z));

		//Vector2 biimiSpriteSize = new Vector2(biimiSprite.bounds.size.x / biimi.transform.localScale.x, biimiSprite.bounds.size.y / biimi.transform.localScale.y);

		biimi.transform.position = this.transform.position + (dirVec * thisRadius);

		float reducedDistance = fullDistance - thisRadius - shieldRadius;

		Debug.Log ("Satellite BeamInfo:  fullDistance = " + fullDistance + " reducedDistance = " + reducedDistance + " beamSpriteSizeXUnits= " + beamSpriteSizeXUnits);

		biimi.transform.localScale = new Vector3 (reducedDistance / (beamSpriteSizeXUnits), 1, 1);

		biimi.transform.right = dirVec;
		biimi.transform.rotation = Quaternion.Euler (new Vector3 (0,0,
			biimi.transform.rotation.eulerAngles.z));
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			if (shield != null) {
				shield.NotifySatelliteDead ();
			}

			Explode ();
		}
	}

	public override void ResetPlay() {
		Rigidbody2D rig = GetComponent<Rigidbody2D> ();
		Alive = true;

		if (rig != null) {
			rig.velocity = Vector2.zero;
			rig.angularVelocity = 0;
			this.transform.rotation = Quaternion.Euler (Vector3.zero);
		}

		this.transform.position = SavePos;

		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		CircleCollider2D coll = GetComponent<CircleCollider2D> ();
		PolygonCollider2D coll2 = GetComponent<PolygonCollider2D> ();

		if (sprite != null) {
			sprite.enabled = true;
		}

		if (coll != null) {
			coll.enabled = true;
		}
		if (coll2 != null) {
			coll2.enabled = true;
		}

		if (biimiSprite != null) {
			biimiSprite.enabled = true;
		}
	}

	public override void Explode(){
		ExplosionSound ();

		ParticleSystem exp = GetComponentInChildren<ParticleSystem> ();
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		PolygonCollider2D coll = GetComponent<PolygonCollider2D> ();


		if (exp != null) {
			exp.Play();

		}

		if (sprite != null) {
			sprite.enabled = false;
		}

		if (coll != null) {
			coll.enabled = false;
		}

		if (biimiSprite != null) {
			biimiSprite.enabled = false;
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


	new protected void ExplosionSound(){
		JukeboxScript.PlayExplosion3 ();
	}
}


