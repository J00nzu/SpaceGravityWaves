using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour {
	public int SatelitCount = 0;

	public bool AllSatelliteDestroied;
	int SatelitSize;
	// Use this for initialization
	void Start () {
		SatelitSize = FindObjectsOfType<SatelliteCollider> ().Length;
	}
	
	// Update is called once per frame
	void Update () {

		if(SatelitCount == SatelitSize)
			;
	}

}
