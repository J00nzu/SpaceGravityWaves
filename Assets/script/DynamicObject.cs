using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicObject : MonoBehaviour {

	RigidbodyType2D initialType;

	public void StartPlay() {
		Rigidbody2D rig = GetComponent<Rigidbody2D> ();
		if (rig != null) {
			rig.bodyType = initialType;
		}
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
		//TODO amir do your thing
	}

	protected abstract void PlayPressed ();
}
