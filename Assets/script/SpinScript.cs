using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour {

	public float speed = -1;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (0, 0, speed));
	}
}
