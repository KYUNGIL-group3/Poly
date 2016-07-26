using UnityEngine;
using System.Collections;

public class HappyBgm : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioManager.Instance().PlayHappySound();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
