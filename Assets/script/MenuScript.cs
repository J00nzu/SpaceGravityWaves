using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour {


	public Button StartGame;

	public Button Credits;

	public Button Quit;

	void Start () {


		Button start = StartGame.GetComponent<Button> ();
		start.onClick.AddListener (StartTask);

		Button creditit = Credits.GetComponent<Button> ();
		creditit.onClick.AddListener (Credit);

		Button exit = Quit.GetComponent<Button> ();
		exit.onClick.AddListener (ExitDaGame);
	}

	void StartTask(){
		SceneManager.LoadScene (2);
	}

	void ExitDaGame(){
		Debug.Log ("pitäis lähtee");
		Application.Quit ();
	}

	void Credit(){
		Debug.Log ("credits");
		SceneManager.LoadScene (1);
	}
}
