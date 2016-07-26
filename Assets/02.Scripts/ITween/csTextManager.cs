using UnityEngine;
using System.Collections;

public class csTextManager : MonoBehaviour {

	public GameObject[] Text; 

	public GameObject NextTextCanvas;

	// Use this for initialization
	void Start () {
		StartCoroutine (FadeStart ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator FadeStart()
	{
		for (int i = 0; i < Text.Length; i++) {
			Text [i].SetActive (true);
			yield return new WaitForSeconds (5.0f);
		}

		if (NextTextCanvas == null) {
			if (Application.loadedLevelName == "POLY_Synopsys") {
				Application.LoadLevel ("Stage0-Tutorial");
			}
			if (Application.loadedLevelName == "POLY_Ending") {
				Application.LoadLevel ("POLY_ROBY");
			}


		} else {
			NextTextCanvas.SetActive (true);
			gameObject.SetActive (false);
		}
	}

	public void Skip()
	{
		Application.LoadLevel ("Stage0-Tutorial");
	}
}
