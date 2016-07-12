﻿using UnityEngine;
using System.Collections;

public class csHardMonster2 : MonoBehaviour {

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
    public Transform firePos1;
    public Transform firePos2;
    public Transform firePos3;
    public GameObject bullet;

    Transform[] obj;
    public float idleStateMaxTime = 2.0f;   //대기시간,경직시간
    public float checkMoveDistance = 5.0f; //몬스터 시야
    public float attackableRange = 5.0f;    //공격범위
    public float attackStateMaxTime = 1.5f; //공격대기시간
    public float checkAttackDistance = 3.0f; //견제공격 범위
    public int monsterAttackPoint = 30;  //몬스터 공격력

    public float reloadTime = 4.0f;
    public float reloadmaxTime = 5.0f;
    bool reloaded = false;

    Transform player;
    public float speed = 2.0f;
    private float distance;
    Transform target;
    CharacterController enemyController;
    Animator anim;
    public int mHp;
    private int maxmHp;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("CharCenter").transform;
        enemyController = GetComponent<CharacterController>();
        obj = gameObject.GetComponentsInChildren<Transform>();
        maxmHp = GetComponent<csHardMonster2>().mHp;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        reloadTime += Time.deltaTime;

        switch (state)
        {
            case STATE.IDLE:
                distance = Vector3.Distance(transform.position, player.position);
                stateTime = 0.0f;
               
                    anim.SetInteger("AniStep", 0);
                

                if (distance <= checkMoveDistance)
                {
                    state = STATE.MOVE;
                    return;
                }

                break;

            case STATE.MOVE:

                obj = gameObject.GetComponentsInChildren<Transform>();
                distance = Vector3.Distance(transform.position, player.position);
                if (gameObject.name != "NormalEnemy2(Clone)" && !reloaded)
                {
                    anim.SetInteger("AniStep", 0);
                    if (reloadTime > reloadmaxTime)
                    {
                        for (int i = 1; i < obj.Length; i++)
                        {
                            if (obj[i].tag == "EnemyDown")
                            {
                                GameObject bullettemp = Instantiate(bullet, obj[i].position, Quaternion.identity) as GameObject;
                                bullettemp.transform.parent = obj[i];
                                reloaded = true;
                            }
                        }

                        return;
                    }

                }
                else if (obj.Length == 14)
                {
                    if (reloadTime > reloadmaxTime)
                    {
                        for (int i = 1; i < obj.Length; i++)
                        {
                            if (obj[i].tag == "EnemyDown")
                            {

                                GameObject bullettemp = Instantiate(bullet, obj[i].position, Quaternion.identity) as GameObject;
                                bullettemp.transform.parent = obj[i];
                            }
                        }
                    }
                    return;
                }
                else if (reloadTime > attackStateMaxTime)
                {
                    reloadTime = 0.0f;
                    state = STATE.ATTACK;
                    return;
                }

                if (distance > checkMoveDistance)
                {

                    state = STATE.IDLE;
                    return;
                }

                if (distance > checkAttackDistance + 0.2f)
                {
                    Vector3 dir = player.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    anim.SetInteger("AniStep", 1);
                    transform.LookAt(player);
                    enemyController.SimpleMove(dir * speed);
                }
                else if (distance < checkAttackDistance - 0.2f)
                {
                    Vector3 dir = player.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    anim.SetInteger("AniStep", 1);
                    transform.LookAt(player);
                    enemyController.SimpleMove(-dir * speed);
                }
                else {
                    transform.LookAt(player);
                }



                break;

            case STATE.ATTACK:

                obj = gameObject.GetComponentsInChildren<Transform>();
                for (int i = 1; i < obj.Length; i++)
                {
                    if (obj[i].tag == "EMissile")
                    {
                        obj[i].transform.parent = null;

                        obj[i].gameObject.AddComponent<Rigidbody>();
                        obj[i].gameObject.GetComponent<Rigidbody>().useGravity = false;
                        Vector3 dir = player.position - obj[i].position;
                        dir.Normalize();
                        anim.SetInteger("AniStep", 2);
                        obj[i].gameObject.GetComponent<Rigidbody>().AddForce(dir * 500.0f);

                        state = STATE.MOVE;
                        reloaded = false;

                        return;
                    }
                }
                state = STATE.MOVE;



                break;

            case STATE.DAMAGE:

                if (mHp <= 0)
                {
                    state = STATE.DEAD;
                    GameManager.Instance().SkillGauge(1);
                    break;
                }
                stateTime += Time.deltaTime;
                if (stateTime > idleStateMaxTime)
                {
                    stateTime = 0.0f;
                    state = STATE.IDLE;
                }
                break;

            case STATE.DEAD:
                state = STATE.NONE;

                obj = gameObject.GetComponentsInChildren<Transform>();

                if (gameObject.GetComponent<Animator>() != null)
                {
                    gameObject.GetComponent<Animator>().enabled = false;
                }

                for (int i = 1; i < obj.Length; i++)
                {
                    obj[i].gameObject.AddComponent<Rigidbody>();
                    obj[i].gameObject.AddComponent<BoxCollider>();
                    obj[i].gameObject.GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 0.2f);
                    Destroy(obj[i].gameObject, 3.0f);
                    obj[i].parent = null;

                }
                Destroy(gameObject, 3.0f);

                break;

        }
    }
    public void Damage(int WeaponAttackPoint)
    {
        if (mHp < maxmHp * 0.5)
        {

            int percentage = Random.Range(1, 100);
            if (percentage >= 1 && percentage <= 30)
            {
                Debug.Log("방어 자세");
                anim.SetInteger("AniStep", 0);
                return;

            }
        }
        //anim.SetBool ("Damage", true);
        stateTime = 0.0f;
        mHp -= WeaponAttackPoint;
        state = STATE.DAMAGE;
    }

}