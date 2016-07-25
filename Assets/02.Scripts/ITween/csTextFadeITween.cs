using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csTextFadeITween : MonoBehaviour {
	
	public Text[] ITweenText;

	// Use this for initialization
	void Start () {
		StartCoroutine (FadeOutIn());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator FadeOutIn()
	{
		for (int i = 0; i < ITweenText.Length; i++) {

		}
	}
}
