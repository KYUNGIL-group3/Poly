using UnityEngine;
using System.Collections;

public class BossSummonMonsterPoint : MonoBehaviour {
    public GameObject[] enemies1;
    public GameObject[] enemies2;
    public GameObject[] SwordMans;
    public GameObject[] SpearMans;
    public GameObject[] CrossbowWarriors;
    public GameObject[] Hammers;
       
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject SwordMan;
    public GameObject SpearMan;
    public GameObject CrossbowWarrior;
    public GameObject Hammer;

    private Vector3 spawntarget;
    Vector3 spawnPoint;
    public int enemySize1;
    public int enemySize2;
    public int enemySize3;
    public int enemySize4;
    public int enemySize5;
    public int enemySize6;

    public float summonRange = 4.0f;
    // Use this for initialization
    void Start () {
        enemies1 = new GameObject[enemySize1];
        enemies2 = new GameObject[enemySize2];
        SwordMans = new GameObject[enemySize3];
        SpearMans = new GameObject[enemySize4];
        CrossbowWarriors = new GameObject[enemySize5];
        Hammers = new GameObject[enemySize6];

        spawntarget = gameObject.transform.position;
        
        //for (int i = 1; i < enemies.Length; ++i)
        //{
        //    enemies[i].gameObject.SetActive(false);
        //}

    }
    public void pointsSpawn()
    {

        AudioManager.Instance().PlayMonsterSpawnSound();
        //enemy1
        for (int i = 0; i < enemySize1; ++i)
        {
            enemies1[i] = Instantiate(enemy1) as GameObject;
            enemies1[i].transform.parent = gameObject.transform;
            enemies1[i].SetActive(false);
        }
        for (int i = 0; i < enemySize1; ++i)
        {

            spawnPoint = spawntarget;

            spawnPoint = spawnPoint
                + Vector3.forward * Random.Range(-summonRange, summonRange)
                + Vector3.right * Random.Range(-summonRange, summonRange)
            + Vector3.up * 1.0f;

            enemies1[i].transform.position = spawnPoint;
            enemies1[i].SetActive(true);
        }
        //enemy2
        for (int i = 0; i < enemySize2; ++i)
        {
            enemies2[i] = Instantiate(enemy2) as GameObject;
            enemies2[i].transform.parent = gameObject.transform;
            enemies2[i].SetActive(false);
        }
        for (int i = 0; i < enemySize2; ++i)
        {

            spawnPoint = spawntarget;

            spawnPoint = spawnPoint
                + Vector3.forward * Random.Range(-summonRange, summonRange)
                + Vector3.right * Random.Range(-summonRange, summonRange)
            + Vector3.up * 1.0f;

            enemies2[i].transform.position = spawnPoint;
            enemies2[i].SetActive(true);
        }
        //SwordMan
        for (int i = 0; i < enemySize3; ++i)
        {
            SwordMans[i] = Instantiate(SwordMan) as GameObject;
            SwordMans[i].transform.parent = gameObject.transform;
            SwordMans[i].SetActive(false);
        }
        for (int i = 0; i < enemySize3; ++i)
        {

            spawnPoint = spawntarget;

            spawnPoint = spawnPoint
                + Vector3.forward * Random.Range(-summonRange, summonRange)
                + Vector3.right * Random.Range(-summonRange, summonRange)
            + Vector3.up * 1.0f;

            SwordMans[i].transform.position = spawnPoint;
            SwordMans[i].SetActive(true);
        }
        //SpearMan
        for (int i = 0; i < enemySize4; ++i)
        {
            SpearMans[i] = Instantiate(SwordMan) as GameObject;
            SpearMans[i].transform.parent = gameObject.transform;
            SpearMans[i].SetActive(false);
        }
        for (int i = 0; i < enemySize4; ++i)
        {

            spawnPoint = spawntarget;

            spawnPoint = spawnPoint
                + Vector3.forward * Random.Range(-summonRange, summonRange)
                + Vector3.right * Random.Range(-summonRange, summonRange)
            + Vector3.up * 1.0f;

            SpearMans[i].transform.position = spawnPoint;
            SpearMans[i].SetActive(true);
        }
        //Crossbow Warrior
        for (int i = 0; i < enemySize5; ++i)
        {
            CrossbowWarriors[i] = Instantiate(SwordMan) as GameObject;
            CrossbowWarriors[i].transform.parent = gameObject.transform;
            CrossbowWarriors[i].SetActive(false);
        }
        for (int i = 0; i < enemySize5; ++i)
        {

            spawnPoint = spawntarget;

            spawnPoint = spawnPoint
                + Vector3.forward * Random.Range(-summonRange, summonRange)
                + Vector3.right * Random.Range(-summonRange, summonRange)
            + Vector3.up * 1.0f;

            CrossbowWarriors[i].transform.position = spawnPoint;
            CrossbowWarriors[i].SetActive(true);
        }
        //Hammer
        for (int i = 0; i < enemySize6; ++i)
        {
            Hammers[i] = Instantiate(SwordMan) as GameObject;
            Hammers[i].transform.parent = gameObject.transform;
            Hammers[i].SetActive(false);
        }
        for (int i = 0; i < enemySize6; ++i)
        {

            spawnPoint = spawntarget;

            spawnPoint = spawnPoint
                + Vector3.forward * Random.Range(-summonRange, summonRange)
                + Vector3.right * Random.Range(-summonRange, summonRange)
            + Vector3.up * 1.0f;

            Hammers[i].transform.position = spawnPoint;
            Hammers[i].SetActive(true);
        }

        //for (int i = 1; i < enemies.Length; ++i)
        //{
        //    enemies[i].gameObject.SetActive(true);
        //}

    }
    // Update is called once per frame
    void Update () {
	
	}
}
