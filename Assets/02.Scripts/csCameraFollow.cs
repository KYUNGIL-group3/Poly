using UnityEngine;
using System.Collections;

public class csCameraFollow : MonoBehaviour {


	GameObject Player;


	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		//Vector3 PlayerPos = Player.GetComponent<Transform> ().position;
		Vector3 PlayerPos = Player.transform.position;
		//transform.position = ballPos;

		transform.position = new Vector3 (PlayerPos.x, PlayerPos.y, PlayerPos.z);
	}

	public void reStartFollow()
	{
		Player = GameObject.Find ("Player(Clone)");
	}
}
