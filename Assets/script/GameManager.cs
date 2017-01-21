using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

	public float LevelRightBound = 2, LevelLeftBound = -4;

	List<PlanetScript> planetList = new List<PlanetScript>();


	// Use this for initialization
	void Start () {
		PlanetScript[] planets = FindObjectsOfType<PlanetScript> ();
		planetList.AddRange (planets);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public List<PlanetScript> GetAllPlanets(){
		return planetList;
	}



	public void Play(){
		//TODO
	}

	public void Pause(){
		//TODO
	}

	public void Restart(){
		//TODO
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
