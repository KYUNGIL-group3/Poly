using UnityEngine;
using System.Collections;

public class csPointSetPosition : MonoBehaviour {

	float Cdis;
	float Fdis;
	float Bdis;
	float Ldis;
	float Rdis;

	// Use this for initialization
	void Start () {
		//StartCoroutine (SetPosition ());
		//StartCoroutine (SetRotation ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Centerdis(float i)
	{
		Cdis = i;
		transform.position = transform.position + new Vector3 (0, -i, 0);  
	}

	public void Rightdis(float i)
	{
		Rdis = i;
	}

	public void Leftdis(float i)
	{
		Ldis = i;
	}

	public void Forwarddis(float i)
	{
		Fdis = i;
	}

	public void Backdis(float i)
	{
		Bdis = i;
	}

//	IEnumerator SetPosition()
//	{
//		yield return new WaitForSeconds (0.1f);
//
//		if (Mathf.Approximately (Cdis, Rdis) &&
//			Mathf.Approximately (Rdis, Ldis) &&
//			Mathf.Approximately (Ldis, Fdis) &&
//			Mathf.Approximately (Fdis, Bdis)) {
//
//		} else {
//			//transform.position = transform.position + new Vector3 (0, 0.3f, 0);
//		}
//	}
//
//	IEnumerator SetRotation()
//	{
//		yield return new WaitForSeconds (0.1f);
//
//		if (Mathf.Approximately (Cdis, Rdis) &&
//			Mathf.Approximately (Rdis, Ldis) &&
//			Mathf.Approximately (Ldis, Fdis) &&
//			Mathf.Approximately (Fdis, Bdis)) {
//
//		} else {
//			//transform.position = transform.position + new Vector3 (0, 0.3f, 0);
//		}
//	}
}
