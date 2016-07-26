using UnityEngine;
using System.Collections;

public class BossSummonMonsterPoint : MonoBehaviour {
    public Transform[] enemies;
    // Use this for initialization
    void Start () {
        enemies = GetComponentsInChildren<Transform>();
        for (int i = 1; i < enemies.Length; ++i)
        {
            enemies[i].gameObject.SetActive(false);
        }
    }
    public void pointsSpawn()
    {
        AudioManager.Instance().PlayMonsterSpawnSound();
        for (int i = 1; i < enemies.Length; ++i)
        {
            enemies[i].gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update () {
	
	}
}
