using UnityEngine;
using System.Collections;

public class csMoveUI : MonoBehaviour {

	float state = 0.0f;
	float maxstate = 3.0f;
	float speed = 3.0f;
	public bool isSkill;

	Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isSkill) {
			state += Time.unscaledDeltaTime;

			if (state < maxstate) {
				//gameObject.transform.position = Vector3.MoveTowards (transform.position, target.transform, speed * Time.unscaledDeltaTime);
			}
		}
	}
}
