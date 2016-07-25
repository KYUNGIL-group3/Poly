using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public GameObject bullet;
    public Transform firePos;
   

    Transform[] obj;
   
    
    public float idleStateMaxTime = 2.0f;   //대기시간,경직시간
    public float checkMoveDistance = 5.0f; //몬스터 시야
    public float attackableRange = 5.0f;    //공격범위
    public float attackStateMaxTime = 1.5f; //공격대기시간
    public float checkAttackDistance = 3.0f; //견제공격 범위
    public int monsterAttackPoint = 30;  //몬스터 공격력
    public GameObject attackfield;
    public Transform atkpoint;
    public float reloadTime = 4.0f;
    public float reloadmaxTime = 5.0f;
    
    bool reloaded = false;
    bool once = false;
    Transform player;
    public float speed = 3.0f;
    private float distance;
    float rotspeed = 2.0f;
    Transform target;
    CharacterController enemyController;
    Animator anim;
    public int mHp;
    private int maxmHp;


	public Slider healthBarSlider;
	public Image m_FillImage;                     
	public Color m_FullHealthColor = Color.green;
	public Color m_ZeroHealthColor = Color.red;  


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
		healthBarSlider.value = (float)mHp / (float)maxmHp * 100;
		float hpLate = (float)mHp / (float)maxmHp;
		m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, hpLate);

		if (GameManager.Instance ().isGameOver) {
			return;
		}
		reloadTime += Time.deltaTime;
        
		switch (state) {
		case STATE.IDLE:
			distance = Vector3.Distance (transform.position, player.position);
			stateTime = 0.0f;
			anim.SetInteger ("AniStep", 0);

			if (distance <= checkMoveDistance) {
				state = STATE.MOVE;
				return;
			}

			break;

		case STATE.MOVE:

			anim.SetInteger ("AniStep", 0);

			obj = gameObject.GetComponentsInChildren<Transform> ();
			distance = Vector3.Distance (transform.position, player.position);



			if (reloadTime > attackStateMaxTime) {
				if (distance > attackableRange) {
					//거리가 공격사거리를 벗어나도 Move상태로 둔다.
				} else {
					reloadTime = 0.0f;
					stateTime = 0.0f;
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

				anim.SetInteger ("AniStep", 1);
				transform.LookAt (player.parent.transform);

				enemyController.SimpleMove (dir * speed);
			} else if (distance < checkAttackDistance - 0.2f) {

				Vector3 dir = player.position - transform.position;
				dir.y = 0.0f;
				dir.Normalize ();
				anim.SetInteger ("AniStep", 1);
				transform.LookAt (player.parent.transform);
				enemyController.SimpleMove (-dir * speed / 1.5f);

			} else {
				transform.LookAt (player.parent.transform);
			}



			break;

		case STATE.ATTACK:


			anim.SetInteger ("AniStep", 2);
			transform.LookAt (player.parent.transform);

			if (stateTime > idleStateMaxTime) {
				state = STATE.MOVE;
				stateTime = 0.0f;
			}
			stateTime += Time.deltaTime;
			if (stateTime < 0.1f) {
				anim.SetInteger ("AniStep", 2);
			}
			if (stateTime > 0.1f && stateTime < 0.5f) {
				anim.SetInteger ("AniStep", 0);
			}
			if (stateTime > 1.0f) {
				reloadTime = 0.0f;
				stateTime = 0.0f;
				state = STATE.MOVE;
			}
			break;

		case STATE.DAMAGE:
                //stateTime += Time.deltaTime;
                //if (mHp <= 0)
                //{
                //    state = STATE.DEAD;
                //    GameManager.Instance().SkillGauge(1);
                //    break;
                //}


			transform.LookAt (player.parent.transform);
			anim.SetInteger ("AniStep", 3);
			state = STATE.IDLE;

			break;

		case STATE.DEAD:
			state = STATE.NONE;
			int SpawnPercent = Random.Range (1, 100);
			if (SpawnPercent >= 1 && SpawnPercent <= 10) {
				GameManager.Instance ().SpawnHealthItem (transform.position);
			}
			obj = gameObject.GetComponentsInChildren<Transform> ();

			if (gameObject.GetComponent<Animator> () != null) {
				gameObject.GetComponent<Animator> ().enabled = false;
			}

			for (int i = 1; i < obj.Length; i++) {
				if (obj [i].gameObject.GetComponent<CharacterJoint> () != null) {
					Destroy (obj [i].gameObject);
					continue;
				}
				if (obj [i].gameObject.tag == "EnemyDown") {
					Destroy (obj [i].gameObject);
					continue;
				}
				//if (obj [i].gameObject.GetComponent<Rigidbody> () != null) {
					obj [i].gameObject.AddComponent<Rigidbody> ();
				//}
				obj [i].gameObject.AddComponent<BoxCollider> ();
				obj [i].gameObject.GetComponent<BoxCollider> ().center = new Vector3 (0.0f, 0.0f, 0.0f);
				obj [i].gameObject.GetComponent<BoxCollider> ().size = new Vector3 (0.2f, 0.2f, 0.2f);

//				obj [i].gameObject.AddComponent<SphereCollider> ();
//				obj [i].gameObject.GetComponent<SphereCollider> ().center = new Vector3 (0.0f, 0.0f, 0.0f);
//				obj [i].gameObject.GetComponent<SphereCollider> ().radius = 0.1f;
				obj [i].gameObject.AddComponent<csFollowWeapon> ();
				//Destroy(obj [i].gameObject , 3.0f);
				obj [i].parent = null;
				obj [i].gameObject.layer = 0;

			}
                //obj [0].parent = null;
                //obj [0].gameObject.layer = 12;
                
			Destroy (gameObject, 0.1f);
                
			break;

		}
	}
    public void Damage(int WeaponAttackPoint)
    {
        if (mHp < maxmHp * 0.5)
        {
            if (GameObject.Find("Crossbow Warrior") && once == true)
            {
                Debug.Log("크로스보우 워리어 공격속도 상승");
                GameObject.Find("Crossbow Warrior").GetComponent<csHardMonster>().attackStateMaxTime -= 0.5f;
                once = false;
            }

            int percentage = Random.Range(1, 100);
            if (percentage >= 1 && percentage <= 30)
            {
                Debug.Log("방어 자세");
                anim.SetInteger("AniStep", 0);
                return;

            }
            int CounterPercentage = Random.Range(1, 100);
            if (CounterPercentage >= 1 && CounterPercentage <= 30)
            {
                Debug.Log("카운터");
               
                CounterAttack();
                return;
                
            }
        }
        //anim.SetBool ("Damage", true);
        //stateTime = 0.0f;
        AudioManager.Instance().PlayWeaponHitSound();   //몬스터 피격 사운드
        mHp -= WeaponAttackPoint;
        if (mHp <= 0)
        {
            state = STATE.DEAD;
            GameManager.Instance().SkillGauge(1);
           
        }
    }

    void Fire()
    {
        AudioManager.Instance().PlayArrowShotSound();   //몬스터 화살 사운드
        GameObject bullettemp = Instantiate(bullet, firePos.position, Quaternion.identity) as GameObject;
        
        bullettemp.GetComponent<csBullet>().bulletdamage = monsterAttackPoint;


        Vector3 dir = player.position - bullettemp.transform.position;
        dir.Normalize();
        bullettemp.transform.LookAt(player);
        bullettemp.gameObject.GetComponent<Rigidbody>().AddForce(dir * 900.0f);

        reloadTime = 0.0f;
        stateTime = 0.0f;
        //anim.SetInteger("AniStep", 0);
    }

    void Move()
    {
        state = STATE.MOVE;
    }
    void CounterAttack()
    {
        state = STATE.DAMAGE;
        //transform.LookAt(player.parent.transform);
        //anim.SetInteger("AniStep", 3);
    }
    void MakeCollider3()
	{
		Vector3 setPos = new Vector3 (atkpoint.position.x, transform.position.y, atkpoint.position.z);


		GameObject AttackFieldObj = Instantiate (attackfield, setPos, Quaternion.identity) as GameObject;

		AttackFieldObj.GetComponent<csCounterField> ().AttackPower = monsterAttackPoint;


	}
}
