using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

	GameManager GM;
	PlanetScript dragged = null;
	Vector3 offset;
	UIScript UI;
	bool firstPress = true;
	bool inputCooldown, inputActive = true;
	float inputCooldownTime = 0.5f;

	MenuPhone MP;


	public bool IsPlanetDragged(){
		return dragged != null;
	}

	public void DeactivateInput(){
		inputActive = false;
	}
	public void ActivateInput(){
		inputActive = true;
	}
	public bool IsInputActive(){
		return inputActive;
	}




	// Use this for initialization
	void Start () {
		MP = FindObjectOfType<MenuPhone> ();
		UI = FindObjectOfType<UIScript> ();
		GM = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (firstPress) {
			if (UI == null) {
				firstPress = false;
				return;
			}
		}
			/*
			if (Input.GetKeyDown (KeyCode.Space) || !UI.HasTutorial ()) {
				UI.HideTutorial ();
				firstPress = false;
			}

			return;

		} 
*/
		//KEYBOARD INPUT
		if(Input.GetKeyDown(KeyCode.Space)/* && !inputCooldown */){
			SpaceBarButton();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			EscButton();
		}


		//MOUSE INPUT
		Vector3 Mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		float Mx = Mp.x;
		float My = Mp.y;

		if (!GM.playing && IsInputActive()) {

			if (dragged == null) {
					if (Input.GetMouseButton (0)) {
						foreach (PlanetScript p in GM.GetAllPlanets()) {
							float r = p.GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;
							Vector3 off = p.transform.position - new Vector3 (Mx, My, 0);
							float dist = (off).magnitude;

							if (dist < r) {
								this.offset = off;
								dragged = p;
								break;
							}
						}
					}
			} else {
				dragged.MoveTo (new Vector3 (Mx, My, 0) + offset);
				if (!Input.GetMouseButton (0)) {
					dragged = null;
				}
			}

		} else {
			dragged = null;
		}

	}

	void OnDrawGizmos(){
		foreach (PlanetScript p in FindObjectsOfType<PlanetScript>()) {
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (p.transform.position, p.GetComponent<SpriteRenderer> ().sprite.bounds.extents.y);
		}
	}


	public void SpaceBarButton(){
		if (!inputCooldown && IsInputActive()) {
			Debug.Log ("button");
			if (firstPress) {
				UI.HideTutorial ();
				firstPress = false;
			} else {
				GM.PressButton1 ();
				StartCoroutine ("InputCooldownWait");
			
			}
		}
	}

	public void EscButton(){
		if (!IsInputActive ()) {
			return;
		}
		if (MP != null) {
			MP.OpenMenu ();
		}
		//SceneManager.LoadScene (0);
	}

	IEnumerator InputCooldownWait(){
		inputCooldown = true;
		yield return new WaitForSeconds (inputCooldownTime);
		inputCooldown = false;
	}

}
