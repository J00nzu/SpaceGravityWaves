using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicObject : MonoBehaviour {

	RigidbodyType2D initialType;

	protected Vector3 SavePos;
	protected bool Alive = true;

	public void StartPlay() {
		Rigidbody2D rig = GetComponent<Rigidbody2D> ();
		if (rig != null) {
			rig.bodyType = initialType;

		}
		SavePos = this.transform.position;
		PlayPressed ();

	}

	public void InitPlay() {
		Rigidbody2D rig = GetComponent<Rigidbody2D> ();
		if (rig != null) {
			initialType = rig.bodyType;
			rig.bodyType = RigidbodyType2D.Static;
		}

	}

	public void ResetPlay() {
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
	}

	protected abstract void PlayPressed ();

	public void Explode(){
		ExplosionSound ();

		ParticleSystem[] exp = GetComponentsInChildren<ParticleSystem> ();
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		CircleCollider2D cColl = GetComponent<CircleCollider2D> ();
		PolygonCollider2D pColl = GetComponent<PolygonCollider2D> ();
		Rigidbody2D rig = GetComponent<Rigidbody2D> ();


		if (exp != null) {
			foreach(ParticleSystem psys in exp)
				psys.Play();
		}

		if (sprite != null) {
			sprite.enabled = false;
		}

		if (cColl != null) {
			cColl.enabled = false;
		}
		if (pColl != null) {
			pColl.enabled = false;
		}

		if (rig != null) {
			rig.velocity = Vector2.zero;
		}

		Alive = false;
	}

	public bool isAlive(){
		return Alive;
	}

	protected void ExplosionSound(){
		JukeboxScript.PlayExplosion2 ();
	}
}
