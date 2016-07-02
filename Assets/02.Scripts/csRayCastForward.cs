using UnityEngine;
using System.Collections;

public class csRayCastForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Debug.DrawRay (transform.position, Vector3.down,Color.red);

		RaycastHit hit;

		if (Physics.Raycast (transform.position, Vector3.down, out hit, 8.0f)) {
			gameObject.transform.parent.gameObject.GetComponent<csPointSetPosition> ().Forwarddis (hit.distance);
			Destroy (gameObject);
		} else {
			Destroy (gameObject.transform.parent.gameObject);
		}
			

	}
}
