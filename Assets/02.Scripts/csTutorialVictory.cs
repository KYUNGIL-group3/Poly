using UnityEngine;
using System.Collections;

public class csTutorialVictory : MonoBehaviour {

	public GameObject goll;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player") {
			Destroy (goll);

			PlayerPrefs.SetInt ("FristAccount", 1);
		}
	}
}
