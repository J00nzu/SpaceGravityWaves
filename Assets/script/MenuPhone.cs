using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPhone : MonoBehaviour {
	GameObject MenuObject;
	Slider MusicVolume;
	Slider SfxVolume;
	GameObject MenuButton;

	bool MenuState = false;

	InputHandler InputH;
	JukeboxScript Jbox;

	// Use this for initialization
	void Start () {

		/**
		 * TODO find sliders and stuffs here..
		*/

		MenuObject = GameObject.Find ("MenuContainer").gameObject;
		MusicVolume = GameObject.Find ("MusicSlider").GetComponent<Slider> ();
		SfxVolume = GameObject.Find ("SfxSlider").GetComponent<Slider> ();
		MenuButton = GameObject.Find ("Manu");

		InputH = FindObjectOfType<InputHandler> ();
		Jbox = FindObjectOfType<JukeboxScript> ();
		MusicVolume.value = 0.5f;
		SfxVolume.value = 0.5f;

		CloseMenu ();
	}


	
	// Update is called once per frame
	void Update () {
		if (MenuState) {
			GameSettings.Get ().music = MusicVolume.value;
			GameSettings.Get ().sfx = SfxVolume.value;
			Jbox.UpdateVolume ();
		}
	}

	public void OpenMenu(){
		MenuObject.SetActive (true);
		InputH.DeactivateInput ();
		MusicVolume.value = GameSettings.Get ().music;
		SfxVolume.value = GameSettings.Get ().sfx;


		StartCoroutine ("RotateButton");

		MenuState = true;
	}

	public void CloseMenu(){
		MenuObject.SetActive (false);
		InputH.ActivateInput ();
		GameSettings.Save ();
		MenuState = false;
	}

	public bool GetMenuState(){
		return MenuState;
	}

	IEnumerator RotateButton(){
		for (int i = 0; i < 15; i++) {
			MenuButton.transform.Rotate (new Vector3(0,0,-2f));
			yield return null;
		}
	}


}
