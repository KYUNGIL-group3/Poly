using UnityEngine;
using System.Collections;

public class csEnemyRig : MonoBehaviour {
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

    public float idleStateMaxTime = 1.0f;   //대기시간,경직시간
    public float attackableRange = 1.5f;    //공격범위
    public float attackStateMaxTime = 1.0f; //공격대기시간
    public int monsterAttackPoint;  //몬스터 공격력
    public float checkAttack;   //견제공격
    public float checkAttackDistance; //견제공격 범위


    Transform player;
    public float speed;
	private float distance;
	Transform target;
	CharacterController enemyController;
    public int mHp = 100;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("MoveSkillObj").transform;
		enemyController = GetComponent<CharacterController> ();
        monsterAttackPoint = 10;

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(state);
        switch (state)
        {
            case STATE.IDLE:
                stateTime += Time.deltaTime;
                if (stateTime > idleStateMaxTime)
                {
                    distance = Vector3.Distance(transform.position, player.position);
                    stateTime = 0.0f;
                    state = STATE.MOVE;
                    if (distance < attackableRange) 
                    {
                        state = STATE.ATTACK;
                    }
                    if (distance < checkAttackDistance)
                    {
                        state = STATE.CHECKATTACK;
                    }
                }


                break;
            case STATE.AFTERIDLE:
                stateTime += Time.deltaTime;
                if (stateTime > idleStateMaxTime)
                {
                    stateTime = 0.0f;
                    state = STATE.IDLE;
                }
                break;
            case STATE.MOVE:
                //해당 스크립트 오브젝트와 player간의 거리
                distance = Vector3.Distance(transform.position, player.position);

                if (distance < attackableRange)
                {
                    state = STATE.ATTACK;
                    stateTime = attackStateMaxTime;
                }
                    
                else {
                    Vector3 dir = player.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    enemyController.SimpleMove(dir * speed);
                }
                break;
            case STATE.ATTACK:
                stateTime += Time.deltaTime;                                                                            
                if (stateTime > attackStateMaxTime)
                {
                    stateTime = 0.0f;
                    GameManager.Instance().PlayerHealth(monsterAttackPoint);
                }
                state = STATE.IDLE;
                break;
            case STATE.DAMAGE:
                stateTime = 0.0f;
                
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
                    state = STATE.MOVE;
                }
                state = STATE.IDLE;
                break;
            case STATE.DEAD:
                state = STATE.NONE;
                break;
                
        }
       
		

	}
	public void Damage(int WeaponAttackPoint)
	{
        mHp -= WeaponAttackPoint;
        state = STATE.DAMAGE;
	}
}
