using UnityEngine;
using System.Collections;

public class csmove : MonoBehaviour {
	// Use this for initialization
	Transform obj = null;
	public float speed = 500.0f; 
	void Start () 
	{
		obj = GameObject.Find ("Cube").transform;

	}

	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround(obj.position, Vector3.up, speed * Time.deltaTime);
		//transform.LookAt (obj);


	}
}
