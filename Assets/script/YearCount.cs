using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearCount : MonoBehaviour {

	public int Years = 2017;
	float yearcoutn;
	// Use this for initialization
	void Start () {
		
		yearcoutn  = Years;
	}
	
	// Update is called once per frame
	void Update () {
		
		yearcoutn += Time.deltaTime;
		if (yearcoutn > Years) {
			Years++;
		}
		Debug.Log ("Year: " + Years);
	}
}
