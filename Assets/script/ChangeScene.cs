using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {


	public int scene = 0;
	public bool onLoad = true;
	// Use this for initialization
	void Start () {
		if (onLoad) {
			SceneManager.LoadScene (scene);
		}
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
