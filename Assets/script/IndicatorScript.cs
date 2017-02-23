using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IndicatorScript : MonoBehaviour {

	/* Set this to a prefab of a sprite with the desired indicator*/
	public GameObject indPrefab;

	List<IndicatorChild> indList = new List<IndicatorChild> ();


	// Use this for initialization
	void Start () {
		MeteorScript[] meteors = FindObjectsOfType<MeteorScript> ();

		if (indPrefab == null) {
			Debug.LogError ("No indicatorPrefab found. Did you forget to set it in IndicatorScript?");
			return;
		}

		foreach (MeteorScript meteor in meteors) {
			GameObject newGo = GameObject.Instantiate (indPrefab);
			IndicatorChild idc = new IndicatorChild ();
			idc.gameObject = newGo;
			idc.sprite = newGo.GetComponent<SpriteRenderer> ();
			idc.target = meteor;
			newGo.transform.parent = this.transform;

			if (idc.sprite == null) {
				Debug.LogError ("No SpriteRenderer found in IndicatorPrefab!!!! Skipping.");
			} else {
				idc.sprite.enabled = false;
				indList.Add (idc);
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach(IndicatorChild indie in indList){
			MeteorScript target = indie.target;
			float verticalHigh = Camera.main.orthographicSize;
			float horizontalHigh = verticalHigh * Screen.width / Screen.height;
			float verticalLow = Camera.main.ScreenToWorldPoint (Vector3.zero).y;
			float horizontalLow = Camera.main.ScreenToWorldPoint (Vector3.zero).x;
			float buffer = 0.3f;
			if ((
				(target.transform.position.y < verticalLow - buffer) ||
				(target.transform.position.y > verticalHigh + buffer) ||
				(target.transform.position.x < horizontalLow - buffer) ||
				(target.transform.position.x > horizontalHigh + buffer)) &&
				(target.isAlive())
			) {
				if (!indie.sprite.enabled) {
					indie.sprite.enabled = true;
				}

				//Rotate indicator
				indie.gameObject.transform.right = target.transform.position.normalized;

				//Move indicator

				Vector2 lineStart = Vector2.zero;
				Vector2 lineEnd = new Vector2 (target.transform.position.x, target.transform.position.y);
				Vector2 intersect = Vector2.zero;


				/**
				 * WARNING:  Horrible abomination below.
				*/
				//Check top bound
				Vector2 topLineStart = new Vector2(horizontalLow, verticalHigh);
				Vector2 topLineEnd = new Vector2(horizontalHigh, verticalHigh);
				if(!LineIntersection(lineStart, lineEnd, topLineStart, topLineEnd, ref intersect)){
					//Check bot bound
					Vector2 botLineStart = new Vector2(horizontalLow, verticalLow);
					Vector2 botLineEnd = new Vector2(horizontalHigh, verticalLow);
					if (!LineIntersection (lineStart, lineEnd, botLineStart, botLineEnd, ref intersect)) {
						//Check left bound
						Vector2 leftLineStart = new Vector2(horizontalLow, verticalHigh);
						Vector2 leftLineEnd = new Vector2(horizontalLow, verticalLow);
						if (!LineIntersection (lineStart, lineEnd, leftLineStart, leftLineEnd, ref intersect)) {
							//Check right bound
							Vector2 rightLineStart = new Vector2(horizontalHigh, verticalHigh);
							Vector2 rightLineEnd = new Vector2(horizontalHigh, verticalLow);
							LineIntersection (lineStart, lineEnd, rightLineStart, rightLineEnd, ref intersect);
						}
					}
				}

				indie.gameObject.transform.position = new Vector3 (intersect.x, intersect.y, 0);

			} else {
				if (indie.sprite.enabled) {
					indie.sprite.enabled = false;
				}
			}
		}
	}

	/*
	 * From https://forum.unity3d.com/threads/line-intersection.17384/
	*/
	public static bool LineIntersection( Vector2 l1_s,Vector2 l1_e, Vector2 l2_s, Vector2 l2_e, ref Vector2 intersection )
	{

		float Ax,Bx,Cx,Ay,By,Cy,d,e,f,num/*,offset*/;
		float x1lo,x1hi,y1lo,y1hi;
		Ax = l1_e.x-l1_s.x;
		Bx = l2_s.x-l2_e.x;

		// X bound box test/
		if(Ax<0) {
			x1lo=l1_e.x; x1hi=l1_s.x;
		} else {
			x1hi=l1_e.x; x1lo=l1_s.x;
		}

		if(Bx>0) {
			if(x1hi < l2_e.x || l2_s.x < x1lo) return false;
		} else {
			if(x1hi < l2_s.x || l2_e.x < x1lo) return false;

		}

		Ay = l1_e.y-l1_s.y;
		By = l2_s.y-l2_e.y;

		// Y bound box test//
		if(Ay<0) {                  
			y1lo=l1_e.y; y1hi=l1_s.y;
		} else {
			y1hi=l1_e.y; y1lo=l1_s.y;
		}

		if(By>0) {
			if(y1hi < l2_e.y || l2_s.y < y1lo) return false;
		} else {
			if(y1hi < l2_s.y || l2_e.y < y1lo) return false;
		}

		Cx = l1_s.x-l2_s.x;
		Cy = l1_s.y-l2_s.y;
		d = By*Cx - Bx*Cy;  // alpha numerator//
		f = Ay*Bx - Ax*By;  // both denominator//

		// alpha tests//
		if(f>0) {
			if(d<0 || d>f) return false;
		} else {
			if(d>0 || d<f) return false;
		}

		e = Ax*Cy - Ay*Cx;  // beta numerator//

		// beta tests //

		if(f>0) {                          
			if(e<0 || e>f) return false;
		} else {
			if(e>0 || e<f) return false;
		}


		// check if they are parallel
		if(f==0) return false;

		// compute intersection coordinates //

		num = d*Ax; // numerator //

		//    offset = same_sign(num,f) ? f*0.5f : -f*0.5f;   // round direction //

		//    intersection.x = p1.x + (num+offset) / f;
		intersection.x = l1_s.x + num / f;



		num = d*Ay;

		//    offset = same_sign(num,f) ? f*0.5f : -f*0.5f;
		//    intersection.y = p1.y + (num+offset) / f;
		intersection.y = l1_s.y + num / f;

		return true;

	}
}

struct IndicatorChild{
	public GameObject gameObject;
	public SpriteRenderer sprite;
	public MeteorScript target;
}
