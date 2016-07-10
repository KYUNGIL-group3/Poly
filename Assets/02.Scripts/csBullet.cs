using UnityEngine;
using System.Collections;

public class csBullet : MonoBehaviour {
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
			GameManager.Instance ().PlayerHealth (30);
		}
	}
}
