using UnityEngine;
using System.Collections;

public class csWrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player") {
			Transform[] wrapto = GetComponentsInChildren<Transform> ();
            AudioManager.Instance().PlayWarpPotalSound();
			coll.transform.position = wrapto [1].transform.position;
			//Debug.Log (wrapto[1].gameObject.name);
		}

	}
}
