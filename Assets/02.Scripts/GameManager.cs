using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	static GameManager _instance = null;
	public Slider healthBarSlider;
	public Slider skillBarSlider;
	public static GameManager Instance()
	{
		return _instance;
	}

	public int hp = 1000;
	public int gauge = 0;

	public bool isGameOver = false;
	public bool isGameClear = false;

	// Use this for initialization
	void Start () {
		if (_instance == null) {
			_instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
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
		if (this.hp < 0) {
			this.hp = 0;
			GameOver ();
		}
		if (this.hp > 1000) {
			this.hp = 1000;
		}
	}


	public void SkillGauge(int gauge)
	{
		this.gauge += gauge;
		if (this.gauge > 100) {
			this.gauge = 100;
		}
	}

}
