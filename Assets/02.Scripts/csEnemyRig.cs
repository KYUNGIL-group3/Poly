using UnityEngine;
using System.Collections;

public class csEnemyRig : MonoBehaviour {
	Transform player;

	public float speed;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("MoveSkillObj").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, player.position) < 1.0f)
			return;
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, player.position, step);
	}
}
