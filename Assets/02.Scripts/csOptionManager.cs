using UnityEngine;
using System.Collections;

public class csOptionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ContinueButton()
	{
		gameObject.SetActive (false);
		Time.timeScale = 1.0f;
	}

	public void ExitButton()
	{
		Application.LoadLevel ("POLY_stage");
		Time.timeScale = 1.0f;
	}

	public void RetryButton()
	{
		Application.LoadLevel (Application.loadedLevelName);
		Time.timeScale = 1.0f;
	}

	public void NextButton()
	{
		switch (Application.loadedLevelName) {
		case "stage1-Happy":
			Application.LoadLevel ("stage2-Enjoy");
			break;
		case "stage2-Enjoy":
			Application.LoadLevel ("stage3-Sadness");
			break;
		case "stage3-Sadness":
			Application.LoadLevel ("stage4-angry");
			break;
		case "stage4-angry":
			//Application.LoadLevel ("Stage5");
			break;


		}
		//Application.LoadLevel (Application.loadedLevelName);
		Time.timeScale = 1.0f;
	}

	public void soundButton(int value)
	{
		int volume = PlayerPrefs.GetInt ("Volume");

		volume += value;

		if (volume < 0) {
			volume = 0;
		}
		if (volume > 100) {
			volume = 100;
		}

		PlayerPrefs.SetInt ("Volume", volume);
	}
}
