using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicObject : MonoBehaviour {

	RigidbodyType2D initialType;

	protected Vector3 SavePos;

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
		ParticleSystem exp = GetComponentInChildren<ParticleSystem> ();
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		CircleCollider2D coll = GetComponent<CircleCollider2D> ();
		PolygonCollider2D coll2 = GetComponent<PolygonCollider2D> ();
		Rigidbody2D rig = GetComponent<Rigidbody2D> ();


		if (exp != null) {
			exp.Play();
		}

		if (sprite != null) {
			sprite.enabled = false;
		}

		if (coll != null) {
			coll.enabled = false;
		}
		if (coll2 != null) {
			coll2.enabled = false;
		}

		if (rig != null) {
			rig.velocity = Vector2.zero;
		}

	}
}
