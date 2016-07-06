using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csAddWeapon : MonoBehaviour {

	public GameObject[] Weapon;
	GameObject Eweapon;
	int WeaponNum=0;

	// Use this for initialization
	void Start () {
		Eweapon = Instantiate (Weapon [WeaponNum], transform.position, transform.rotation) as GameObject;
		Eweapon.transform.parent = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonUp ("Change")) {
			WeaponNum++;
			if (Weapon.Length <= WeaponNum) {
				WeaponNum = 0;
			}
			Destroy (Eweapon);
			Eweapon = Instantiate (Weapon [WeaponNum], transform.position, transform.rotation) as GameObject;
			Eweapon.transform.parent = gameObject.transform;
		} 
	}

}
