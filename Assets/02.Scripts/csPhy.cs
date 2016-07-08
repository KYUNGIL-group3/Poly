using UnityEngine;
using System.Collections;

public class csPhy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnCollisionEnter(Collision collision)
//	{
//		Transform[] obj = gameObject.GetComponentsInChildren<Transform> ();
//		//		Transform[] obj = gameObject.GetComponentsInChildren<Transform> ();
//		gameObject.GetComponent<Animator> ().enabled = false;
//		for (int i = 1; i < obj.Length; i++) {
//			obj [i].gameObject.AddComponent<Rigidbody> ();
//			//obj [i].gameObject.GetComponent<Rigidbody> ().mass = 1;
//			//obj [i].gameObject.GetComponent<Rigidbody> ().drag = 10;
//			//obj [i].gameObject.GetComponent<Rigidbody> ().angularDrag = 10;
//			obj [i].gameObject.AddComponent<BoxCollider> ();
//
//			obj [i].parent = null;
//			//obj [i].gameObject.GetComponent<BoxCollider> ().isTrigger = true;
//		}
//
//	}


	void OnTriggerEnter(Collider collision)
	{
		Transform[] obj = gameObject.GetComponentsInChildren<Transform> ();

		gameObject.GetComponent<Animator>().enabled = false;
		for (int i = 1; i < obj.Length; i++) {
			obj [i].gameObject.AddComponent<Rigidbody> ();
			obj [i].gameObject.AddComponent<BoxCollider> ();
			obj [i].gameObject.GetComponent<BoxCollider> ().size = new Vector3 (0.2f, 0.2f, 0.2f);
			//obj [i].gameObject.GetComponent<Rigidbody> ().drag = 50;

			//obj [i].gameObject.AddComponent<MeshCollider> ();
			Destroy(obj [i].gameObject,1.5f);
			obj [i].parent = null;

		}

	}
}
