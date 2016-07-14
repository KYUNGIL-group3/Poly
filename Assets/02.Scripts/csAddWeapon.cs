using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csAddWeapon : MonoBehaviour {

	public GameObject[] Weapon;
	public GameObject Player;
	GameObject Eweapon;
	Animator Playeranim;
	int WeaponNum=0;

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
		if (CrossPlatformInputManager.GetButtonUp ("Change")) {
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
			}
		} 
	}

}
