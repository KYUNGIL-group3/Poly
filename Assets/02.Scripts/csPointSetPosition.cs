using UnityEngine;
using System.Collections;

public class csPointSetPosition : MonoBehaviour {

	public GameObject pointsprite;
	float Cdis;
	float Fdis;
	float Bdis;
	float Ldis;
	float Rdis;
	float del = 0.0f;
	bool once = false;
	// Use this for initialization
	void Start () {
		


		//StartCoroutine (setNextPoint ());

	}
	
	// Update is called once per frame
	void Update () {
		if (once) {
			return;
		}
		if (del > 0.1f) {
			once = true;
			RaycastHit hit;

			if (Physics.Raycast (transform.position, Vector3.forward, out hit, 1.5f, 13 << 13)) {
			
			} else {
				GameObject goTemp = Instantiate (pointsprite, transform.position + Vector3.forward * 1.0f,
					                    Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f))) as GameObject;
				goTemp.transform.parent = gameObject.transform.parent;
			}
		} else {
			del += Time.unscaledDeltaTime;
		}

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


}
