using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	static SceneManager _instance = null;

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
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
		Application.LoadLevel ("Stage1-Happy");
    }
    public void ItemButton()
    {

    }
    public void ProfileButton()
    {
		Application.LoadLevel ("POLY_Profile");
    }

}
