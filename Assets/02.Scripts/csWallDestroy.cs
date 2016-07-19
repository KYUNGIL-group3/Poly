using UnityEngine;
using System.Collections;

public class csWallDestroy : MonoBehaviour {

	public GameObject[] WallList;
	public GameObject DownObj;
	public GameObject CreateWrapGate;

	// Use this for initialization
	void Start () {
		if (CreateWrapGate) {
			CreateWrapGate.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//보스생성 조건 추가

		Transform[] spawnPointCount = gameObject.GetComponentsInChildren<Transform> ();

		if (DownObj) {
			if (DownObj.transform.position.y < 2.0f) {
				return;
			}
			if (spawnPointCount.Length == WallList.Length + 1) {
				DownObj.transform.position += Vector3.down * 1.0f * Time.deltaTime;
				DownObj.tag = "Map";

				if (CreateWrapGate) {
					CreateWrapGate.SetActive(true);
				}
			}
			return;
		}

		if (spawnPointCount.Length == WallList.Length + 1) {
			for (int i = 0; i < WallList.Length; i++) {
				Destroy (WallList [i].gameObject);
			}
			if (CreateWrapGate) {
				CreateWrapGate.SetActive(true);
			}

			Destroy (gameObject);
		}
	}
}
