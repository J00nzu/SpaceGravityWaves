using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalerScript : MonoBehaviour {

	public static float NORMAL_ASPECT_RATIO = 16/9f;

	void Start () {
		UIScript uis = FindObjectOfType<UIScript> ();
		if (uis == null)
			return;

		float newAspectRatio = uis.getWidth()/uis.getHeight();

		float aspectRatioDivided = newAspectRatio / NORMAL_ASPECT_RATIO;
		Debug.Log ("normalAspect= " + NORMAL_ASPECT_RATIO + " newAspect= " + newAspectRatio + " aspectDivided= " + aspectRatioDivided);
		this.GetComponent<Camera> ().orthographicSize /= aspectRatioDivided;
	}

}
