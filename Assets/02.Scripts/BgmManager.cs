using UnityEngine;
using System.Collections;

public class BgmManager : MonoBehaviour {
    public AudioClip MainUISound;   //메인    적용
    public AudioClip HappySound;    //스테이지1 적용
    public AudioClip EnjoySound;    //스테이지2 적용
    public AudioClip SadnessSound;    //스테이지3   적용
    public AudioClip AngrySound;    //스테이지4 적용
    public AudioClip FearSound;    //스테이지5  적용
    static BgmManager _instance = null;
    public static BgmManager Instance()
    {
        return _instance;
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void PlayMainUISound()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = MainUISound;
        GetComponent<AudioSource>().Play();
    }
    public void PlayHappySound()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = HappySound;
        GetComponent<AudioSource>().Play();
    }
    public void PlayEnjoySound()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = EnjoySound;
        GetComponent<AudioSource>().Play();
    }
    public void PlaySadnessSound()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = SadnessSound;
        GetComponent<AudioSource>().Play();
    }
    public void PlayAngrySound()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = AngrySound;
        GetComponent<AudioSource>().Play();
    }
    public void PlayFearSound()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = FearSound;
        GetComponent<AudioSource>().Play();
    }

    public void PlayClear()
    {
        GetComponent<AudioSource>().Stop();
        AudioManager.Instance().PlayStageClearSound();
    }
    public void PlayFail()
    {
        GetComponent<AudioSource>().Stop();
        AudioManager.Instance().PlayStageFailSound();
    }
}
