using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class JukeboxScript : MonoBehaviour {

	public AudioClip[] levelMusic;

	GameManager GM;
	SatelliteCollider shieldObject;
	AudioSource music1, music2, hit1, hit2, hit3,
	launch, blackHole, click, gravity, shield;
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

		music1 = transform.Find ("Music1").GetComponent<AudioSource> ();
		music2 = transform.Find ("Music2").GetComponent<AudioSource> ();

		hit1 = transform.Find ("Hit1").GetComponent<AudioSource> ();
		hit2 = transform.Find ("Hit2").GetComponent<AudioSource> ();
		hit3 = transform.Find ("Hit3").GetComponent<AudioSource> ();
		click = transform.Find ("Click").GetComponent<AudioSource> ();
		launch = transform.Find ("Launch").GetComponent<AudioSource> ();
		blackHole = transform.Find ("BlackHole").GetComponent<AudioSource> ();
		gravity = transform.Find ("Gravity").GetComponent<AudioSource> ();
		shield = transform.Find ("Shield").GetComponent<AudioSource> ();


		instance = this;

		UpdateVolume ();

		gravity.volume = 0;
		shield.volume = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (firstUpdate) {
			GM = FindObjectOfType<GameManager> ();
			shieldObject = FindObjectOfType<SatelliteCollider> ();
			firstUpdate = false;
		}

		float gClosestDistance = 999;
		float gVolume = 0;

		float sClosestDistance = 999;
		float sVolume = 0;


		if (!GM.Restarting) {

			//Gravity sound volume
			foreach (MeteorScript meteor in GM.GetAllMeteors()) {
				if (meteor != null && GM != null) {
					foreach (PlanetScript p in GM.GetAllPlanets()) {
						if (p != null) {
							float dist = (p.transform.position - meteor.transform.position).magnitude;

							if (dist < gClosestDistance) {
								gClosestDistance = dist;
							}
						}
					}
				}
			}

			gVolume = 1 - (gClosestDistance / 5);

			if (gVolume < 0) {
				gVolume = 0;
			}


			if(shieldObject!=null){
				//Shield sound volume
				foreach (MeteorScript meteor in GM.GetAllMeteors()) {
					if (meteor != null && GM != null) {
						float dist = (shieldObject.transform.position - meteor.transform.position).magnitude;

						if (dist < sClosestDistance) {
							sClosestDistance = dist;
						}
					}
				}

				sVolume = 1 - (sClosestDistance / 5);

				if (sVolume < 0) {
					sVolume = 0;
				}
				Debug.Log (sVolume);
			}

		}



		gravity.volume = gVolume * sfxVol;
		shield.volume = sVolume * sfxVol;
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
			instance.hit3.Play ();
	}

	public static void PlayClick(){
		if(instance != null)
			instance.click.Play ();
	}

	public static void PlayLaunch(){
		if(instance != null)
			instance.launch.Play ();
	}

	public static void PlayBlackHole(){
		if(instance != null)
			instance.blackHole.Play ();
	}


	public void UpdateVolume(){
		sfxVol = GameSettings.Get ().sfx;
		musicVol = GameSettings.Get ().music;

		music1.volume = musicVol;
		music2.volume = musicVol;

		hit1.volume = sfxVol;
		hit2.volume = sfxVol;
		hit3.volume = sfxVol;
		launch.volume = sfxVol;
		blackHole.volume = sfxVol;
		click.volume = sfxVol;
		//gravity.volume = sfxVol;
		//shield.volume = sfxVol;
	}

	public void NewLevelLoaded(Scene scene, LoadSceneMode loadMode){
		firstUpdate = true;
		int sceneIndex = scene.buildIndex - GameManager.lvlIndexOffset - 1;
		Debug.Log ("sceneIndexJukebox: " + sceneIndex);
		try{
			AudioClip newClip = levelMusic[sceneIndex];

			if(newClip != music1.clip){
				music1.clip = newClip;
				music1.Play();
			}
		}catch(Exception ex){
			Debug.Log (ex);
		}
	}

}
