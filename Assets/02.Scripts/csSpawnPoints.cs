﻿using UnityEngine;
using System.Collections;

public class csSpawnPoints : MonoBehaviour {
	GameObject[] enemies1;
    GameObject[] enemies2;
	public GameObject enemy1;
    public GameObject enemy2;

	private Vector3 spawn1target;
    private Vector3 spawn2target;
	Vector3 spawnPoint1;
    Vector3 spawnPoint2;

	Transform[] monstercount;

	bool isSpawn = true;

	private int Num;
	public int enemySize1;
    public int enemySize2;

	// Use this for initialization
	void Start () {
		//spawnCount = 10;

		enemies1 = new GameObject[enemySize1];
        enemies2 = new GameObject[enemySize2];
	
		spawn1target = gameObject.transform.position;
        spawn2target = gameObject.transform.position + new Vector3(0.5f, 0.0f, 0.5f);

		for (int i = 0; i < enemySize1; ++i) {
			enemies1 [i] = Instantiate (enemy1)as GameObject;
			enemies1 [i].transform.parent = gameObject.transform;
			enemies1 [i].SetActive (false);
		}
        for (int i = 0; i < enemySize2; ++i)
        {
            enemies2[i] = Instantiate(enemy2) as GameObject;
            enemies2[i].transform.parent = gameObject.transform;
            enemies2[i].SetActive(false);
        }
        //pointsSpawn();
    }
	void pointsSpawn()
	{
		for (int i = 0; i < enemySize1; ++i) {

			spawnPoint1 = spawn1target;

			spawnPoint1 = spawnPoint1
			+ Vector3.forward * Random.Range (-4, 4)
			+ Vector3.right * Random.Range (-4, 4)
			+ Vector3.up * 1.0f;

			enemies1 [i].transform.position = spawnPoint1;
			enemies1 [i].SetActive (true);
		}
        for (int i = 0; i < enemySize2; ++i)
        {

            spawnPoint2 = spawn2target;

            spawnPoint2 = spawnPoint2
            + Vector3.forward * Random.Range(-7, 7)
            + Vector3.right * Random.Range(-7, 7)
            + Vector3.up * 1.0f;

            enemies2[i].transform.position = spawnPoint2;
            enemies2[i].SetActive(true);
        }
    }

	// Update is called once per frame
	void Update () {
		if (isSpawn == false) {
			monstercount = gameObject.GetComponentsInChildren<Transform> ();
			if (monstercount.Length == 1) {

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
