using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ArrowPos : MonoBehaviour {
	MeteoritLaunch meteorit;


	// Use this for initialization
	void Start () {
		meteorit = FindObjectOfType<MeteoritLaunch> ();

	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = meteorit.transform.position;
		this.transform.rotation = Quaternion.Euler(0,0,meteorit.angle) ;
	}
}
