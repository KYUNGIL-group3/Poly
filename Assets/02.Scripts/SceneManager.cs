using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames;

public class SceneManager : MonoBehaviour {
	static SceneManager _instance = null;
	private bool bWaitingForAuth = false;
	public int _killCount;
	public int _DeadCount;
	public float _PlayTimefloat;
	public int _Piece;
	public int _Weapon1;
	public int _Weapon2;
	public int ClearStage;
	public int Volume;

	string Sstr;
	string Mstr;
  
    public static SceneManager Instance()
	{
		return _instance;
	}

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt ("FristAccount",0);
		//PlayerPrefs.SetInt ("ClearStage", 0);
		Volume = PlayerPrefs.GetInt ("Volume");
		_Weapon1 = PlayerPrefs.GetInt ("Weapon1");
		_Weapon2 = PlayerPrefs.GetInt ("Weapon2");
		_killCount = PlayerPrefs.GetInt ("KillCount");
		_DeadCount = PlayerPrefs.GetInt ("DeadCount");
		_Piece = PlayerPrefs.GetInt ("Piece");
		ClearStage =  PlayerPrefs.GetInt ("ClearStage");
        BgmManager.Instance().PlayMainUISound();


		GooglePlayGames.PlayGamesPlatform.Activate ();
		doLogin ();
	}


	// Update is called once per frame
	void Update () {

        
        //_PlayTimefloat += Time.unscaledDeltaTime;
        //PlayerPrefs.SetFloat ("PlayTime", _PlayTimefloat);

    }

	void SetPrefs()
	{
		Volume = PlayerPrefs.GetInt ("Volume");
		_Weapon1 = PlayerPrefs.GetInt ("Weapon1");
		_Weapon2 = PlayerPrefs.GetInt ("Weapon2");
		_killCount = PlayerPrefs.GetInt ("KillCount");
		_DeadCount = PlayerPrefs.GetInt ("DeadCount");
		_Piece = PlayerPrefs.GetInt ("Piece");
		ClearStage =  PlayerPrefs.GetInt ("ClearStage");
	}

	bool StageNum(int mystage)
	{
		if (ClearStage >= mystage) {
			return true;
		} else {
			return false;
		}
	}

	void SetClearStage(int nowStage)
	{
		if (PlayerPrefs.GetInt ("ClearStage") < nowStage) {
			PlayerPrefs.SetInt ("ClearStage", nowStage);

		}
	}

	public void KillCountSet(int count)
	{
		int Allcount = PlayerPrefs.GetInt ("KillCount");
		Allcount += count;
		PlayerPrefs.SetInt ("KillCount" , Allcount);
		doLeaderboardKillScore (Allcount);
	}

	public void DeadCountSet(int count)
	{
		int Allcount = PlayerPrefs.GetInt ("DeadCount");
		Allcount += count;
		PlayerPrefs.SetInt ("DeadCount" , Allcount);

	}

	public void AddPlayTime(float time)
	{
		_PlayTimefloat += time;
		PlayerPrefs.SetFloat ("PlayTime", _PlayTimefloat);
	}

	public void Weapon1Set(int WeaponNum)
	{
		PlayerPrefs.SetInt ("Weapon1" , WeaponNum);
	}

	public void Weapon2Set(int WeaponNum)
	{
		PlayerPrefs.SetInt ("Weapon2" , WeaponNum);
	}

	public int Weapon1Get()
	{
		return PlayerPrefs.GetInt ("Weapon1");
	}

	public int Weapon2Get()
	{
		return PlayerPrefs.GetInt ("Weapon2");
	}

	bool HaveWeapon(int WeaponNum)
	{
		switch (WeaponNum) {
		case 0:
			if (PlayerPrefs.GetInt ("WeaponNum1") == 1) {
				return true;
			} else {
				return false;
			}
			break;

		case 1:
			if (PlayerPrefs.GetInt ("WeaponNum2") == 1) {
				return true;
			} else {
				return false;
			}
			break;
		case 2:
			if (PlayerPrefs.GetInt ("WeaponNum3") == 1) {
				return true;
			} else {
				return false;
			}
			break;

		case 3:
			if (PlayerPrefs.GetInt ("WeaponNum4") == 1) {
				return true;
			} else {
				return false;
			}
			break;

		case 4:
			if (PlayerPrefs.GetInt ("WeaponNum5") == 1) {
				return true;
			} else {
				return false;
			}
			break;
		}
		return false;
	}

    public void StartButton()
    {
        AudioManager.Instance().PlayStartTouchSound();
		if (PlayerPrefs.GetInt ("FristAccount") == 0) {
			Application.LoadLevel ("POLY_Synopsys");
		} else {
			
			Application.LoadLevel ("POLY_ROBY");
		}
    }
    public void ShopButton()
    {

    }
	public void StageButton()
	{
        AudioManager.Instance().PlayStartTouchSound();
        Application.LoadLevel ("POLY_stage");
	}
	public void Stage1()
	{
        AudioManager.Instance().PlayStartTouchSound();
        Application.LoadLevel ("stage1-Happy");
        BgmManager.Instance().PlayHappySound();
	}
	public void Stage2()
	{
        AudioManager.Instance().PlayStartTouchSound();
        Application.LoadLevel ("stage2-Enjoy");
        BgmManager.Instance().PlayEnjoySound();
    }
	public void Stage3()
	{
        AudioManager.Instance().PlayStartTouchSound();
        Application.LoadLevel ("stage3-Sadness");
        BgmManager.Instance().PlaySadnessSound();
    }
	public void Stage4()
	{
        AudioManager.Instance().PlayStartTouchSound();
        Application.LoadLevel ("stage4-angry");
        BgmManager.Instance().PlayAngrySound();
        
    }
	public void Stage5()
	{
        AudioManager.Instance().PlayStartTouchSound();
        Application.LoadLevel ("stage5-fear");
        BgmManager.Instance().PlayFearSound();
       
    }

	public void WeaponButton()
    {
        AudioManager.Instance().PlayStartTouchSound();
        Application.LoadLevel ("POLY_WEAPON");
    }
    public void ProfileButton()
    {
        AudioManager.Instance().PlayStartTouchSound();
        Application.LoadLevel ("POLY_Profile");
    }

	public string PlayTime(float inputplaytime)
	{
		int S = (int)(inputplaytime % 60);
		int M = (int)((inputplaytime / 60) % 60);
		int H = (int)(((inputplaytime / 60) / 60));

		Sstr = ""+S;
		Mstr = ""+M;

		if (S < 10) {
			Sstr = "0" + S;
		}

		if (M < 10) {
			Mstr = "0" + M;
		}

		string timestr = H + ":" + Mstr + ":" + Sstr;

		return timestr;
	}

	void doLogin()
	{
		if (bWaitingForAuth) {
			return;
		}
		if (!Social.localUser.authenticated) {
			// Authenticate
			bWaitingForAuth = true;

			Social.localUser.Authenticate((bool success) => {
				bWaitingForAuth = false;
				if (success) {
					string token = GooglePlayGames.PlayGamesPlatform.Instance.GetToken();
				} else {
					//mStatusText = Social.localUser.userName +  " : failed";
				}
			});
		}
	}

	public void doAchievementShow()
	{
		Social.ShowAchievementsUI();
	}

	public void doLeaderboardShow()
	{
		Social.ShowLeaderboardUI();
	}

	public void doAchievementStage1()
	{

		string unLockAchievement_id = "CgkIgoLo6cUQEAIQAA";

		Social.ReportProgress(unLockAchievement_id, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void doAchievementStage2()
	{

		string unLockAchievement_id = "CgkIgoLo6cUQEAIQAQ";

		Social.ReportProgress(unLockAchievement_id, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void doAchievementStage3()
	{

		string unLockAchievement_id = "CgkIgoLo6cUQEAIQAg";

		Social.ReportProgress(unLockAchievement_id, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void doAchievementStage4()
	{

		string unLockAchievement_id = "CgkIgoLo6cUQEAIQAw";

		Social.ReportProgress(unLockAchievement_id, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void doAchievementStage5()
	{

		string unLockAchievement_id = "CgkIgoLo6cUQEAIQBA";

		Social.ReportProgress(unLockAchievement_id, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void doLeaderboardKillScore(int myScore)
	{
		string leader_board_id = "";
		#if UNITY_ANDROID
		leader_board_id = "CgkIgoLo6cUQEAIQBQ";
		#elif UNITY_IPHONE
		leader_board_id = "SOCIALTEST_HIGH_SCORE";
		#endif

		Social.ReportScore(myScore, leader_board_id,
			(bool success) =>
			{
				#if UNITY_ANDROID
				//PlayGamesPlatform.Instance.ShowLeaderboardUI(leader_board_id);
				#elif UNITY_IPHONE
				Social.ShowLeaderboardUI();
				#endif
			}
		);
	}
}
