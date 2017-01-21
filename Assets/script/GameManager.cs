using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public float LevelRightBound = 2, LevelLeftBound = -4;

	public bool playing = false;
	public bool NextLevel = false;

	public int WaittingTime = 1;

	public bool Restarting = false;

	Statistic point;

	List<PlanetScript> planetList = new List<PlanetScript>();


	// Use this for initialization
	void Start () {
		
		point = FindObjectOfType<Statistic> ();
		PlanetScript[] planets = FindObjectsOfType<PlanetScript> ();
		planetList.AddRange (planets);
		Pause ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public List<PlanetScript> GetAllPlanets(){
		return planetList;
	}



	public void Play(){
		if (playing)
			return;
		foreach (DynamicObject m in FindObjectsOfType<DynamicObject>()) {
			m.StartPlay ();
		}
		playing = true;
	}

	public void Pause(){
		foreach (DynamicObject m in FindObjectsOfType<DynamicObject>()) {
			m.InitPlay ();
		}
		playing = false;
	}

	public void Restart(){
		point.deadpoint++;
		Debug.Log ("Dead: "+ point.deadpoint + " Kills: "+ point.earthpoint);
		foreach (DynamicObject m in FindObjectsOfType<DynamicObject>()) {
			m.ResetPlay ();
		}
		Pause ();
	}

	public void Dead(){
		if (!Restarting) {
			
			Restarting = true;
			StartCoroutine ("WaitRestart");
		}
	}

	public void Victory(){
		
		if (!NextLevel) {
			point.earthpoint++;
			Debug.Log ("Dead: "+ point.deadpoint + " Kills: "+ point.earthpoint);
			NextLevel = true;
			StartCoroutine ("WaitForNextLevel");

		}



	}


	IEnumerator WaitForNextLevel(){
		yield return new WaitForSeconds (WaittingTime);
		Debug.Log ("Victory");
		NextLevel = false;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);


	}

	 IEnumerator WaitRestart(){



		yield return new WaitForSeconds (WaittingTime);
		Restart ();
		Restarting = false;
	}







	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawLine (new Vector3 (LevelRightBound, Camera.main.ScreenToWorldPoint(Vector3.zero).y, 0),
			new Vector3 (LevelRightBound, Camera.main.orthographicSize, 0));
		Gizmos.DrawLine (new Vector3 (LevelLeftBound, Camera.main.ScreenToWorldPoint(Vector3.zero).y, 0),
			new Vector3 (LevelLeftBound, Camera.main.orthographicSize, 0));
		
		Gizmos.DrawLine (new Vector3 (LevelRightBound, Camera.main.ScreenToWorldPoint(Vector3.zero).y, 0),
			new Vector3 (LevelLeftBound, Camera.main.ScreenToWorldPoint(Vector3.zero).y, 0));
		Gizmos.DrawLine (new Vector3 (LevelLeftBound, Camera.main.orthographicSize, 0),
			new Vector3 (LevelRightBound, Camera.main.orthographicSize, 0));
	}

}
