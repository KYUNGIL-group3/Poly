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

	public void ReplayButton()
	{
		Application.LoadLevel (Application.loadedLevelName);
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
