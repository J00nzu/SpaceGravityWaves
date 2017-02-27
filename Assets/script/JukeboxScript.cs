using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class JukeboxScript : MonoBehaviour {

	public AudioClip[] levelMusic;

	GameManager GM;
	AudioSource music, hit1, hit2, gravity;
	static JukeboxScript instance;

	private float sfxVol;
	private float musicVol;

	bool firstUpdate = true;


	// Use this for initialization
	void Start () {

		DontDestroyOnLoad (this);

		//if scene already has this object
		if (FindObjectsOfType (GetType()).Length > 1) {
			Destroy (gameObject);
			return;
		}

		SceneManager.sceneLoaded += NewLevelLoaded;

		music = transform.Find ("Music").GetComponent<AudioSource> ();
		hit1 = transform.Find ("Hit1").GetComponent<AudioSource> ();
		hit2 = transform.Find ("Hit2").GetComponent<AudioSource> ();
		gravity = transform.Find ("Gravity").GetComponent<AudioSource> ();

		instance = this;

		UpdateVolume ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (firstUpdate) {
			GM = FindObjectOfType<GameManager> ();

			firstUpdate = false;
		}

		float closestDistance = 999;
		float gVolume = 0;


		if (!GM.Restarting) {
			foreach (MeteorScript meteor in GM.GetAllMeteors()) {
				if (meteor != null && GM != null) {
					foreach (PlanetScript p in GM.GetAllPlanets()) {
						if (p != null) {
							float dist = (p.transform.position - meteor.transform.position).magnitude;

							if (dist < closestDistance) {
								closestDistance = dist;
							}
						}
					}
				}
			}

			gVolume = 1 - (closestDistance / 5);

			if (gVolume < 0) {
				gVolume = 0;
			}

		}



		gravity.volume = gVolume * sfxVol;
	}

	public static void PlayExplosion1(){
		if(instance != null)
			instance.hit1.Play ();
	}

	public static void PlayExplosion2(){
		if(instance != null)
			instance.hit2.Play ();
	}

	public static void PlayExplosion3(){
		if(instance != null)
			instance.hit2.Play ();

		//TODO :add real sound
	}


	public void UpdateVolume(){
		sfxVol = GameSettings.Get ().sfx;
		musicVol = GameSettings.Get ().music;

		music.volume = musicVol;

		hit1.volume = sfxVol;
		hit2.volume = sfxVol;
	}

	public void NewLevelLoaded(Scene scene, LoadSceneMode loadMode){
		firstUpdate = true;
		int sceneIndex = scene.buildIndex - GameManager.lvlIndexOffset - 1;

		try{
			AudioClip newClip = levelMusic[sceneIndex];;

			if(newClip != music.clip){
				music.clip = newClip;
				music.Play();
			}
		}catch(Exception ex){
			Debug.Log (ex);
		}
	}

}
