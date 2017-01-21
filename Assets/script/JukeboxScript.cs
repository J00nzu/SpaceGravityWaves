using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxScript : MonoBehaviour {

	GameManager GM;
	MeteorScript meteor;
	AudioSource music, hit1, hit2, gravity;
	static JukeboxScript instance;



	// Use this for initialization
	void Start () {
		music = transform.Find ("Music").GetComponent<AudioSource> ();
		hit1 = transform.Find ("Hit1").GetComponent<AudioSource> ();
		hit2 = transform.Find ("Hit2").GetComponent<AudioSource> ();
		gravity = transform.Find ("Gravity").GetComponent<AudioSource> ();

		GM = FindObjectOfType<GameManager> ();
		meteor = FindObjectOfType<MeteorScript> ();

		DontDestroyOnLoad (this);

		instance = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float closestDistance = 999;

		foreach (PlanetScript p in GM.GetAllPlanets()) {
			float dist = (p.transform.position - meteor.transform.position).magnitude;

			if (dist < closestDistance) {
				closestDistance = dist;
			}
		}

		float volume = 1 - (closestDistance / 5);
		if (volume < 0) {
			volume = 0;
		}

		gravity.volume = volume;
	}

	public static void PlayExplosion1(){
		instance.hit1.Play ();
	}

	public static void PlayExplosion2(){
		instance.hit2.Play ();
	}


}
