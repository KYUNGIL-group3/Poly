using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	static SceneManager _instance = null;

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
		if (PlayerPrefs.GetInt ("FristAccount") == 0) {
			PlayerPrefs.SetInt ("Volume", 50);
			PlayerPrefs.SetInt ("FristAccount", 1);
		}

		Volume = PlayerPrefs.GetInt ("Volume");
		_Weapon1 = PlayerPrefs.GetInt ("Weapon1");
		_Weapon2 = PlayerPrefs.GetInt ("Weapon2");
		_killCount = PlayerPrefs.GetInt ("KillCount");
		_DeadCount = PlayerPrefs.GetInt ("DeadCount");
		_Piece = PlayerPrefs.GetInt ("Piece");
		ClearStage =  PlayerPrefs.GetInt ("ClearStage");
	}
	
	// Update is called once per frame
	void Update () {
		_PlayTimefloat += Time.unscaledDeltaTime;
		PlayerPrefs.SetFloat ("PlayTime", _PlayTimefloat);
		//if (isGamePlay) 
			//PlayTime ();

		if(Input.GetButtonDown("Fire1"))
			{
				PlayTime();
			}
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

	void KillCountSet(int count)
	{
		int Allcount = PlayerPrefs.GetInt ("KillCount");
		Allcount += count;
		PlayerPrefs.SetInt ("KillCount" , count);
	}

	void DeadCountSet(int count)
	{
		int Allcount = PlayerPrefs.GetInt ("DeadCount");
		Allcount += count;
		PlayerPrefs.SetInt ("DeadCount" , count);
	}

	void Weapon1Set(int WeaponNum)
	{
		PlayerPrefs.SetInt ("Weapon1" , WeaponNum);
	}

	void Weapon2Set(int WeaponNum)
	{
		PlayerPrefs.SetInt ("Weapon2" , WeaponNum);
	}

	int Weapon1Get(int WeaponNum)
	{
		return PlayerPrefs.GetInt ("Weapon1");
	}

	int Weapon2Get(int WeaponNum)
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
		Application.LoadLevel ("POLY_ROBY");
    }
    public void ShopButton()
    {

    }
	public void StageButton()
	{
		Application.LoadLevel ("POLY_stage");
	}
	public void Stage1()
	{
		Application.LoadLevel ("stage1-Happy");
	}
	public void Stage2()
	{
		Application.LoadLevel ("stage2-Enjoy");
	}
	public void Stage3()
	{
		Application.LoadLevel ("stage3-Sadness");
	}
	public void Stage4()
	{
		Application.LoadLevel ("stage4-angry");
	}

	public void WeaponButton()
    {
		Application.LoadLevel ("POLY_WEAPON");
    }
    public void ProfileButton()
    {
		Application.LoadLevel ("POLY_Profile");
    }

	string PlayTime()
	{
		int S = (int)(_PlayTimefloat % 60);
		int M = (int)((_PlayTimefloat / 60) % 60);
		int H = (int)(((_PlayTimefloat / 60) / 60));

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

}
