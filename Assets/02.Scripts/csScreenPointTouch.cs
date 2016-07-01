using UnityEngine;
using System.Collections;

public class csScreenPointTouch : MonoBehaviour {

	public ArrayList Vec3ArrayList;


	// Use this for initialization
	void Start () {
		Vec3ArrayList = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll (ray);
			for (int i = 0; i < hits.Length; i++) {
				RaycastHit hit = hits [i];
				if (hit.transform.tag.Equals ("Map")) {
					Vec3ArrayList.Add (hit.point);
				}
			}
		} 

		if (Input.GetButtonUp ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll (ray);
			for (int i = 0; i < hits.Length; i++) {
				RaycastHit hit = hits [i];
				if (hit.transform.tag.Equals ("Map")) {
					Vec3ArrayList.Add (hit.point);
				}
			}
		}
	}
}
