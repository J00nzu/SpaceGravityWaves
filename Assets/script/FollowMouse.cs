using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

	public float divisor = 10;
	float smoothFactor = 0.5f;
	Vector3 targetPos;
	float AndroidXScale, AndroidYScale;

	const float AndroidMultiplier = 20;

	void Start(){
		
	}

	// Update is called once per frame
	void Update () {
		if (Application.isMobilePlatform) {
			AndroidYScale = Camera.main.orthographicSize;
			AndroidXScale = AndroidYScale * (Screen.width / Screen.height);

			targetPos = (new Vector3 (Input.acceleration.x*AndroidXScale, Input.acceleration.y*AndroidYScale, transform.position.z) / divisor) * AndroidMultiplier;
		} else {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition) / divisor;
			targetPos = new Vector3(pos.x, pos.y, transform.position.z);
		}

	}

	void FixedUpdate(){
		transform.position = Vector3.Lerp (transform.position, targetPos, 0.1f);
	}
}
