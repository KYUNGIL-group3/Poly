﻿using UnityEngine;
using System.Collections;

public class csEnemy2 : MonoBehaviour
{
    enum STATE
    {
        NONE = -1,
        IDLE = 0,
        MOVE,
        CHECKATTACK,
        AFTERIDLE,
        ATTACK,
        DAMAGE,
        DEAD
    }

    STATE state = STATE.IDLE;
    float stateTime = 0.0f;   
    public GameObject bullet;
	public GameObject bulletDestroy;
	Transform[] obj;
	public float idleStateMaxTime = 2.0f;   //대기시간,경직시간
	public float checkMoveDistance = 5.0f; //몬스터 시야
	public float attackableRange = 5.0f;    //공격범위
	public float attackStateMaxTime = 1.5f; //공격대기시간
	public float checkAttackDistance = 3.0f; //견제공격 범위
	public int monsterAttackPoint = 30;  //몬스터 공격력
    public GameObject HitEffect; //몬스터 피격 파티클
    public float reloadTime = 4.0f;
	public float reloadmaxTime = 5.0f;
    bool reloaded = false;

    Transform player;
    public float speed = 2.0f;
    private float distance;
    Transform target;
    CharacterController enemyController;
    //Animator anim;
    public int mHp = 600;
    // Use this for initialization
    void Start()
    {
		player = GameObject.FindWithTag("CharCenter").transform;
        enemyController = GetComponent<CharacterController>();
        obj = gameObject.GetComponentsInChildren<Transform>();
        
        StartCoroutine (istrigger ());
    }

    // Update is called once per frame
    void Update()
	{
		if (GameManager.Instance ().isGameOver) {
			return;
		}

		if (GameManager.Instance ().restartcount == 1) {
			player = GameObject.FindWithTag("CharCenter").transform;
		}

		reloadTime += Time.deltaTime;
        
		switch (state) {
		case STATE.IDLE:
			distance = Vector3.Distance (transform.position, player.position);
			stateTime = 0.0f;

			if (distance <= checkMoveDistance) {
				state = STATE.MOVE;
				return;
			}

			break;

		case STATE.MOVE:

			obj = gameObject.GetComponentsInChildren<Transform> ();
			distance = Vector3.Distance (transform.position, player.position);
			if (obj.Length == 14) {
                    if (reloadTime > reloadmaxTime)
                    {
                        for (int i = 1; i < obj.Length; i++)
                        {
                            if (obj[i].tag == "EnemyDown")
                            {
                                
                                GameObject bullettemp = Instantiate(bullet, obj[i].position, transform.rotation) as GameObject;
                                
                                bullettemp.transform.parent = obj[i];
                            }
                        }
                    }
				return;
			} else if (reloadTime > attackStateMaxTime) {
				if (attackableRange > distance) {
					reloadTime = 0.0f;
					state = STATE.ATTACK;
					return;
				}
			}

			if (distance > checkMoveDistance) {

				state = STATE.IDLE;
				return;
			}

			if (distance > checkAttackDistance + 0.2f) {
				Vector3 dir = player.position - transform.position;
				dir.y = 0.0f;
				dir.Normalize ();
				transform.LookAt (player);
				enemyController.SimpleMove (dir * speed);
			} else if (distance < checkAttackDistance - 0.2f) {
				Vector3 dir = player.position - transform.position;
				dir.y = 0.0f;
				dir.Normalize ();
				transform.LookAt (player);
				enemyController.SimpleMove (-dir * speed);
			} else {
				transform.LookAt (player);
			}



			break;

		case STATE.ATTACK:
			
			obj = gameObject.GetComponentsInChildren<Transform> ();
                for (int i = 1; i < obj.Length; i++)
                {
                    if (obj[i].tag == "EMissile")
                    {
                        obj[i].transform.parent = null;

                        //obj [i].gameObject.AddComponent<Rigidbody> ();
                        //obj [i].gameObject.GetComponent<Rigidbody> ().useGravity = false;
                        Vector3 dir = player.position - obj[i].position;
                        dir.Normalize();
                        obj[i].transform.LookAt(player);
                        obj[i].gameObject.GetComponent<Rigidbody>().AddForce(dir * 500.0f);

                        state = STATE.MOVE;
                        reloaded = false;

                        return;
                    }
                }
			state = STATE.MOVE;
			


			break;

		case STATE.DAMAGE:

			//if (mHp <= 0) {
			//	state = STATE.DEAD;
			//	GameManager.Instance ().SkillGauge (1);
			//	break;
			//}
			//stateTime += Time.deltaTime;
			//if (stateTime > idleStateMaxTime) {
			//	stateTime = 0.0f;
			//	state = STATE.IDLE;
			//}
			break;

		case STATE.DEAD:
			state = STATE.NONE;
                AudioManager.Instance().PlayFragmentBrokenSound();
                bulletDestroy.GetComponent<csNullparentDestroy> ().DestroyAll();
		
			obj = gameObject.GetComponentsInChildren<Transform> ();

			if (gameObject.GetComponent<Animator> () != null) {
				gameObject.GetComponent<Animator>().enabled = false;	
			}

			for (int i = 1; i < obj.Length; i++) {
				if (obj [i].gameObject.tag == "EMissile") {
					continue;
				}

				if (obj [i].gameObject.tag == "EnemyDown") {
					continue;
				}

				if (obj [i].gameObject.GetComponent<Rigidbody> () == null) {
					obj [i].gameObject.AddComponent<Rigidbody> ();
				}
				if (obj [i].gameObject.GetComponent<BoxCollider> () == null) {
					obj [i].gameObject.AddComponent<BoxCollider> ();
				}
				obj [i].gameObject.GetComponent<BoxCollider> ().center = new Vector3 (0.0f, 0.0f, 0.0f);
				obj [i].gameObject.GetComponent<BoxCollider> ().size = new Vector3 (0.2f, 0.2f, 0.2f);
				obj [i].gameObject.AddComponent<csFollowWeapon> ();

				obj [i].parent = null;
				obj [i].gameObject.layer = 12;

			}
			//obj [0].parent = null;
			//obj [0].gameObject.layer = 12;
			gameObject.layer = 12;
			Destroy(gameObject,0.1f);
			break;

		}
	}
    public void Damage(int WeaponAttackPoint)
	{
        mHp -= WeaponAttackPoint;
        AudioManager.Instance().PlayWeaponHitSound();   //몬스터 피격 사운드
        Instantiate(HitEffect, transform.position, transform.rotation); //몬스터 피격 이펙트
        //anim.SetBool ("Damage", true);
        //stateTime = 0.0f;
        
        if (mHp <= 0)
        {
            state = STATE.DEAD;
            GameManager.Instance().SkillGauge(1);
           
        }
    }

    IEnumerator istrigger()
	{
		yield return new WaitForSeconds (0.5f);
		gameObject.GetComponent<CapsuleCollider> ().isTrigger = false;
	}
}
