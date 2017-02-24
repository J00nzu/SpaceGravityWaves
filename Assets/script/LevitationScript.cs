using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationScript : MonoBehaviour {
	// Use this for initialization

	public float Div = 8;
	public float dis = 0.2f;


	void Start () {
		
	}

	void FixedUpdate(){
		transform.position = new Vector3 (transform.position.x, Mathf.PingPong(Time.time/Div,dis));


	}

	// Update is called once per frame



}
