using UnityEngine;
using System.Collections;

public class csWeapon : MonoBehaviour {

	GameObject player;
	int WeaponTypeNum;
	int AttackPoint;
	float AttackRange;
	int weaponGauge = 0;
	ArrayList EnemyArrayList;

	public int[] WeaponStateAttack;
	public float[] WeaponStateRange;



	// Use this for initialization
	void Start () {
		UpgradeWeapon (weaponGauge);
		player = GameObject.Find ("Player");
		EnemyArrayList = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
		weaponGauge = GameManager.Instance ().gauge;
		if (!GameManager.Instance ().isTimeControl) {
			UpgradeWeapon (weaponGauge);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy") {
            
			if (player.GetComponent<csPlayerController> ().isAttack) {
				if (EnemyArrayList.Contains (col.transform.gameObject)) {
					
				} else {
					EnemyArrayList.Add (col.transform.gameObject);
				}

				if(col.gameObject.GetComponent<csEnemy1> ())
					col.gameObject.GetComponent<csEnemy1> ().Damage(AttackPoint);
				if(col.gameObject.GetComponent<csEnemy2> ())
					col.gameObject.GetComponent<csEnemy2> ().Damage(AttackPoint);
				if (col.gameObject.GetComponent<csHardMonster>())
					col.gameObject.GetComponent<csHardMonster>().Damage(AttackPoint);
				if (col.gameObject.GetComponent<csHardMonster2>())
					col.gameObject.GetComponent<csHardMonster2>().Damage(AttackPoint);
                if (col.gameObject.GetComponent<csBossMonster>())
                    col.gameObject.GetComponent<csBossMonster>().Damage(AttackPoint);
            }
		}
	}

	void UpgradeWeapon(int Count)
	{
		if (Count > 100) {
			SetState (3);
		} else if (Count >= 60) {

			SetState (2);
		} else if (Count >= 30) {

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

	public void targetreset()
	{
		EnemyArrayList.Clear ();
		EnemyArrayList = new ArrayList ();
	}
}
