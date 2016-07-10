using UnityEngine;
using System.Collections;

public class csEnemyRig2 : MonoBehaviour
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
    public Transform firePos1;
    public Transform firePos2;
    public Transform firePos3;
    public GameObject bullet;
   
    Transform[] obj;
    public float idleStateMaxTime = 1.0f;   //대기시간,경직시간
    public float attackableRange = 5.0f;    //공격범위
    public float attackStateMaxTime = 3.0f; //공격대기시간
    public int monsterAttackPoint;  //몬스터 공격력
    public float checkAttackDistance = 6.0f; //견제공격 범위(몬스터 시야)
    public float akTime = 0.0f;
    public float akmaxTime = 1.5f;

    Transform player;
    public float speed;
    private float distance;
    Transform target;
    CharacterController enemyController;
    //Animator anim;
    public int mHp = 300;
    // Use this for initialization
    void Start()
    {
		player = GameObject.FindWithTag("CharCenter").transform;
        enemyController = GetComponent<CharacterController>();
        obj = gameObject.GetComponentsInChildren<Transform>();
        for (int i = 1; i < obj.Length; i++)
        {
            if (obj[i].tag == "EMissile")
            {
                obj[i].transform.parent = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        akTime += Time.deltaTime;
        
        switch (state)
        {
            case STATE.IDLE:
                
                distance = Vector3.Distance(transform.position, player.position);
                stateTime += Time.deltaTime;
               

                if (stateTime > idleStateMaxTime)

                {

                    stateTime = 0.0f;
                    //적의 사거리에 플레이어가 있으면 MOVE로 상태를 바꾼다.
                    if (distance < checkAttackDistance)
                    {
                        state = STATE.MOVE;
                        transform.LookAt(player);
                        Debug.Log("탐지");
                    }

                    //if (distance < attackableRange)
                    //{
                    //    if (akTime > akmaxTime)
                    //    {
                    //        //anim.SetBool ("enemyIsAttack", true);
                    //        akTime = 0.0f;
                    //        state = STATE.ATTACK;
                    //        Debug.Log("Attack으로 이동");
                    //    }
                    //    else
                    //    {
                    //        state = STATE.MOVE;
                    //        Debug.Log("Move로 이동");
                    //        stateTime = 0.0f;

                    //    }
                    //}
                   

                }

                break;

            case STATE.MOVE:
                //해당 스크립트 오브젝트와 player간의 거리
                distance = Vector3.Distance(transform.position, player.position);

                //MOVE상태에서 적 사거리 내에 플레이어가 없으면 추적한다.
               
                if (distance > attackableRange)
                {
                    Vector3 dir = player.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    Debug.Log("추적");
                    transform.LookAt(player);
                    enemyController.SimpleMove(dir * speed);
                    //추적하는 중에 사거리에 있으면 ATTACK으로 상태를 바꾼다.
                    //사거리에 없다면 MOVE로 상태를 바꾼다.

                    if (distance < attackableRange)
                    {
                        if (akTime > akmaxTime)
                        {
                            //anim.SetBool ("enemyIsAttack", true);
                            akTime = 0.0f;
                            state = STATE.ATTACK;
                            Debug.Log("Attack으로 이동1");
                        }

                        else
                        {
                            state = STATE.MOVE;
                            Debug.Log("Move로 이동1");
                            stateTime = 0.0f;

                        }
                    }


                }
                //플레이어가 적 사거리내에 있으면 사거리 만큼 회피한다.
                //회피한 후 플레이이가 적 사거리 내에 있으면 ATTACK으로 상태를 바꾼다.
                //사거리에 없으면 MOVE로 상태를 바꾼다.
                if (distance < checkAttackDistance + 0.2)
                {
                    Vector3 dir = player.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    Debug.Log("회피");
                    transform.LookAt(player);
                    enemyController.SimpleMove(-dir * speed);
                    if (distance < attackableRange)
                    {
                        if (akTime > akmaxTime)
                        {
                            //anim.SetBool ("enemyIsAttack", true);
                            akTime = 0.0f;
                            state = STATE.ATTACK;
                            Debug.Log("Attack으로 이동2");
                        }
                        else
                        {
                            state = STATE.MOVE;
                            Debug.Log("Move로 이동2");
                            stateTime = 0.0f;

                        }
                    }


                }



                break;

            case STATE.ATTACK:
                distance = Vector3.Distance(transform.position, player.position);
                stateTime += Time.deltaTime;

                if (stateTime > attackStateMaxTime)
                {
                    stateTime = 0.0f;
                    if (distance < attackableRange)
                    {
                        GameManager.Instance().PlayerHealth(monsterAttackPoint);
                        Debug.Log("Attack");
                        obj = gameObject.GetComponentsInChildren<Transform>();
                        for (int i = 1; i < obj.Length; i++)
                        {
                            if (obj[i].tag == "EMissile")
                            {
                                obj[i].transform.parent = null;
                                Instantiate(bullet, firePos1.transform.position, firePos1.transform.rotation);

                                break;
                            }
                        }
                        state = STATE.MOVE;
                        //anim.SetBool ("enemyIsAttack", false);                   
                    }
                    else if (distance > attackableRange)
                    {
                        state = STATE.MOVE;
                        stateTime = 0.0f;
                    }

                }


                break;

            case STATE.DAMAGE:

                if (mHp <= 0)
                {
                    state = STATE.DEAD;
                    GameManager.Instance().SkillGauge(1);
                    Destroy(gameObject);
                    break;
                }
                stateTime += Time.deltaTime;
                if (stateTime > idleStateMaxTime)
                {
                    stateTime = 0.0f;
                    //anim.SetBool ("Damage", false);
                    state = STATE.IDLE;
                }
                //state = STATE.IDLE;
                break;

            case STATE.DEAD:
                state = STATE.NONE;
                break;

        }



    }
    public void Damage(int WeaponAttackPoint)
    {

        //anim.SetBool ("Damage", true);
        stateTime = 0.0f;
        mHp -= WeaponAttackPoint;
        state = STATE.DAMAGE;
    }
}
