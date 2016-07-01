using UnityEngine;
using System.Collections;

public class csRaycastAll : MonoBehaviour {

	private float speed = 5.0f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		float amtMove = speed * Time.deltaTime;
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		transform.Translate (Vector3.right * hor * amtMove);
		transform.Translate (Vector3.forward * ver * amtMove);

		Debug.DrawRay (transform.position, transform.forward * 8, Color.red);

		RaycastHit[] hits;
		hits = Physics.RaycastAll (transform.position, transform.forward, 8);

		for (int i = 0;i< hits.Length; i++) {
			RaycastHit hit = hits [i];

			Debug.Log (hit.collider.gameObject.name);
		}
	}
}
