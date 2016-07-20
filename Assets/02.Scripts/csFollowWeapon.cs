using UnityEngine;
using System.Collections;

public class csFollowWeapon : MonoBehaviour {
	GameObject weapon;
	bool isfollow = false;
	// Use this for initialization
	void Start () {
		StartCoroutine (FollowStart ());
	}
	
	// Update is called once per frame
	void Update () {
		if (isfollow == false) {
			return;
		}


		weapon = GameObject.FindWithTag ("WEAPON");
		//transform.LookAt (weapon.transform);
		//transform.Translate (Vector3.forward * 10.0f * Time.deltaTime);
		transform.position = Vector3.MoveTowards(transform.position, weapon.transform.position , 30.0f * Time.deltaTime);


	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "WEAPON") {
			Destroy (gameObject);
		}
	}

	IEnumerator FollowStart()
	{
		yield return new WaitForSeconds (2.0f);
		GetComponent<BoxCollider> ().isTrigger = true;
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		isfollow = true;
	}


}
