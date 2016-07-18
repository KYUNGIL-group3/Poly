using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csText : MonoBehaviour {

	public GameObject Text;
	public GameObject par;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			GameObject temp = Instantiate (Text, transform.position , Quaternion.identity) as GameObject;
			//Debug.Log (temp.GetComponent<LayoutElement> ().preferredWidth);

			temp.transform.parent = par.gameObject.transform;
		}
	}
}
