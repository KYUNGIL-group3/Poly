using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif


public class GameManager : MonoBehaviour {

	public GameObject PlayerPrefeb;
	static GameManager _instance = null;
	Slider healthBarSlider;
	Slider skillBarSlider;
    public GameObject recovery; 
	GameObject Player;
	public GameObject OptionCamvas;
	public GameObject VictoryCamvas;
	public GameObject FailCamvas;
	public GameObject ResurrectionCanvas;

	public GameObject Boss;
	public GameObject AllSpwanPoint;
	bool once = true;

	public int Stagekillcount = 0;
	public int StageDeadcount = 0;
	public float PlayTime = 0.0f;
	public int restartcount = 0;

	public Vector3 DeadPosition;

	int count = 0;

	public static GameManager Instance()
	{
		return _instance;
	}

	public int hp;
	public int gauge;
    public int maxHp = 1000;
    public int maxGauge = 100;

	public bool isGameOver = false;
	public bool isGameClear = false;

	public int weapon1num = 0;
	public int weapon2num = 1;

	float tencount = 10.0f;
	float timecount = 0.0f;

	public int countmons;

	public bool isTimeControl = false;

    // Use this for initialization

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
		}
		weapon1num = SceneManager.Instance ().Weapon1Get ();
		weapon2num = SceneManager.Instance ().Weapon2Get ();

    }

	void Start () {
		Player = GameObject.Find ("Player");
		healthBarSlider = GameObject.Find ("Hp").GetComponent<Slider> ();
		skillBarSlider = GameObject.Find ("SkillGauge").GetComponent<Slider> ();
        hp = maxHp;
        gauge = 0;

		OptionCamvas.SetActive (false);
		//weapon1num = 0;
		//weapon2num = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (once) {
			if (!Boss) {
				once = false;
				GameClear ();

			}
		} else {
			return;
		}

		PlayTime += Time.unscaledDeltaTime;

		timecount += Time.deltaTime;
		if (timecount > tencount) {
			timecount = 0.0f;
			SkillGauge (1);
		}
		healthBarSlider.value = hp;

		skillBarSlider.value = gauge;
	}

	public void GameOver()
	{
        
		if (isGameOver)
		return;

		DeadPosition = Player.transform.position;

		AddDeadCount(1);

		SceneManager.Instance ().AddPlayTime (PlayTime);
		SceneManager.Instance ().KillCountSet (Stagekillcount);
		SceneManager.Instance ().DeadCountSet (StageDeadcount);
		if (restartcount == 0) {
			Resurrection ();
		} else {
			Fail ();
		}

        BgmManager.Instance().PlayFail();
        isGameOver = true;
		Time.timeScale = 0.5f;
	}

	public void GameClear()
	{
       

		if (isGameClear)
			return;

		Transform[] DesSpwanPoint = AllSpwanPoint.GetComponentsInChildren<Transform> ();
		for (int i = 1; i < DesSpwanPoint.Length; i++) {
			if(DesSpwanPoint[i].gameObject.GetComponent<csEnemy1> ())
				DesSpwanPoint[i].gameObject.GetComponent<csEnemy1> ().Damage(10000);
			if(DesSpwanPoint[i].gameObject.GetComponent<csEnemy2> ())
				DesSpwanPoint[i].gameObject.GetComponent<csEnemy2> ().Damage(10000);
			if (DesSpwanPoint[i].gameObject.GetComponent<csHardMonster>())
				DesSpwanPoint[i].gameObject.GetComponent<csHardMonster>().Damage(10000);
			if (DesSpwanPoint[i].gameObject.GetComponent<csHardMonster2>())
				DesSpwanPoint[i].gameObject.GetComponent<csHardMonster2>().Damage(10000);
			if (DesSpwanPoint[i].gameObject.GetComponent<csBossMonster>())
				DesSpwanPoint[i].gameObject.GetComponent<csBossMonster>().Damage(10000);
		}

		isGameClear = true;

		SceneManager.Instance ().AddPlayTime (PlayTime);
		SceneManager.Instance ().KillCountSet (Stagekillcount);
		SceneManager.Instance ().DeadCountSet (StageDeadcount);

		Victory ();
        BgmManager.Instance().PlayClear();
        switch (Application.loadedLevelName) {
		case "stage1-Happy":
                
			if (PlayerPrefs.GetInt ("ClearStage") < 2) {
				PlayerPrefs.SetInt ("ClearStage", 2);
				PlayerPrefs.SetInt ("Weapon1", 0);
				PlayerPrefs.SetInt ("Weapon2", 1);
			}
			break;
		case "stage2-Enjoy":
			if (PlayerPrefs.GetInt ("ClearStage") < 3) {
				PlayerPrefs.SetInt ("ClearStage", 3);
			}
			break;
		case "stage3-Sadness":
			if (PlayerPrefs.GetInt ("ClearStage") < 4) {
				PlayerPrefs.SetInt ("ClearStage", 4);
			}
			break;
		case "stage4-angry":
			if (PlayerPrefs.GetInt ("ClearStage") < 5) {
				PlayerPrefs.SetInt ("ClearStage", 5);
			}
			break;
		case "stage5-fear":
			if (PlayerPrefs.GetInt ("ClearStage") < 6) {
				PlayerPrefs.SetInt ("ClearStage", 6);
			}

			if (PlayerPrefs.GetInt ("FristEnding") == 0) {
				PlayerPrefs.SetInt ("FristEnding", 1);
				Application.LoadLevel ("POLY_Ending");
			}
			break;

		}

		Time.timeScale = 0.5f;
	}

	public void PlayerHealth(int hp)
	{
		if (isTimeControl) {
			return;
		}

		this.hp -= hp;
        AudioManager.Instance().PlayPlayerHitSound();   //플레이어 피격 사운드 재생
        Player.GetComponent<csPlayerController>().DamageEF();
        if (this.hp < 0) {
			this.hp = 0;
			GameOver ();
		}
		if (this.hp > maxHp) {
			this.hp = maxHp;
		}
	}
    public void RecoveryHealth(int hp)
    {
        this.hp += hp;
        if (this.hp > 1000)
        {
            this.hp = 1000;
        }
    }
    public void SpawnHealthItem(Vector3 pos)
    {
        Instantiate(recovery,pos+new Vector3(0.0f,1.0f,0.0f),Quaternion.identity);
    }

    public void useSkillGauge(int count)
    {
        this.gauge -= count*5;

    }


	public void SkillGauge(int gauge)
	{
		this.gauge += gauge;
		AddKillCount (1);
		if (this.gauge > maxGauge) {
			this.gauge = maxGauge;
		}
	}

	public int Weapon1Num()
	{
		return weapon1num;
	}

	public int Weapon2Num()
	{
		return weapon2num;
	}

	public void OptionButton(){
		if (isTimeControl) {
			return;
		}
		Time.timeScale = 0.0f;
		OptionCamvas.SetActive (true);
	}

	public void Victory(){
       
		VictoryCamvas.SetActive (true);
	}

	public void Fail(){
       
		FailCamvas.SetActive (true);
	}

	public void Resurrection()
	{
		ResurrectionCanvas.SetActive (true);
	}

	public void ResurrectionPlayer()
	{
		restartcount++;
		Time.timeScale = 1.0f;
		Instantiate (PlayerPrefeb, DeadPosition, Quaternion.identity);
		Player = GameObject.Find ("Player(Clone)");
		GetComponent<csCameraFollow> ().reStartFollow ();
		hp = maxHp;
		isGameOver = false;
		isGameClear = false;
		once = true;

	}

	public void NoResurrection()
	{
		Fail ();
	}

	public void abncc()
	{
		count++;
		Debug.Log (count);
	}

	public void AddKillCount(int killcount)
	{
		Stagekillcount += killcount;
	}

	public void AddDeadCount(int deadcount)
	{
		StageDeadcount += deadcount;
	}


	public void ShowRewarededAd()
	{
		#if UNITY_ADS
		if(Advertisement.IsReady())
		{
			ShowOptions options = new ShowOptions();
			options.resultCallback = HandleShowResult;
			Advertisement.Show(null, options);
		}
		#endif
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result) {
		case ShowResult.Finished:
			ResurrectionPlayer ();

			break;
		case ShowResult.Skipped:
			break;
		case ShowResult.Failed:
			break;
		}
	}
}
