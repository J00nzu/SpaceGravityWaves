using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPhone : MonoBehaviour {
	GameObject MenuObject;
	Slider MusicVolume;
	Slider SfxVolume;
	GameObject YearCounter;
	GameObject MenuButton;

	struct LvlSelectButton{
		public int index;
		public Button button;
		public Image image;
	}

	GameObject LevelSelect;
	List<LvlSelectButton> LevelSelectButtons = new List<LvlSelectButton> ();

	bool MenuState = false;

	InputHandler InputH;
	JukeboxScript Jbox;
	GameManager GM;

	// Use this for initialization
	void Start () {

		/**
		 * TODO find sliders and stuffs here..
		*/

		MenuObject = GameObject.Find ("MenuContainer").gameObject;
		MusicVolume = GameObject.Find ("MusicSlider").GetComponent<Slider> ();
		SfxVolume = GameObject.Find ("SfxSlider").GetComponent<Slider> ();
		MenuButton = GameObject.Find ("Manu");



		LevelSelect = GameObject.Find ("LevelSelect");
		YearCounter = GameObject.Find ("YearCounter");


		GM = FindObjectOfType<GameManager> ();

		for (int i = 1; i <= 10; i++) {
			GameObject gob = GameObject.Find ("ButtonLvl"+i);
			Button bu = gob.GetComponent<Button> ();
			Image im = gob.GetComponent<Image> ();
			LvlSelectButton lvButt = new LvlSelectButton ();
			lvButt.index = i;
			lvButt.button = bu;
			lvButt.image = im;

			if (i <= GameSettings.Get ().progress) {
				im.color = new Color (0, 0, 0, 0);
				bu.interactable = true;
				bu.onClick.AddListener (() => {
					GM.ChangeLevel(lvButt.index);
					JukeboxScript.PlayClick ();
				});
			} else {
				im.color = new Color (0, 0, 0, 1f);
				im.enabled = true;
				bu.interactable = false;
			}
		}

		if (GameSettings.Get ().yearEnabled) {
			ShowYearCounter ();
		} else {
			HideYearCounter ();
		}

		InputH = FindObjectOfType<InputHandler> ();
		Jbox = FindObjectOfType<JukeboxScript> ();
		MusicVolume.value = 0.5f;
		SfxVolume.value = 0.5f;


		CloseMenu ();
		CloseLevelSelect ();
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
		JukeboxScript.PlayClick ();
	}

	public void CloseMenu(){
		MenuObject.SetActive (false);
		InputH.ActivateInput ();
		GameSettings.Save ();
		MenuState = false;
		CloseLevelSelect ();
		JukeboxScript.PlayClick ();
	}

	public bool GetMenuState(){
		return MenuState;
	}

	public void SwitchLevelSelect(){
		if (LevelSelect.activeInHierarchy) {
			CloseLevelSelect ();
		} else {
			OpenLevelSelect ();
		}

	}

	public void OpenLevelSelect(){
		LevelSelect.SetActive (true);
		JukeboxScript.PlayClick ();
	}

	public void CloseLevelSelect(){
		LevelSelect.SetActive (false);
		JukeboxScript.PlayClick ();
	}

	public void SwitchYearCounter(){
		JukeboxScript.PlayClick ();
		if (YearCounter.activeInHierarchy) {
			HideYearCounter ();
			GameSettings.Get ().yearEnabled = false;
		} else {
			ShowYearCounter ();
			GameSettings.Get ().yearEnabled = true;
		}
	}

	public void ShowYearCounter(){
		YearCounter.SetActive (true);
	}

	public void HideYearCounter(){
		YearCounter.SetActive (false);
	}

	IEnumerator RotateButton(){
		for (int i = 0; i < 15; i++) {
			MenuButton.transform.Rotate (new Vector3(0,0,-2f));
			yield return null;
		}
	}

}
