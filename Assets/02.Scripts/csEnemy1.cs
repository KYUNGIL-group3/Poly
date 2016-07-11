﻿using UnityEngine;
using System.Collections;

public class csEnemy1 : MonoBehaviour {
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
	Transform[] obj;
	public float idleStateMaxTime = 2.0f;   //대기시간,경직시간
	public float checkMoveDistance = 5.0f; //몬스터 시야
    public float attackableRange = 2.0f;    //공격범위
    public float attackStateMaxTime = 5.0f; //공격대기시간
	public float checkAttackDistance = 0.0f; //견제공격 범위
	public int monsterAttackPoint = 20;  //몬스터 공격력

	float rotspeed = 2.0f;
	float attackMotionSpeed = 5.0f;
	float rottime = 0.0f;
	public float rotmaxtime = 0.7f;

	bool isattackMotion = true;

    Transform player;
    public float speed = 3.0f;
	private float distance;
	Transform target;
	CharacterController enemyController;
	Animator anim;
    public int mHp = 300;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("CharCenter").transform;
		enemyController = GetComponent<CharacterController> ();
		obj = gameObject.GetComponentsInChildren<Transform> ();

		if (gameObject.name != "NormalEnemy1(Clone)") {
			anim = GetComponent<Animator> ();
		}
    }
	
	// Update is called once per frame
	void Update () {

		if (gameObject.name == "NormalEnemy1(Clone)") {
			for (int i = 1; i < obj.Length; i++) {
				if (obj [i].tag == "Body") {
					obj [i].transform.Rotate (new Vector3 (0.0f, rotspeed, 0.0f));
				}
			}
		}

		switch (state) {
		case STATE.IDLE:
			distance = Vector3.Distance (transform.position, player.position);
			stateTime = 0.0f;
			if (gameObject.name != "NormalEnemy1(Clone)") {
				anim.SetInteger ("AniStep", 0);
			}
			if (distance < attackableRange) {
				state = STATE.ATTACK;

			} else if (distance <= checkMoveDistance) {
				state = STATE.MOVE;
			}

			break;

		case STATE.MOVE:
			distance = Vector3.Distance (transform.position, player.position);

			if (gameObject.name != "NormalEnemy1(Clone)") {
				anim.SetInteger ("AniStep", 0);
			}
			if (distance > checkMoveDistance) {
				state = STATE.IDLE;
				return;
			}
			if (distance < attackableRange + 0.5f) {
				state = STATE.ATTACK;

			} else {
				Vector3 dir = player.position - transform.position;
				dir.y = 0.0f;
				dir.Normalize ();
				if (gameObject.name != "NormalEnemy1(Clone)") {
					anim.SetInteger ("AniStep", 1);
					transform.LookAt (player.parent.transform);
				}
				enemyController.SimpleMove (dir * speed);
			}

			break;

		case STATE.ATTACK:
			distance = Vector3.Distance (transform.position, player.position);
			stateTime += Time.deltaTime;

			if (gameObject.name != "NormalEnemy1(Clone)") {
				anim.SetInteger ("AniStep", 0);
			}
			if (distance > checkMoveDistance) {
				state = STATE.IDLE;
				return;
			}

			if (stateTime > attackStateMaxTime) {
				stateTime = 0.0f;
				if (distance < attackableRange + 0.5f) {
					if (gameObject.name != "NormalEnemy1(Clone)") {
						anim.SetInteger ("AniStep", 2);
						transform.LookAt (player.parent.transform);
					}
					GameManager.Instance ().PlayerHealth (monsterAttackPoint);
				}					
			}

			if (distance > attackableRange + 0.5f) {
				state = STATE.MOVE;
			}
               
                
			break;

		case STATE.DAMAGE:

			if (mHp <= 0) {
				state = STATE.DEAD;
				GameManager.Instance ().SkillGauge (1);
				break;
			}
			stateTime += Time.deltaTime;
			if (stateTime > idleStateMaxTime) {
				stateTime = 0.0f;
				state = STATE.IDLE;
			}
			break;

		case STATE.DEAD:
			state = STATE.NONE;

			obj = gameObject.GetComponentsInChildren<Transform> ();

			if (gameObject.GetComponent<Animator> () != null) {
				gameObject.GetComponent<Animator>().enabled = false;	
			}

			for (int i = 1; i < obj.Length; i++) {
				obj [i].gameObject.AddComponent<Rigidbody> ();
				obj [i].gameObject.AddComponent<BoxCollider> ();
				obj [i].gameObject.GetComponent<BoxCollider> ().size = new Vector3 (0.2f, 0.2f, 0.2f);
				Destroy(obj [i].gameObject , 3.0f);
				obj [i].parent = null;

			}
			Destroy(gameObject , 3.0f);
			break;
                
		}
       
		

	}

	void FixedUpdate () {
		rottime += Time.deltaTime;
		if (isattackMotion) {
			for (int i = 1; i < obj.Length; i++) {
				if (obj [i].tag == "EnemyDown") {
					obj [i].transform.Rotate (new Vector3 (attackMotionSpeed * 0.5f, 0.0f, 0.0f));
				}
			}
		}

		if (rottime > rotmaxtime) {
			rottime = 0.0f;
			isattackMotion = false;
		}
	}

	public void Damage(int WeaponAttackPoint)
	{
		stateTime = 0.0f;
        mHp -= WeaponAttackPoint;
        state = STATE.DAMAGE;
	}
}