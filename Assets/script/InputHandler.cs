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
	public bool firstPress = true;
	bool inputCooldown, inputActive = true;
	float inputCooldownTime = 0.5f;

	MenuPhone MP;

	Vector3 LastMp;
	int LastTouchID=0;


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

		if (Application.isMobilePlatform) {
			Input.multiTouchEnabled = true;
		}
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

		Vector3 Mp = Vector3.zero;
		bool MousePressed = false;


		//MULTI-TOUCH INPUT
		if (Application.isMobilePlatform) {
			Touch[] myTouches = Input.touches;


			if (myTouches.Length == 0) {
				Mp = LastMp;
				MousePressed = false;
				LastTouchID = 0;
			} else if (myTouches.Length == 1) {
				Touch touche = myTouches [0];

				MousePressed = true;
				LastTouchID = touche.fingerId;
				Mp = Camera.main.ScreenToWorldPoint (touche.position);
				LastMp = Mp;
			} else {
				bool touchIdFound = false;

				foreach (Touch touche in myTouches) {
					
					if (LastTouchID == touche.fingerId) {
						MousePressed = true;
						Mp = Camera.main.ScreenToWorldPoint (touche.position);
						LastMp = Mp;
						touchIdFound = true;
						break;
					}

				}
				//Break MousePressed if touchId was not found
				if (!touchIdFound) {
					Mp = LastMp;
					MousePressed = false;
					LastTouchID = 0;
				}
			}




		} else {
			//For da computor
			Mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			MousePressed = Input.GetMouseButton (0);
		}


		float Mx = Mp.x;
		float My = Mp.y;

		if (!GM.Introing) {
			int highestLayerOrder=0;

			if (dragged == null) {
				if (MousePressed  && !inputCooldown) {
					foreach (PlanetScript p in GM.GetAllPlanets()) {
						float r = p.GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;
						Vector3 off = p.transform.position - new Vector3 (Mx, My, 0);
						float dist = (off).magnitude;

						if (Application.isMobilePlatform) {
							r = Mathf.Clamp (r, 0.6f, 20);
						}

						if (dist < r) {
							int layerOrder = p.GetComponent<SpriteRenderer> ().sortingOrder;

							if (layerOrder > highestLayerOrder) {
								highestLayerOrder = layerOrder;
								this.offset = off;
								dragged = p;
							}
						}
					}


					if (dragged != null && GM.playing && !inputCooldown && !GM.Restarting) {
						GM.Pause ();
						GM.Restart ();
					}
				}
			} else {
				dragged.MoveTo (new Vector3 (Mx, My, 0) + offset);
				if (!MousePressed) {
					dragged = null;
				}

				if (GM.playing && inputCooldown) {
					dragged = null;
				}

			}


		}

	}

	void OnDrawGizmos(){
		foreach (PlanetScript p in FindObjectsOfType<PlanetScript>()) {
			Gizmos.color = Color.green;

			float r = p.GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;

			r = Mathf.Clamp (r, 0.6f, 20);


			Gizmos.DrawWireSphere (p.transform.position, r);
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
