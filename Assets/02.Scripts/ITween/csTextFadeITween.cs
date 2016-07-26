using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csTextFadeITween : MonoBehaviour {
	
	Color myColor;
	// Use this for initialization
	void Start () {
		FadeIn ();

	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FadeIn()
	{
		Hashtable hash = new Hashtable ();

		hash.Add ("from", 0.0f);
		hash.Add ("to", 255.0f);
		hash.Add ("time", 2.0f);
		hash.Add ("onComplete", "FadeOut");
		hash.Add ("onupdate", "ValueToUpdate");

		iTween.ValueTo (gameObject, hash);

	}

	public void FadeOut()
	{

		Hashtable hash1 = new Hashtable ();

		hash1.Add ("from", 255.0f);
		hash1.Add ("to", 0.0f);
		hash1.Add ("time", 2.0f);
		hash1.Add ("delay", 1.0f);
		hash1.Add ("onupdate", "ValueToUpdate");

		iTween.ValueTo (gameObject, hash1);
	}

	void ValueToUpdate(float updateValue)
	{
		myColor = GetComponent<Text> ().color;
		myColor.a = updateValue / 255.0f;
		GetComponent<Text> ().color = myColor;
	}


}
