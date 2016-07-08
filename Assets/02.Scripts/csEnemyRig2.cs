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

   
    Transform[] obj;
    public float idleStateMaxTime = 1.0f;   //대기시간,경직시간
    public float attackableRange = 5.0f;    //공격범위
    public float attackStateMaxTime = 4.0f; //공격대기시간
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
                //obj[i].transform.parent = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        akTime += Time.deltaTime;
        //Debug.Log (state);
        switch (state)
        {
            case STATE.IDLE:
              
                distance = Vector3.Distance(transform.position, player.position);
                stateTime += Time.deltaTime;
                if (distance < attackableRange)
                {
                    if (akTime > akmaxTime)
                    {
                        //anim.SetBool ("enemyIsAttack", true);
                        akTime = 0.0f;
                        state = STATE.ATTACK;
                        Debug.Log("Attack으로 이동");
                    }
                    else
                    {
                        state = STATE.MOVE;
                        Debug.Log("Move로 이동");
                        stateTime = 0.0f;

                    }
                }
                else if (distance < checkAttackDistance)
                {
                    state = STATE.ATTACK;
                    Debug.Log("견제");
                }
                
                if (stateTime > idleStateMaxTime)

                {

                    stateTime = 0.0f;


                }

                break;

            case STATE.MOVE:
                //해당 스크립트 오브젝트와 player간의 거리
                distance = Vector3.Distance(transform.position, player.position);

                if (distance < attackableRange)
                {
                    state = STATE.IDLE;
                    stateTime = 0.0f;
                    Debug.Log("Idle이동");
                }
                else {
                    Vector3 dir = player.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    if (distance >= 3.1f)
                    {
                        enemyController.SimpleMove(dir * speed);
                        Debug.Log("추적");
                        transform.LookAt(player);

                        //state = STATE.IDLE;
                        if (akTime > akmaxTime)
                        {
                            state = STATE.IDLE;
                        }

                    }
                    if (distance < 3.0f)
                    {
                        enemyController.SimpleMove(-dir * speed);
                        Debug.Log("회피");
                        transform.LookAt(player);
                        //state = STATE.IDLE;

                        if (akTime > akmaxTime)
                        {
                            state = STATE.IDLE;
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
                    if (distance < 5.1f)
                    {
                        GameManager.Instance().PlayerHealth(monsterAttackPoint);
                        Debug.Log("Attack");
                        obj = gameObject.GetComponentsInChildren<Transform>();
                        for (int i = 1; i < obj.Length; i++)
                        {
                            if (obj[i].tag == "EMissile")
                            {
                                obj[i].transform.parent = null;

                                
                                break;
                            }
                        }
                        //anim.SetBool ("enemyIsAttack", false);
                        state = STATE.IDLE;
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
