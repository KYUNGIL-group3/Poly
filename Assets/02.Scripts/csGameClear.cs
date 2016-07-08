using UnityEngine;
using System.Collections;

public class csGameClear : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//보스생성 조건 추가

		Transform[] spawnPointCount = gameObject.GetComponentsInChildren<Transform> ();
		if (spawnPointCount.Length == 1) {
			

			GameManager.Instance ().GameClear ();
		}
	}
}
