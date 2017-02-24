using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPhone : MonoBehaviour {
	public GameObject menuobject;
	public Slider MusicVolume;
	public Slider SfxVolume;
	public Image img;

	//public Vector3 MenuImage;
	public bool MenuState = false;

	InputHandler InputH;
	JukeboxScript Jbox;
	// Use this for initialization
	void Start () {
		InputH = FindObjectOfType<InputHandler> ();
		Jbox = FindObjectOfType<JukeboxScript> ();
		MusicVolume.value = 0.5f;
		SfxVolume.value = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (SfxVolume.value);
		Jbox.MusicSoundSlider (MusicVolume.value);
		Jbox.SfxSoundSlider (SfxVolume.value);



	}

	public void OpenMenu(){
		menuobject.SetActive (true);
		MenuState = true;
	}

	public bool GetMenuState(){


		return MenuState;
	}


}
