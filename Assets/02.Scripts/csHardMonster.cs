using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class csHardMonster : MonoBehaviour {
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
    public float checkAttackDistance = 2.0f; //견제공격 범위
    public int monsterAttackPoint;  //몬스터 공격력
    public GameObject HitEffect; //몬스터 피격 파티클
    //float knockbackCount = 10.0f;
    //public float DelayTime;
    public GameObject attackfield;
    public GameObject attackfield2;
    public GameObject attackfield3;
    public Transform atkpoint;
    public Transform atkpoint2;
    public Transform atkpoint3;
    float rotspeed = 2.0f;
    float attackMotionSpeed = 5.0f;
    float rottime = 0.0f;
    public float rotmaxtime = 0.7f;

    bool isattackMotion = true;
	bool once = true;
    Transform player;
    public float speed = 3.0f;
    private float distance;
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
    void Start () {
        player = GameObject.FindWithTag("CharCenter").transform;
        enemyController = GetComponent<CharacterController>();
        obj = gameObject.GetComponentsInChildren<Transform>();
        maxmHp = GetComponent<csHardMonster>().mHp;
        anim = GetComponent<Animator>();
        //monsterAttackPoint = GetComponent<csHardMonster>().monsterAttackPoint;
        //Debug.Log(monsterAttackPoint);


    }
	
	// Update is called once per frame
	void Update () {
		healthBarSlider.value =(float)mHp / (float)maxmHp * 100;
		float hpLate = (float)mHp / (float)maxmHp;
		m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor,hpLate);
          
		if (GameManager.Instance ().isTimeControl) {
			
		} else {
			if (once) {
				GameManager.Instance ().CameraMove (this.gameObject);
				//Transform cameraPos = GameObject.Find ("Main Camera").transform;
				//cameraPos.gameObject.GetComponent<SmoothFollow> ().height = 3;
			}
		}

		if (GameManager.Instance ().isGameOver) {
			return;
		}

		if (GameManager.Instance ().restartcount == 1) {
			player = GameObject.FindWithTag("CharCenter").transform;
		}

        switch (state)
        {
            case STATE.IDLE:
                distance = Vector3.Distance(transform.position, player.position);
                stateTime = 0.0f;

                anim.SetInteger("AniStep", 0);

                if (distance < attackableRange)
                {
                    state = STATE.ATTACK;

                }
                else if (distance <= checkMoveDistance)
                {
                    state = STATE.MOVE;
                }

                break;

            case STATE.MOVE:
                distance = Vector3.Distance(transform.position, player.position);


                anim.SetInteger("AniStep", 0);

                if (distance > checkMoveDistance)
                {
                    state = STATE.IDLE;
                    return;
                }
                if (distance < attackableRange + 0.5f)
                {
                    state = STATE.ATTACK;

                }
                else {
                    Vector3 dir = player.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();

                    anim.SetInteger("AniStep", 1);
                    transform.LookAt(player.parent.transform);

                    enemyController.SimpleMove(dir * speed);
                }

                break;

            case STATE.ATTACK:
                distance = Vector3.Distance(transform.position, player.position);
                stateTime += Time.deltaTime;


                anim.SetInteger("AniStep", 0);

                if (distance > checkMoveDistance)
                {
                    state = STATE.IDLE;
                    return;
                }

                if (stateTime > attackStateMaxTime)
                {
                    stateTime = 0.0f;
                    if (distance < attackableRange + 0.5f)
                    {

                        anim.SetInteger("AniStep", 2);
                        transform.LookAt(player.parent.transform);

                        //GameManager.Instance().PlayerHealth(monsterAttackPoint);
                    }
                }

                if (distance > attackableRange + 0.5f)
                {
                    state = STATE.MOVE;
                }


                break;

            case STATE.DAMAGE:


                //if (mHp <= 0)
                //{
                //    state = STATE.DEAD;
                //    GameManager.Instance().SkillGauge(1);
                //    break;
                //}

                //stateTime += Time.deltaTime;
                //knockbackCount-=Time.deltaTime*20.0f;
                //if (knockbackCount > 0)
                //{
                //    gameObject.GetComponent<CharacterController>().Move(new Vector3(0.0f, 0.0f, -(knockbackCount) * Time.deltaTime));

                //}



                ////knockbackCount = 10.0f;

                transform.LookAt(player.parent.transform);
                anim.SetInteger("AniStep", 3);
                state = STATE.IDLE;
                //if (stateTime > idleStateMaxTime)
                //{
                //    Debug.Log("a");
                //    stateTime = 0.0f;
                //    state = STATE.IDLE;
                //}


                break;

		case STATE.DEAD:
			
                state = STATE.NONE;
                AudioManager.Instance().PlayFragmentBrokenSound();
                int SpawnPercent = Random.Range(1, 100);
                if (SpawnPercent >= 1 && SpawnPercent <= 10)
                {
                    AudioManager.Instance().PlayRecoveryItemDropSound();
                    GameManager.Instance().SpawnHealthItem(transform.position);
                }
                

                obj = gameObject.GetComponentsInChildren<Transform>();

                if (gameObject.GetComponent<Animator>() != null)
                {
                    gameObject.GetComponent<Animator>().enabled = false;
                }

                for (int i = 1; i < obj.Length; i++)
                {
                    obj[i].gameObject.AddComponent<Rigidbody>();
                    obj[i].gameObject.AddComponent<BoxCollider>();
                    obj[i].gameObject.GetComponent<BoxCollider>().center = new Vector3(0.0f, 0.0f, 0.0f);
                    obj[i].gameObject.GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 0.2f);
                    obj[i].gameObject.AddComponent<csFollowWeapon>();
                    //Destroy(obj [i].gameObject , 3.0f);
                    obj[i].parent = null;
                    obj[i].gameObject.layer = 0;

                }
                //obj [0].parent = null;
                //obj [0].gameObject.layer = 12;
                
                Destroy(gameObject,0.1f);
                
                break;

        }
    }
    public void Damage(int WeaponAttackPoint)
    {
        if (mHp < maxmHp * 0.5)
        {
            //once = true;
            //if (GameObject.Find("Swordsman") && once == true)
            //{
            //    Debug.Log("스워드맨 공격력 상승");
            //    GameObject.Find("Swordsman").GetComponent<csHardMonster>().monsterAttackPoint += 20;
            //    once = false;
            //}
            //if (GameObject.Find("Spearman") && once == true)
            //{
            //    Debug.Log("스피어맨 공격력 상승");
            //    GameObject.Find("Spearman").GetComponent<csHardMonster>().monsterAttackPoint += 20;
            //    once = false;
            //}
            

            int percentage = Random.Range(1, 100);
            if (percentage >= 1 && percentage <= 30)
            {
                //Debug.Log("방어 자세");
                anim.SetInteger("AniStep", 0);
                return;

            }
            int CounterPercentage = Random.Range(1, 100);
            if (CounterPercentage >= 1 && CounterPercentage <= 30)
            {
                //Debug.Log("카운터");

                CounterAttack();
                
                return;

            }
            
        }

        //stateTime = 0.0f;
        mHp -= WeaponAttackPoint;
        AudioManager.Instance().PlayWeaponHitSound();   //몬스터 피격 사운드
        Instantiate(HitEffect, transform.position, transform.rotation); //몬스터 피격 이펙트
        

		if (mHp <= 0) {
			if (once) {
				once = false;
				GameManager.Instance ().CameraMove (null);
				//Transform cameraPos = GameObject.Find ("Main Camera").transform;
				//cameraPos.gameObject.GetComponent<SmoothFollow> ().height = 7;
			}
			state = STATE.DEAD;
			GameManager.Instance ().SkillGauge (1);
            
		}
    }
    void CounterAttack()
    {
        
        //Debug.Log("CounterAttack");
        state = STATE.DAMAGE;
        //transform.LookAt(player.parent.transform);
        //anim.SetInteger("AniStep", 3);
    }
    void MakeCollider()
    {
        Vector3 setPos = new Vector3(atkpoint.position.x,transform.position.y, atkpoint.position.z);
       

        GameObject AttackFieldObj = Instantiate(attackfield, setPos, Quaternion.identity) as GameObject;
       
        AttackFieldObj.GetComponent<csAttackField>().AttackPower = monsterAttackPoint;
        

    }
    void MakeCollider2()
    {
       
        Vector3 setPos2 = new Vector3(atkpoint2.position.x, transform.position.y, atkpoint2.position.z);

       
        GameObject AttackFieldObj2 = Instantiate(attackfield2, setPos2, Quaternion.identity) as GameObject;
        
        AttackFieldObj2.GetComponent<csAttackField>().AttackPower = monsterAttackPoint;

    }
    //반격용 콜라이더를 만듬
    public void MakeCollider3()
    {

        Vector3 setPos3 = new Vector3(atkpoint3.position.x, transform.position.y, atkpoint3.position.z);


        GameObject AttackFieldObj3 = Instantiate(attackfield3, setPos3, Quaternion.identity) as GameObject;

        AttackFieldObj3.GetComponent<csCounterField>().AttackPower = monsterAttackPoint;
        //AttackFieldObj3이 해당 스크립트를 가진 오브젝트의 자식으로 들어간다.(해당 오브젝트가 AttackFieldObj3의 부모가 된다.)
        AttackFieldObj3.transform.parent = gameObject.transform;
    }
    
}
