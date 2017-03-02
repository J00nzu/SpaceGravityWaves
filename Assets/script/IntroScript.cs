using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour {

	public float startOrthoSize;
	public float targetOrthoSize;
	public float orthoAccerlation;
	public float orthoDampen;
	public float backUpMultiplier;
	public int maxBounceTimes;


	public float orthoVel = 9;
	float orthoSize = 0;

	int currBounceTimes = 0;
	bool isBounce = false;

	Camera cam;
	UIScript UI;
	GameManager GM;
	GameObject bg;

	bool running = true;

	void Start () {
		cam = Camera.main;
		UI = FindObjectOfType<UIScript> ();
		GM = FindObjectOfType<GameManager> ();
		bg = GameObject.Find ("Background");

		cam.orthographicSize = startOrthoSize;
		orthoSize = startOrthoSize;

		GM.Introing = true;
	}
	
	void Update () {
		if (running) {
			if (orthoSize < targetOrthoSize) {
				if (orthoVel > 1) {
					orthoVel *= orthoDampen;
				}
				orthoVel -= (orthoAccerlation * Time.deltaTime * backUpMultiplier) * (orthoVel > 1 ? orthoVel : 1);
			} else {
				orthoVel += orthoAccerlation * Time.deltaTime;
			}


			if (GameSettings.Get ().progress >= 2 && Input.GetMouseButton (0)) {
				stopRun ();
				orthoVel = 400 * (orthoSize/startOrthoSize);
			} else {
				orthoSize -= orthoVel * Time.deltaTime;
			}

			if (!isBounce) {
				if (Mathf.Abs (orthoSize - targetOrthoSize) < 0.3f) {
					isBounce = true;
					currBounceTimes++;
				}
			} else {
				if (Mathf.Abs (orthoSize - targetOrthoSize) > 0.3f) {
					isBounce = false;
				}
			}

			if (currBounceTimes >= maxBounceTimes) {
				stopRun ();
			}
		} else {
			orthoSize += (targetOrthoSize - orthoSize) * Time.deltaTime;
			orthoSize -= orthoVel * Time.deltaTime;
			Mathf.SmoothDamp (orthoVel, 0, ref orthoVel, 2f);
		}

		float bgSize = orthoSize / targetOrthoSize;
		bg.transform.localScale = new Vector3 (bgSize, bgSize, 1);

		cam.orthographicSize = orthoSize;

	}

	void stopRun(){
		running = false;
		if (UI != null) {
			UI.NotifyIntroEnd ();
		}
		StartCoroutine ("WaitAWhile");
	}

	IEnumerator WaitAWhile(){
		yield return new WaitForSeconds(10);

		cam.orthographicSize = targetOrthoSize;
		bg.transform.localScale = new Vector3 (1, 1, 1);
		this.enabled = false;
		GM.Introing = false;

	}
}
