using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MacPlayerPrefs : MonoBehaviour {

	[MenuItem ("Edit/Reset Playerprefs")] public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
