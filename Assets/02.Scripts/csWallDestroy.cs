using UnityEngine;
using System.Collections;

public class csWallDestroy : MonoBehaviour {

	public GameObject[] WallList;
	public GameObject DestroyObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//보스생성 조건 추가

		Transform[] spawnPointCount = gameObject.GetComponentsInChildren<Transform> ();

		if (DestroyObj) {
			if (DestroyObj.transform.position.y < 2.0f) {
				return;
			}
			if (spawnPointCount.Length == WallList.Length + 1) {
				DestroyObj.transform.position += Vector3.down * 1.0f * Time.deltaTime;
				DestroyObj.tag = "Map";
			}
			return;
		}

		if (spawnPointCount.Length == WallList.Length + 1) {
			for (int i = 0; i < WallList.Length; i++) {
				Destroy (WallList [i].gameObject);
			}

			Destroy (gameObject);
		}
	}
}
