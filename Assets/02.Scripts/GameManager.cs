using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	static GameManager _instance = null;
	public Slider healthBarSlider;
	public Slider skillBarSlider;
    public GameObject recovery; 
    GameObject Player;
    
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
       
        hp = maxHp;
        gauge = 0;

		//weapon1num = 0;
		//weapon2num = 1;
	}
	
	// Update is called once per frame
	void Update () {
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

		isGameOver = true;
		Debug.Log ("GameOver");
	}

	public void GameClear()
	{
		if (isGameClear)
			return;

		isGameClear = true;
		Debug.Log ("GameClear");
	}

	public void PlayerHealth(int hp)
	{
		this.hp -= hp;
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

}
