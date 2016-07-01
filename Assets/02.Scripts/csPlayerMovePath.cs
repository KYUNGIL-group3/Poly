using UnityEngine;
using System.Collections;

public class csPlayerMovePath : MonoBehaviour {

	public GameObject PathManager;

	// Use this for initialization
	void Start () {
		PathManager = GameObject.FindWithTag ("Path");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
