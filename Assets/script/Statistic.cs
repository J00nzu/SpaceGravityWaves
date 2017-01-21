using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour {

	public int deadpoint = 0;
	public int earthpoint = 0;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		Debug.Log ("deads:"+deadpoint + "kills:" + earthpoint);

	}
	
	// Update is called once per frame



}
