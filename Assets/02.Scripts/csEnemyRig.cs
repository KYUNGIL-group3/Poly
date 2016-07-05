using UnityEngine;
using System.Collections;

public class csEnemyRig : MonoBehaviour {
	Transform player;

	public float speed;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, player.position, step);
	}
}
