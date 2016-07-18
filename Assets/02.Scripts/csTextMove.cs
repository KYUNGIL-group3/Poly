using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csTextMove : MonoBehaviour {

	public GameObject ChildText;
	float speed = 2000.0f;
	Vector3 target;

	float ismovetime = 0.0f;
	float ismovemaxtime = 1.0f;

	// Use this for initialization
	void Start () {
		target = new Vector3 (transform.parent.transform.position.x, transform.position.y, transform.position.z);
		//ChildText.GetComponent<Text> ().preferredWidth;
		Debug.Log (ChildText.GetComponent<Text> ().preferredWidth);
	}
	
	// Update is called once per frame
	void Update () {
		if (ismovetime > ismovemaxtime) {
			return;
		}
		ismovetime += Time.deltaTime;

		transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
	}
}
