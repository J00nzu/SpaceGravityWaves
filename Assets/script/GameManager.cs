using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public float LevelRightBound = 2, LevelLeftBound = -4;

	public bool playing = false;
	public bool NextLevel = false;



	float winWaitingTime = 6;
	float loseWaitingTime = 1.5f;

	UIScript UI;


	public bool Restarting = false;

	Statistic point;

	List<PlanetScript> planetList = new List<PlanetScript>();


	// Use this for initialization
	void Start () {
		
		point = FindObjectOfType<Statistic> ();
		PlanetScript[] planets = FindObjectsOfType<PlanetScript> ();
		planetList.AddRange (planets);
		Pause ();
		UI = FindObjectOfType<UIScript> ();
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
		if(point!=null)
			point.deadpoint++;
		foreach (DynamicObject m in FindObjectsOfType<DynamicObject>()) {
			m.ResetPlay ();
		}
		SatelliteCollider sc = FindObjectOfType<SatelliteCollider> ();
		if (sc != null) {
			sc.Reset ();
		}
		Pause ();
	}

	public void Dead(){
		if (!Restarting) {
			
			Restarting = true;
			StartCoroutine ("WaitRestart");
			FindObjectOfType<MeteorScript> ().Explode ();

			JukeboxScript.PlayExplosion2 ();
		}
	}

	public void Victory(){
		
		if (!NextLevel) {
			if(point!=null)
				point.earthpoint++;
			NextLevel = true;
			StartCoroutine ("WaitForNextLevel");
			FindObjectOfType<MeteorScript> ().Explode ();
			FindObjectOfType<EarthScript> ().Explode ();

			JukeboxScript.PlayExplosion1 ();
			UI.ShowVictory ();
		}



	}


	IEnumerator WaitForNextLevel(){
		yield return new WaitForSeconds (winWaitingTime);
		NextLevel = false;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	 IEnumerator WaitRestart(){
		yield return new WaitForSeconds (loseWaitingTime);
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
