using UnityEngine;
using System.Collections;

public class csPhy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Oncrush(GameObject gameobj)
	{
		Transform[] obj = gameobj.GetComponentsInChildren<Transform> ();

		if (gameobj.GetComponent<Animator> () != null) {
			gameobj.GetComponent<Animator>().enabled = false;	
		}

		for (int i = 1; i < obj.Length; i++) {
			obj [i].gameObject.AddComponent<Rigidbody> ();
			obj [i].gameObject.AddComponent<BoxCollider> ();
			obj [i].gameObject.GetComponent<BoxCollider> ().size = new Vector3 (0.2f, 0.2f, 0.2f);
			Destroy (obj [i].gameObject, 3.0f);
			obj [i].parent = null;

		}

	}
}
