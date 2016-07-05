using UnityEngine;
using System.Collections;

public class csEnemyRig : MonoBehaviour {
	Transform player;

	public float speed;
	private float distance;
	Transform target;
	CharacterController enemyController;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("MoveSkillObj").transform;
		enemyController = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance (transform.position, player.position);

		if (distance < 1.0f)
			return;
		else {
			Vector3 dir = player.position - transform.position;
			dir.y = 0.0f;
			dir.Normalize ();
			enemyController.SimpleMove (dir * speed);
		}
		//transform.position = Vector3.MoveTowards (transform.position, player.position, step);

	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "WEAPON") {
			Debug.Log ("피격");
		}

	}
}
