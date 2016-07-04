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
		Debug.Log ("GameOver");
	}

	public void GameClear()
	{
		Debug.Log ("GameClear");
	}

	public void PlayerHealth(int hp)
	{
		this.hp = hp;
	}


	public void SkillGauge(int gauge)
	{
		this.gauge = gauge;
	}

}
