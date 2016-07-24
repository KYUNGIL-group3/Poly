using UnityEngine;
using System.Collections;

public class csSpawnPoint : MonoBehaviour {
	public Transform[] enemies;

	bool isSpawn = true;

	// Use this for initialization
	void Start () {
		enemies = GetComponentsInChildren<Transform> ();
		for (int i = 1; i < enemies.Length; ++i) {
			enemies [i].gameObject.SetActive (false);
		}

		//pointsSpawn();
	}
	public void pointsSpawn()
	{
		for (int i = 1; i < enemies.Length; ++i) {
			enemies [i].gameObject.SetActive (true);
		}
	}

	// Update is called once per frame
	void Update () {
		if (isSpawn == false) {
			enemies = gameObject.GetComponentsInChildren<Transform> ();
			if (enemies.Length == 1) {

				Destroy (gameObject);
			}
		}

	}
	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag=="Player")
		if(isSpawn){
			pointsSpawn ();
			isSpawn = false;
		}
	}
}
