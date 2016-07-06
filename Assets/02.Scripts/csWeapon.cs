using UnityEngine;
using System.Collections;

public class csWeapon : MonoBehaviour {

	GameObject player;
	int WeaponTypeNum;
	int AttackPoint;
	float AttackRange;
	int killCount = 0;

	public int[] WeaponStateAttack;
	public float[] WeaponStateRange;


	// Use this for initialization
	void Start () {
		UpgradeWeapon (killCount);
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy") {
			if (player.GetComponent<csPlayerController> ().isAttack) {
				col.gameObject.GetComponent<csEnemyRig> ().Damage(AttackPoint);
			}
		}
	}

	void UpgradeWeapon(int Count)
	{
		if (Count > 100) {
			SetState (3);
		} else if (Count > 60) {

			SetState (2);
		} else if (Count > 30) {

			SetState (1);
		} else {
			SetState (0);
		}
	}

	void SetState(int level)
	{
		AttackPoint = WeaponStateAttack [level];
		AttackRange = WeaponStateRange [level];
		transform.localScale = new Vector3 (AttackRange, AttackRange, AttackRange);
	}
}
