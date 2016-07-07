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
    public float attackableRange = 2.0f;    //공격범위
    public float attackStateMaxTime = 4.0f; //공격대기시간
    public int monsterAttackPoint;  //몬스터 공격력
    public float checkAttackDistance = 2.0f; //견제공격 범위


    Transform player;
    public float speed;
	private float distance;
	Transform target;
	CharacterController enemyController;
	//Animator anim;
    public int mHp = 300;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("CharCenter").transform;
		enemyController = GetComponent<CharacterController> ();
    }
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (state);
		switch (state) {
		    case STATE.IDLE:
                distance = Vector3.Distance(transform.position, player.position);
                stateTime += Time.deltaTime;
                if (distance <= 5.0f)
                {
                    state = STATE.MOVE;
                    Debug.Log("Move로 이동");
                    stateTime = 0.0f;
                }
                if (stateTime > idleStateMaxTime) {
				
				stateTime = 0.0f;

                   
                    if (distance < attackableRange)
                    {
                        //anim.SetBool ("enemyIsAttack", true);
                        state = STATE.ATTACK;
                        Debug.Log("Attack으로 이동");
                    }

                    //if (distance < checkAttackDistance) {
                    //state = STATE.CHECKATTACK;
                    //}
                }

            break;

            case STATE.AFTERIDLE:
			stateTime += Time.deltaTime;
			if (stateTime > idleStateMaxTime) {
				stateTime = 0.0f;
				state = STATE.IDLE;
			}
			break;

            case STATE.MOVE:
                //해당 스크립트 오브젝트와 player간의 거리
			distance = Vector3.Distance (transform.position, player.position);

			if (distance < attackableRange) {
				state = STATE.IDLE;
				//stateTime = 0.0f;
                    Debug.Log("Idle이동");
			}
                else {
				Vector3 dir = player.position - transform.position;
				dir.y = 0.0f;
				dir.Normalize ();
				enemyController.SimpleMove (dir * speed);
                    Debug.Log("추적");
            }
			break;

            case STATE.ATTACK:
                distance = Vector3.Distance(transform.position, player.position);
                stateTime += Time.deltaTime;
                
                if (stateTime > attackStateMaxTime) {
				stateTime = 0.0f;
				GameManager.Instance ().PlayerHealth (monsterAttackPoint);
				Debug.Log("Attack");
                    //anim.SetBool ("enemyIsAttack", false);
                    state = STATE.IDLE;
                }
               
                
			break;

            case STATE.DAMAGE:

			if (mHp <= 0) {
				state = STATE.DEAD;
				GameManager.Instance ().SkillGauge (1);
				Destroy (gameObject);
				break;
			}
			stateTime += Time.deltaTime;
			if (stateTime > idleStateMaxTime) {
				stateTime = 0.0f;
				//anim.SetBool ("Damage", false);
				state = STATE.AFTERIDLE;
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
