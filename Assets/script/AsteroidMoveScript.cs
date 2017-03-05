using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMoveScript : MonoBehaviour {

	public float speed = 0.5f;
	public bool SetToMove;
	AsteroidTo to;
	AsteroidFrom Afrom;
	GameManager GM;
	// Use this for initialization
	void Start () {
		GM = FindObjectOfType<GameManager> ();
		to = FindObjectOfType<AsteroidTo> ();
		Afrom = FindObjectOfType<AsteroidFrom> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(SetToMove){
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (this.transform.position,to.transform.position,step);
		}
	}
	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Finish") {


			this.transform.position = Afrom.transform.position;
		}

		if (other.gameObject.name == "meteor") {
			
			MeteorScript met = other.transform.GetComponent<MeteorScript> ();
			if (met!=null) {
				met.Explode ();
				GM.Dead ();
			}
		}

	}




}
