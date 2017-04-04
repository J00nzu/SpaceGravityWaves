using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoaderScript : MonoBehaviour {

	Grabbable grabbed;
	Vector3 offset;
	AsyncOperation asyLoad;

	public Image fadeOut;
	public Image rotator;
	public int levelIndexToLoad;

	bool finalLoading;

	// Use this for initialization
	void Start () {
		if (Application.platform == RuntimePlatform.OSXPlayer) {
			Screen.SetResolution (1280, 720, false);
		}

		if (Application.isMobilePlatform) {
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = false;
			Screen.autorotateToPortraitUpsideDown = false;
			Screen.orientation = ScreenOrientation.AutoRotation;
		}

		StartCoroutine ("LevelLoadRoutine");
	}

	IEnumerator LevelLoadRoutine(){
		fadeOut.CrossFadeAlpha (0, 6, true);

		yield return new WaitForSeconds(4);

		if (!Application.isEditor) {
			asyLoad = SceneManager.LoadSceneAsync (levelIndexToLoad);
		}
	}


	// Update is called once per frame
	void Update () {
		if (asyLoad != null) {
			if (!finalLoading && asyLoad.progress>=0.8f) {
				fadeOut.CrossFadeAlpha (1, 4, true);
				finalLoading = true;
			}
		}

		rotator.rectTransform.Rotate(new Vector3 (0, 0, -40) * Time.deltaTime);

		if (grabbed == null) {
			if (Input.GetMouseButton (0)) {
				
				RaycastHit hit; 
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
				if ( Physics.Raycast (ray,out hit,100.0f)) {
					Grabbable grabo = hit.transform.GetComponent<Grabbable> ();
					if (grabo != null) {
						grabbed = grabo;
						Vector3 off = grabo.transform.position - hit.point;
						this.offset = new Vector3 (off.x, off.y, 0);

						Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
						Debug.Log("Offset is " + offset); // ensure you picked right object

					}
				}
			}
		} else {
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			string[] layerNames = { "RaycastTarget" };
			if (Physics.Raycast (ray, out hit, 100f, LayerMask.GetMask (layerNames))) {
				Vector3 targetPosition = (new Vector3 (hit.point.x, hit.point.y, grabbed.transform.position.z) + offset);
				Rigidbody rig = grabbed.GetComponent<Rigidbody> ();
				rig.velocity += targetPosition - grabbed.transform.position;

			}

			if (!Input.GetMouseButton (0)) {
				grabbed = null;
			}
		}


	}
}
