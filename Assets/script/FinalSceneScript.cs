using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

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
		ChangeToMenu ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {

			SceneManager.LoadScene (scene);

		}
	}

	public void ChangeToMenu(){
		SceneManager.LoadScene (scene);
	}
}
