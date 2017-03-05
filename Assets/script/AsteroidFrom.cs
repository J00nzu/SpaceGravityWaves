using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFrom : MonoBehaviour {
	public AsteroidTo to;
	public GameObject Asteroids;
	// Use this for initialization
	public float speed = 5.0f;
	void Start () {
		
		//to = FindObjectOfType<AsteroidTo> ();
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		Asteroids.transform.position = Vector3.MoveTowards (Asteroids.transform.position,to.transform.position,step);

	}

	public void MoveAsteroidBack(){


		Asteroids.transform.position = this.transform.position;
	}




}
