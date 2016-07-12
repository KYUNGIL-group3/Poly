using UnityEngine;
using System.Collections;

public class csSkillAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy") {
			if(col.gameObject.GetComponent<csEnemy1> ())
				col.gameObject.GetComponent<csEnemy1> ().Damage(300);
			if(col.gameObject.GetComponent<csEnemy2> ())
				col.gameObject.GetComponent<csEnemy2> ().Damage(300);
			if (col.gameObject.GetComponent<csHardMonster>())
				col.gameObject.GetComponent<csHardMonster>().Damage(300);
			if (col.gameObject.GetComponent<csHardMonster2>())
				col.gameObject.GetComponent<csHardMonster2>().Damage(300);
		}
	}


}
