using UnityEngine;
using System.Collections;

public class csWallDestroy : MonoBehaviour {

	public GameObject Wall;
	public GameObject Wall2;
	public GameObject DestroyObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//보스생성 조건 추가

		Transform[] spawnPointCount = gameObject.GetComponentsInChildren<Transform> ();
		if (!Wall2) {
			if (spawnPointCount.Length == 2) {
				if (DestroyObj) {
					Destroy (DestroyObj.gameObject);
				}
				Destroy (Wall.gameObject);
				Destroy (gameObject);
			}
		} else {
			if (spawnPointCount.Length == 3) {
				if (DestroyObj) {
					Destroy (DestroyObj.gameObject);
				}
				Destroy (Wall.gameObject);
				Destroy (Wall2.gameObject);
				Destroy (gameObject);
			}
		}
	}
}
