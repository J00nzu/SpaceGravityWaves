using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : DynamicObject {

	public float angle;

	public float meteoritForce;

	Rigidbody2D body;

	//private float d;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();

	}

	protected override void PlayPressed(){
		StartForce ();
	}
	
	// Update is called once per frame


	void StartForce(){
		

		Vector2 dir = new Vector2 ();

		dir.x=  (Mathf.Cos (Mathf.Deg2Rad * angle));
		dir.y =  (Mathf.Sin (Mathf.Deg2Rad * angle));


		//(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
		body.AddForce (dir*meteoritForce);
	}

}
