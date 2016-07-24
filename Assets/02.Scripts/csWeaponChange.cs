using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class csWeaponChange : MonoBehaviour {

	public Button[] Weapons;
	public Button mainSlot;
	public Button subSlot;

	bool selectMainWeapon = true;

	// Use this for initialization
	void Start () {

		Weapons [SceneManager.Instance ().Weapon1Get ()].targetGraphic.color = Color.blue;

		if (SceneManager.Instance ().Weapon1Get () != SceneManager.Instance ().Weapon2Get ()) {
			Weapons [SceneManager.Instance ().Weapon2Get ()].targetGraphic.color = Color.red;
		}



		mainSlot.targetGraphic.color = Color.blue;
		subSlot.targetGraphic.color = Color.white;

//		if (SceneManager.Instance ().Weapon1Get () != SceneManager.Instance ().Weapon2Get ()) {
//
//			switch (SceneManager.Instance ().Weapon2Get ()) {
//			case 0:
//				Weapon1.targetGraphic.color = Color.red;
//				break;
//			case 1:
//				Weapon2.targetGraphic.color = Color.red;
//				break;
//			case 2:
//				Weapon3.targetGraphic.color = Color.red;
//				break;
//			case 3:
//				Weapon4.targetGraphic.color = Color.red;
//				break;
//			case 4:
//				Weapon5.targetGraphic.color = Color.red;
//				break;
//			}
//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectMain()
	{
		selectMainWeapon = true;
		mainSlot.targetGraphic.color = Color.blue;
		subSlot.targetGraphic.color = Color.white;
	}

	public void selectSub()
	{
		selectMainWeapon = false;
		mainSlot.targetGraphic.color = Color.white;
		subSlot.targetGraphic.color = Color.red;
	}

	public void selectWeapon(int WeaponNum)
	{
		if (selectMainWeapon) {
			if (SceneManager.Instance ().Weapon2Get () == WeaponNum) {

				int weapon1 = SceneManager.Instance ().Weapon1Get ();
				int weapon2 = SceneManager.Instance ().Weapon2Get ();

				Weapons [weapon2].targetGraphic.color = Color.blue;
				Weapons [weapon1].targetGraphic.color = Color.red;

				SceneManager.Instance ().Weapon1Set (weapon2);
				SceneManager.Instance ().Weapon2Set (weapon1);
				return;
			} else {
				for (int i = 0; i < Weapons.Length; i++) {
					if (i != SceneManager.Instance ().Weapon2Get ()) {
						Weapons [i].targetGraphic.color = Color.white;	
					}
				}
				Weapons [WeaponNum].targetGraphic.color = Color.blue;
				SceneManager.Instance ().Weapon1Set (WeaponNum);
			}
		} else {
			if (SceneManager.Instance ().Weapon1Get () == WeaponNum) {
				int weapon1 = SceneManager.Instance ().Weapon1Get ();
				int weapon2 = SceneManager.Instance ().Weapon2Get ();

				Weapons [weapon2].targetGraphic.color = Color.blue;
				Weapons [weapon1].targetGraphic.color = Color.red;

				SceneManager.Instance ().Weapon1Set (weapon2);
				SceneManager.Instance ().Weapon2Set (weapon1);
				return;
			} else {
				for (int i = 0; i < Weapons.Length; i++) {
					if (i != SceneManager.Instance ().Weapon1Get ()) {
						Weapons [i].targetGraphic.color = Color.white;	
					}
				}
				Weapons [WeaponNum].targetGraphic.color = Color.red;
				SceneManager.Instance ().Weapon2Set (WeaponNum);
			}
		}
	}
}
