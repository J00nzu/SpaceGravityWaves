using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ArrowPos : MonoBehaviour {
	MeteorScript meteorit;
	GameManager GM;
	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		meteorit = FindObjectOfType<MeteorScript> ();
		GM = FindObjectOfType<GameManager>();
		sprite = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (meteorit != null) {
			this.transform.position = meteorit.transform.position;
			this.transform.rotation = Quaternion.Euler (0, 0, meteorit.angle);
		}

		if (GM != null) {
			if (GM.playing && sprite.enabled) {
				sprite.enabled = false;
			}else if( !GM.Restarting && !GM.playing && !sprite.enabled){
				sprite.enabled = true;
			}
		}
	}
}
