using UnityEngine;
using System.Collections;

public class csLockContents : MonoBehaviour {

	public GameObject content2;
	public GameObject content3;
	public GameObject content4;
	public GameObject content5;

	// Use this for initialization
	void Start () {

		int ClearStage = PlayerPrefs.GetInt ("ClearStage");

		switch (ClearStage) {
		case 6:
			content5.SetActive (false);
			content4.SetActive (false);
			content3.SetActive (false);
			content2.SetActive (false);
			break;
		case 5:
			content5.SetActive (false);
			content4.SetActive (false);
			content3.SetActive (false);
			content2.SetActive (false);
			break;
		case 4:
			content4.SetActive (false);
			content3.SetActive (false);
			content2.SetActive (false);
			break;
		case 3:
			content3.SetActive (false);
			content2.SetActive (false);
			break;
		case 2:
			content2.SetActive (false);
			break;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
