using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

	public float divisor = 10;

	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android
		   || Application.platform == RuntimePlatform.IPhonePlayer) {

			transform.position = new Vector3 (Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
		} else {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition) / divisor;
			transform.position = new Vector3(pos.x, pos.y, transform.position.z);
		}
	}
}
