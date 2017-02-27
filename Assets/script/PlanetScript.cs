using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {


	public float mass = 1;

	GameManager GM;

	void Start(){
		GM = FindObjectOfType<GameManager> ();
	}


	public void MoveTo(Vector3 position){
		float x = Mathf.Clamp (position.x, GM.LevelLeftBound, GM.LevelRightBound);
		float y = Mathf.Clamp(position.y, Camera.main.ScreenToWorldPoint(Vector3.zero).y, Camera.main.orthographicSize);
		float z = position.z;
	
		this.transform.position = new Vector3 (x, y, z);
	}

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag == "Player"){
			MeteorScript met = other.transform.GetComponent<MeteorScript> ();
			if (met!=null) {
				met.Explode ();
				GM.Dead ();
			}
		}
	}



}
