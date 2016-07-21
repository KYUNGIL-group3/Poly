using UnityEngine;
using System.Collections;

public class csFollowWeapon : MonoBehaviour {
	GameObject weapon;
	bool isfollow = false;
	bool once = true;
	// Use this for initialization
	void Start () {
		StartCoroutine (FollowStart ());
	}
	
	// Update is called once per frame
	void Update () {
		if (isfollow == false) {
			return;
		}
		weapon = GameObject.FindWithTag ("CharCenter");
		//transform.LookAt (weapon.transform);
		//transform.Translate (Vector3.forward * 10.0f * Time.deltaTime);
		transform.position = Vector3.MoveTowards(transform.position, weapon.transform.position , 30.0f * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "CharCenter") {
			if (once) {
				once = false;
				GameManager.Instance ().RecoveryHealth(1);
			}
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
