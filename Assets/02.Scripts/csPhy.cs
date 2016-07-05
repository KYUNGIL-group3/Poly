using UnityEngine;
using System.Collections;

public class csPhy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Transform[] obj = gameObject.GetComponentsInChildren<Transform> ();
		//Transform[] obj = gameObject.GetComponentInChildren<Transform>();
		for (int i = 1; i < obj.Length; i++) {
			if (obj [i].tag == "Point") {
				obj [i].parent = null;
				obj [i].gameObject.AddComponent<Rigidbody> ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log (collision.collider.name);
		if (collision.transform.tag == "Player") {
			Transform[] obj = gameObject.GetComponentsInChildren<Transform> ();
			for (int i = 1; i < obj.Length; i++) {
				obj [i].parent = null;
				obj [i].gameObject.AddComponent<Rigidbody> ();
			}
		}

	}
}
