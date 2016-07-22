using UnityEngine;
using System.Collections;

public class csButtonITween : MonoBehaviour {

	bool onoff = true;
	Vector3 deflatVector;
	public int right = 0;
	public int up = -200;



	// Use this for initialization
	void Start () {


		deflatVector = transform.position;
		//AttackUIMove ();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UIMove()
	{
		if (onoff) {
			onoff = false;
			Hashtable hash = new Hashtable ();
			hash.Add ("position", deflatVector + new Vector3 (right, up, 0));
			hash.Add ("time", 1.5f);
			hash.Add ("ignoretimescale",true);
			iTween.MoveTo (gameObject, hash);
		} else {
			onoff = true;
			Hashtable hash = new Hashtable ();
			hash.Add ("position", deflatVector);
			hash.Add ("time", 1.5f);
			hash.Add ("ignoretimescale",true);
			iTween.MoveTo (gameObject, hash);
		}
	}
}
