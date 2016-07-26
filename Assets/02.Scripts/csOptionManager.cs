using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csOptionManager : MonoBehaviour {

	public Text scoretext;

	string weapon1;
	string weapon2;
	// Use this for initialization
	void Start () {
		if (scoretext == null) {
			return;
		}

		switch (SceneManager.Instance().Weapon1Get()) {
		case 0:
			weapon1 = "한손검";
			break;
		case 1:
			weapon1 = "쌍검";
			break;
		case 2:
			weapon1 = "창";
			break;
		case 3:
			weapon1 = "석궁";
			break;
		case 4:
			weapon1 = "해머";
			break;
		}

		switch (SceneManager.Instance().Weapon2Get()) {
		case 0:
			weapon2 = "한손검";
			break;
		case 1:
			weapon2 = "쌍검";
			break;
		case 2:
			weapon2 = "창";
			break;
		case 3:
			weapon2 = "석궁";
			break;
		case 4:
			weapon2 = "해머";
			break;
		}

		if (SceneManager.Instance ().Weapon1Get () == SceneManager.Instance ().Weapon2Get ()) {
			weapon2 = "X";
		}

		if (Application.loadedLevelName == "POLY_Profile") {
			ProfileScene ();
		} else if (Application.loadedLevelName.Substring(0,5) == "stage"
			||Application.loadedLevelName.Substring(0,5) == "Stage") {
			GameEndScene ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ProfileScene()
	{
		int killscore = PlayerPrefs.GetInt ("KillCount");
		int deadscore = PlayerPrefs.GetInt ("DeadCount");
		float playtime = PlayerPrefs.GetFloat ("PlayTime");


		scoretext.text = killscore + "EA\n" +
			deadscore + "EA\n" +
			SceneManager.Instance().PlayTime(playtime) +"\n" +
			"["+ weapon1 +"]\n" +
			"["+ weapon2 +"]";
	}

	void GameEndScene()
	{
		int killscore = GameManager.Instance ().Stagekillcount;
		int deadscore = GameManager.Instance ().StageDeadcount;
		float playtime = GameManager.Instance ().PlayTime;

		scoretext.text = killscore + "EA\n" +
			deadscore + "EA\n" +
			SceneManager.Instance().PlayTime(playtime) +"\n" +
			"["+ weapon1  +"]\n" +
			"["+ weapon2  +"]";
	}

	public void ContinueButton()
	{
		gameObject.SetActive (false);
		Time.timeScale = 1.0f;
	}

	public void ExitButton()
	{
        AudioManager.Instance().PlayUIButtonTouchSound();
		Application.LoadLevel ("POLY_stage");
		Time.timeScale = 1.0f;
        BgmManager.Instance().PlayMainUISound();
    }

	public void RetryButton()
	{
        AudioManager.Instance().PlayUIButtonTouchSound();
		Application.LoadLevel (Application.loadedLevelName);

        switch (Application.loadedLevelName)
        {
            case "stage1-Happy":
                
                BgmManager.Instance().PlayHappySound();
                break;
            case "stage2-Enjoy":
                
                BgmManager.Instance().PlayEnjoySound();
                break;
            case "stage3-Sadness":
               
                BgmManager.Instance().PlaySadnessSound();
                break;
            case "stage4-angry":
                
                BgmManager.Instance().PlayAngrySound();
                break;
            case "stage5-fear":
                
                BgmManager.Instance().PlayFearSound();
                break;

        }
        Time.timeScale = 1.0f;
        
	}

	public void WeaponSelletButton()
	{
        AudioManager.Instance().PlayUIButtonTouchSound();
        Application.LoadLevel ("POLY_WEAPON");
		Time.timeScale = 1.0f;
        BgmManager.Instance().PlayMainUISound();
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
			Application.LoadLevel ("stage5-fear");
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
