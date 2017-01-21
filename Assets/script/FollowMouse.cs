using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

	public float divisor = 10;

	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition) / divisor;
		transform.position = new Vector3(pos.x, pos.y, transform.position.z);
	}
}
