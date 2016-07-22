using UnityEngine;
using System.Collections;

public class csITweenManagerButton : MonoBehaviour {

	public GameObject[] ITweenObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActionStart()
	{
		for (int i = 0; i < ITweenObj.Length; i++) {
			ITweenObj [i].GetComponent<csButtonITween> ().UIMove ();
		}
	}
}
