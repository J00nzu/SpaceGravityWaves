using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	GameManager GM;
	PlanetScript dragged = null;
	Vector3 offset;

	public bool IsPlanetDragged(){
		return dragged != null;
	}



	// Use this for initialization
	void Start () {
		GM = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		//KEYBOARD INPUT
		if(Input.GetKeyDown(KeyCode.Space)){
			if (GM.playing && !GM.Restarting) {
				GM.Restart ();
			} else {
				GM.Play ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}


		//MOUSE INPUT
		Vector3 Mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		float Mx = Mp.x;
		float My = Mp.y;

		if (!GM.playing) {

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
}
