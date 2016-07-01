using UnityEngine;
using System.Collections;

public class csRunFollow : MonoBehaviour {


	public GameObject ball;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ballPos = ball.GetComponent<Transform> ().position;
		//transform.position = ballPos;

		transform.position = new Vector3 (ballPos.x,ballPos.y,ballPos.z);
	}
}
