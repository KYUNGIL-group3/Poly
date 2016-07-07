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
		if (gameObject.name != "LHand") {
			Eweapon = Instantiate (Weapon [WeaponNum], transform.position, transform.rotation) as GameObject;
			Eweapon.transform.parent = gameObject.transform;
		}
		Playeranim = Player.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonUp ("Change")) {
			if (!Player.GetComponent<csPlayerController> ().isAttack && Player.GetComponent<csPlayerController> ().isidle) {
				WeaponNum++;
				if (Weapon.Length <= WeaponNum) {
					WeaponNum = 0;
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
