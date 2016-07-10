using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
	{
		reloadTime += Time.deltaTime;
        
		switch (state) {
		case STATE.IDLE:
			distance = Vector3.Distance (transform.position, player.position);
			stateTime = 0.0f;
			//stateTime += Time.deltaTime;
		
			if (distance <= checkMoveDistance) {
				state = STATE.MOVE;
				return;
			}

//			if (distance < attackableRange) {
//				state = STATE.ATTACK;
//				return;
//			}

			break;

		case STATE.MOVE:

			obj = gameObject.GetComponentsInChildren<Transform>();
			distance = Vector3.Distance (transform.position, player.position);
			if (obj.Length == 14) {
				if (reloadTime > reloadmaxTime) {
					for (int i = 1; i < obj.Length; i++) {
						if (obj [i].tag == "EnemyDown") {

							GameObject bullettemp = Instantiate (bullet, obj [i].position, Quaternion.identity) as GameObject;
							bullettemp.transform.parent = obj [i];
						}
					}
				}
				return;
			} else if (reloadTime > attackStateMaxTime) {
				reloadTime = 0.0f;
				state = STATE.ATTACK;
				return;
			}

			if (distance > checkMoveDistance) {

				state = STATE.IDLE;
				return;
			}

                //MOVE상태에서 적 사거리 내에 플레이어가 없으면 추적한다.
               
			if (distance > checkAttackDistance + 0.2f) {
				Vector3 dir = player.position - transform.position;
				dir.y = 0.0f;
				dir.Normalize ();
				//Debug.Log ("추적");

				transform.LookAt (player);
				enemyController.SimpleMove (dir * speed);

				//추적하는 중에 사거리에 있으면 ATTACK으로 상태를 바꾼다.
				//사거리에 없다면 MOVE로 상태를 바꾼다.

			}
                //플레이어가 적 사거리내에 있으면 사거리 만큼 회피한다.
                //회피한 후 플레이이가 적 사거리 내에 있으면 ATTACK으로 상태를 바꾼다.
                //사거리에 없으면 MOVE로 상태를 바꾼다.

			if (distance < checkAttackDistance - 0.2f) {
				Vector3 dir = player.position - transform.position;
				dir.y = 0.0f;
				dir.Normalize ();
				//Debug.Log ("회피");
				transform.LookAt (player);
				enemyController.SimpleMove (-dir * speed);
//
//				if (distance < attackableRange) {
//					if (akTime > akmaxTime) {
//						//anim.SetBool ("enemyIsAttack", true);
//						akTime = 0.0f;
//						state = STATE.ATTACK;
//						Debug.Log ("Attack으로 이동2");
//					} else {
//						state = STATE.MOVE;
//						Debug.Log ("Move로 이동2");
//						stateTime = 0.0f;
//
//					}
//				}


			}



			break;

		case STATE.ATTACK:
			
			obj = gameObject.GetComponentsInChildren<Transform> ();
			for (int i = 1; i < obj.Length; i++) {
				if (obj [i].tag == "EMissile") {
					obj [i].transform.parent = null;
					//obj [i].LookAt (player.transform);

					obj [i].gameObject.AddComponent<Rigidbody> ();
					obj [i].gameObject.GetComponent<Rigidbody> ().useGravity = false;
					Vector3 dir = player.position - obj [i].position;
					dir.Normalize ();
					obj [i].gameObject.GetComponent<Rigidbody> ().AddForce (dir * 500.0f);
					//Instantiate (bullet, firePos1.transform.position, firePos1.transform.rotation);

					state = STATE.MOVE;

					return;
				}
			}
			state = STATE.MOVE;
			


			break;

		case STATE.DAMAGE:

			if (mHp <= 0) {
				state = STATE.DEAD;
				GameManager.Instance ().SkillGauge (1);
				//Destroy (gameObject);
				break;
			}
			stateTime += Time.deltaTime;
			if (stateTime > idleStateMaxTime) {
				stateTime = 0.0f;
				//anim.SetBool ("Damage", false);
				state = STATE.IDLE;
			}
                //state = STATE.IDLE;
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
				//obj [i].gameObject.GetComponent<Rigidbody> ().drag = 50;

				//obj [i].gameObject.AddComponent<MeshCollider> ();
				Destroy(obj [i].gameObject , 3.0f);
				obj [i].parent = null;

			}
			Destroy(gameObject , 3.0f);

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
