using UnityEngine;
using System.Collections;

public class csKCube : MonoBehaviour {
	public int Hp = 100;
	bool isDamage = true;
	GameObject playerC;
	bool PlayerisAttack;

	// Use this for initialization
	void Start () {
		playerC = GameObject.FindWithTag ("Player");
		PlayerisAttack = playerC.GetComponent<csPlayerController> ().isAttack;

	}
	
	// Update is called once per frame
	void Update () {
		if (Hp <= 0)
			Destroy (gameObject);
	}

	void OnCollisionEnter(Collision col)
	{
		PlayerisAttack = playerC.GetComponent<csPlayerController> ().isAttack;
		if (col.gameObject.tag == "WEAPON" && isDamage && PlayerisAttack) {
			Hp = Hp- 10;
			StartCoroutine (DamageFalse ());
		}
	}

	IEnumerator DamageFalse()
	{
		isDamage = false;
		yield return new WaitForSeconds (1.0f);
		isDamage = true;
	}
}
