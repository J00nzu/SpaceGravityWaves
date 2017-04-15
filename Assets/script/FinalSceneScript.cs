using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.UI;
using System;

public class FinalSceneScript : MonoBehaviour {
	
	public int scene = 0;
	// Use this for initialization
	void Start () {
		Statistic stat = FindObjectOfType<Statistic> ();
		if (stat != null) {
			Analytics.CustomEvent ("gameClear", new Dictionary<string, object> {
				{"yearsTook", stat.year},
				{"deadPoint", stat.deadpoint},
				{"earthPoint", stat.earthpoint}
			});
		}

		GameSettings.Get ().lastLevelBuildIndex = 0;



		try{
			Text years = GameObject.Find ("Years").GetComponent<Text> ();
			Text earths = GameObject.Find ("Earths").GetComponent<Text> ();
			Text fails = GameObject.Find ("Fails").GetComponent<Text> ();


			if (stat != null) {
				years.text = ((int)stat.year - 2017)+"";
				earths.text = (stat.earthpoint)+"";
				fails.text = (stat.deadpoint)+"";
			}

		}catch(Exception ex){
			
		}

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButton(0) ) {
			ChangeToMenu ();
		}
	}

	public void ChangeToMenu(){
		SceneManager.LoadScene (scene);
	}
}
