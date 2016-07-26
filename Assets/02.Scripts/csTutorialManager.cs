using UnityEngine;
using System.Collections;

public class csTutorialManager : MonoBehaviour {
	public GameObject[] tutorial;
	int i = 0;
	// Use this for initialization
	void Start () {
		tutorial [0].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {

			tutorial [i].SetActive (false);
			i++;
			if (i + 1 > tutorial.Length) {
				gameObject.SetActive (false);
			} else {
				tutorial [i].SetActive (true);
			}
		}
	}
}
