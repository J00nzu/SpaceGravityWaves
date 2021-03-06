using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof( Rigidbody2D))]

public class GravityPull : MonoBehaviour {



	float gravity = 8;
	float gravityexp = 1.5f;

	public float Mass_1;



	Rigidbody2D body;
	 
	//private float d;
	GameManager GM;



	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		GM = FindObjectOfType<GameManager> ();


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ForceCalc ();
	}

	private void ForceCalc(){
		
		if (!GM.Restarting && GM.playing) {
			foreach (PlanetScript planet in GM.GetAllPlanets()) {
				Vector2 dir = planet.transform.position - this.transform.position;
				float force;
				float distance = dir.magnitude;
				dir = dir.normalized;
				force = (Mass_1 * gravity * planet.mass) / Mathf.Pow (distance, gravityexp);
				body.AddForce (dir * force);
			}

		}
	
	}



}
