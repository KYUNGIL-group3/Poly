using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csAddWeapon : MonoBehaviour {

	public GameObject[] Weapon;
	public GameObject Player;
	GameObject Eweapon;
	Animator Playeranim;
	public int WeaponNum=0;

	float changetime= 0.0f;
	float changemaxtime= 1.0f;
	bool changeOk = true;

	public bool changefree = false;

	// Use this for initialization
	void Start () {
		WeaponNum = GameManager.Instance ().Weapon1Num();
		if (gameObject.name != "LHand" || WeaponNum == 1) {
			Eweapon = Instantiate (Weapon [WeaponNum], transform.position, transform.rotation) as GameObject;
			Eweapon.transform.parent = gameObject.transform;
		}

		Playeranim = Player.GetComponent<Animator> ();
		if (gameObject.name == "RHand") {

			Playeranim.SetInteger ("WeaponType", WeaponNum);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!changeOk) {
			changetime += Time.deltaTime;
			if (changetime > changemaxtime) {
				changetime = 0.0f;
				changeOk = true;
			}
			return;
		}
		//if (CrossPlatformInputManager.GetButtonUp ("Change")) {
		if (Input.GetKeyDown("k")) {
            AudioManager.Instance().PlayWeaponTransSound();
			if (!Player.GetComponent<csPlayerController> ().isAttack) {
				if (WeaponNum == GameManager.Instance ().Weapon1Num ()) {
					WeaponNum = GameManager.Instance ().Weapon2Num ();
				}
				else{
					WeaponNum = GameManager.Instance ().Weapon1Num ();
				}

				if (gameObject.name == "RHand") {

					Playeranim.SetInteger ("WeaponType", WeaponNum);
				}
				Destroy (Eweapon);
				if (gameObject.name != "LHand" || WeaponNum == 1) {
					Eweapon = Instantiate (Weapon [WeaponNum], transform.position, transform.rotation) as GameObject;
					Eweapon.transform.parent = gameObject.transform;
				}
				changeOk = false;
			}
		} 
	}

}
