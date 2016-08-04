using UnityEngine;
using System.Collections;

public class csFollowWeapon : MonoBehaviour {
	GameObject weapon;
	bool isfollow = false;
	bool once = true;
	// Use this for initialization
	void Start () {
		StartCoroutine (FollowStart ());
		int a = Random.Range (-1, 1);
		int b = Random.Range (-1, 1);
		int c = Random.Range (-1, 1);

		float right = Random.Range (-500.0f, -300.0f) * a;
		float forward = Random.Range (-500.0f, -300.0f) * b;
		float up = Random.Range (-500.0f, -300.0f) * c;


		GetComponent<Rigidbody> ().AddForce ((transform.forward * forward) + (transform.right * right) + (transform.up * up));
		weapon = GameObject.FindWithTag ("CharCenter");
	}
	
	// Update is called once per frame
	void Update () {
		if (isfollow == false) {
			return;
		}
		//transform.LookAt (weapon.transform);
		//transform.Translate (Vector3.forward * 10.0f * Time.deltaTime);
		transform.position = Vector3.MoveTowards(transform.position, weapon.transform.position , 30.0f * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "CharCenter") {
            AudioManager.Instance().PlayFragmentAbsorbSound();
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
		if (GetComponent<Rigidbody> () != null) {
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().isKinematic = true;
		}
		isfollow = true;
	}


}
