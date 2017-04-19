using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

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

		MenuObject = GameObject.Find ("MenuContainer").gameObject;
		MusicVolume = GameObject.Find ("MusicSlider").GetComponent<Slider> ();
		SfxVolume = GameObject.Find ("SfxSlider").GetComponent<Slider> ();
		MenuButton = GameObject.Find ("Manu");



		LevelSelect = GameObject.Find ("LevelSelect");
		YearCounter = GameObject.Find ("YearCounter");


		GM = FindObjectOfType<GameManager> ();

		for (int i = 1; i <= 11; i++) {
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
					Analytics.CustomEvent ("loadlevel", new Dictionary<string, object> {
						{"levelIndex", lvButt.index}
					});
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


		CloseMenu (false);
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
		OpenMenu (true);
	}

	public void OpenMenu(bool sound){
		MenuObject.SetActive (true);
		InputH.DeactivateInput ();
		MusicVolume.value = GameSettings.Get ().music;
		SfxVolume.value = GameSettings.Get ().sfx;


		StartCoroutine ("RotateButton");

		MenuState = true;
		if(sound)JukeboxScript.PlayClick ();
	}

	public void CloseMenu(){
		CloseMenu (true);
	}

	public void CloseMenu(bool sound){
		MenuObject.SetActive (false);
		InputH.ActivateInput ();
		GameSettings.Save ();
		MenuState = false;
		CloseLevelSelect (sound);
		if(sound)JukeboxScript.PlayClick ();
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
		OpenLevelSelect (true);
	}

	public void OpenLevelSelect(bool sound){
		LevelSelect.SetActive (true);
		if(sound)JukeboxScript.PlayClick ();
	}

	public void CloseLevelSelect(){
		CloseLevelSelect (true);
	}

	public void CloseLevelSelect(bool sound){
		LevelSelect.SetActive (false);
		if(sound)JukeboxScript.PlayClick ();
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

	public void HideMenuControls(){
		YearCounter.SetActive (false);
		MenuButton.SetActive (false);
	}

	public void ShowMenuControls(){
		YearCounter.SetActive (true);
		MenuButton.SetActive (true);
	}

	public void GT_beeb_O(){
		GameSettings.Save ();
		Application.Quit ();
	}

	IEnumerator RotateButton(){
		for (int i = 0; i < 15; i++) {
			MenuButton.transform.Rotate (new Vector3(0,0,-2f));
			yield return null;
		}
	}

}
