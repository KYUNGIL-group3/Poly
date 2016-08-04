using UnityEngine;
using System.Collections;

public class csBossMonster : MonoBehaviour
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
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;    
    public Transform firePos;

    public float idleStateMaxTime = 1.0f;   //대기시간,경직시간
    public float checkMoveDistance = 20.0f; //몬스터 시야
    public float attackableRange = 20.0f;    //공격범위
    public float attackStateMaxTime = 2.0f; //공격대기시간
    public float checkAttackDistance = 20.0f; //견제공격 범위
    public int monsterAttackPoint;  //몬스터 공격력

    public GameObject HitEffect; //몬스터 피격 파티클    
    public GameObject attackfield;
    public GameObject attackfield2;
    public GameObject attackfield3;
    public GameObject attackRange;
    public GameObject bullet;

    public Transform atkpoint;
    public Transform atkpoint2;
    public Transform atkpoint3;
    
    float rotspeed = 2.0f;
    float attackMotionSpeed = 5.0f;
    float rottime = 0.0f;
    public float rotmaxtime = 0.7f;

    bool isattackMotion = true;
    int PatternNum = 0;
    Transform player;
    
    public float speed = 3.0f;
    private float distance;
    Transform target;
    CharacterController enemyController;
    Animator anim;
    public int mHp;
    private int maxmHp;

    LineRenderer line;
    Transform Boss;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("CharCenter").transform;
        enemyController = GetComponent<CharacterController>();
        obj = gameObject.GetComponentsInChildren<Transform>();
        maxmHp = GetComponent<csBossMonster>().mHp;
        anim = GetComponent<Animator>();
        monsterAttackPoint = GetComponent<csBossMonster>().monsterAttackPoint;
        //Debug.Log(monsterAttackPoint);
        attackRange.SetActive(false);

        spawn1 = GameObject.Find("Pattern4Point1").transform;
        spawn2 = GameObject.Find("Pattern4Point2").transform;
        spawn3 = GameObject.Find("Pattern4Point3").transform;
        spawn4 = GameObject.Find("Pattern4Point4").transform;


        
    }
    // Update is called once per frame
    void Update()
    {


        if (GameManager.Instance().isGameOver)
        {
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
                if (distance > checkAttackDistance)
                {
                    Pattern1();
                }
                if (stateTime > attackStateMaxTime)
                {
                    stateTime = 0.0f;
                    if (distance < attackableRange + 0.5f)
                    {

                        anim.SetInteger("AniStep", 1);
                        //transform.LookAt(player.parent.transform);
                    }
                        //패턴
                        PatternNum = Random.Range(1, 4);
                        if (PatternNum == 1)
                        {
                       
                            int percentage = Random.Range(1, 100);
                            if (percentage >= 1 && percentage <= 47)
                            {
                                Pattern1();
                                PatternNum = 0;
                                state = STATE.IDLE;
                            }
                        }
                        if (PatternNum == 2)
                        {
                            int percentage = Random.Range(1, 100);
                            if (percentage >= 1 && percentage <= 44)
                            {
                                Pattern2();
                                PatternNum = 0;
                                state = STATE.IDLE;
                            }
                        }
                        if (PatternNum == 3)
                        {
                            int percentage = Random.Range(1, 100);
                            if (percentage >= 1 && percentage <= 48)
                            {
                                Pattern3();
                                PatternNum = 0;
                                state = STATE.IDLE;
                            }
                        }
                        if (PatternNum == 4)
                        {
                            int percentage = Random.Range(1, 100);
                            if (percentage >= 1 && percentage <= 45)
                            {
                                Pattern4();
                                PatternNum = 0;
                                state = STATE.IDLE;
                            }
                        }
                    

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

                //knockbackCount = 10.0f;
                //if (stateTime > idleStateMaxTime)
                //{
                //    stateTime = 0.0f;
                //    state = STATE.IDLE;
                //}
               
                break;

            case STATE.DEAD:
                state = STATE.NONE;
                AudioManager.Instance().PlayFragmentBrokenSound();
                GameManager.Instance().SpawnHealthItem(transform.position);

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
               
                Destroy(gameObject, 0.1f);

                break;

        }
    }

    void FixedUpdate()
    {
        rottime += Time.deltaTime;
        if (isattackMotion)
        {
            for (int i = 1; i < obj.Length; i++)
            {
                if (obj[i].tag == "EnemyDown")
                {
                    obj[i].transform.Rotate(new Vector3(attackMotionSpeed * 0.5f, 0.0f, 0.0f));
                }
            }
        }

        if (rottime > rotmaxtime)
        {
            rottime = 0.0f;
            isattackMotion = false;
        }
    }
    
    public void Damage(int WeaponAttackPoint)
    {
        
        mHp -= WeaponAttackPoint;
        AudioManager.Instance().PlayWeaponHitSound();   //몬스터 피격 사운드
        Instantiate(HitEffect, transform.position, transform.rotation); //몬스터 피격 이펙트
        

        if (mHp <= maxmHp * 0.9)
        {
            //Debug.Log("패턴1 : 날려버리기");
            state = STATE.ATTACK;
            //PatternNum = 1;
            
        }
        if (mHp < maxmHp * 0.7 && mHp <= maxmHp * 0.7)
        {
            //Debug.Log("패턴2 : 내려찍기");
            state = STATE.ATTACK;
            //PatternNum = 2;
            
        }
        if (mHp < maxmHp * 0.5 && mHp <= maxmHp * 0.5)
        {
            //Debug.Log("패턴3 : 충격파 날리기");
            state = STATE.ATTACK;
            //PatternNum = 3;
            //int percentage = Random.Range(1, 100);
            //if (percentage >= 1 && percentage <= 50)
            //{
            //    Debug.Log("방어 자세");
            //    anim.SetInteger("AniStep", 0);
            //    return;

            //}
           
        }

        if (mHp < maxmHp * 0.3 && mHp <= maxmHp * 0.3)
        {
            //Debug.Log("패턴4 : 몬스터 소환");
            state = STATE.ATTACK;
            //PatternNum = 4;
            
        }
       
              
        
        if (mHp <= 0)
        {
            //Debug.Log("사망");
            state = STATE.DEAD;
            GameManager.Instance().SkillGauge(1);
            
        }

    }

    void Fire()
    {
        
        GameObject bullettemp = Instantiate(bullet, firePos.position, Quaternion.identity) as GameObject;

        bullettemp.GetComponent<csBullet>().bulletdamage = monsterAttackPoint;


        Vector3 dir = player.position - bullettemp.transform.position;
        dir.Normalize();
        bullettemp.transform.LookAt(player);
        bullettemp.gameObject.GetComponent<Rigidbody>().AddForce(dir * 1000.0f);

          
    }

    void MakeCollider()
    {
        Vector3 setPos = new Vector3(atkpoint.position.x, transform.position.y + 9.0f, atkpoint.position.z - 1.0f);


        GameObject AttackFieldObj = Instantiate(attackfield, setPos, Quaternion.identity) as GameObject;

        AttackFieldObj.GetComponent<csAttackField>().AttackPower = monsterAttackPoint;


    }
    void MakeCollider2()
    {

        Vector3 setPos2 = new Vector3(atkpoint2.position.x, transform.position.y + 9.0f, atkpoint2.position.z - 1.0f);


        GameObject AttackFieldObj2 = Instantiate(attackfield2, setPos2, Quaternion.identity) as GameObject;

        AttackFieldObj2.GetComponent<csAttackField>().AttackPower = monsterAttackPoint;

    }
    //반격용 콜라이더를 만듬
    public void MakeCollider3()
    {

        Vector3 setPos3 = new Vector3(atkpoint3.position.x, transform.position.y + 9.0f, atkpoint3.position.z);


        GameObject AttackFieldObj3 = Instantiate(attackfield3, setPos3, Quaternion.identity) as GameObject;

        AttackFieldObj3.GetComponent<csCounterField>().AttackPower = monsterAttackPoint;
        //AttackFieldObj3이 해당 스크립트를 가진 오브젝트의 자식으로 들어간다.(해당 오브젝트가 AttackFieldObj3의 부모가 된다.)
        AttackFieldObj3.transform.parent = gameObject.transform;
    }

    void Pattern1()
    {
        //Debug.Log("패턴1 : 날려버리기(원거리) 발동");
        anim.SetInteger("AniStep", 2);
    }
    void Pattern2()
    {
       // Debug.Log("패턴2 : 내려찍기 발동");
        anim.SetInteger("AniStep", 3);
    }
    void Pattern3()   
    {
       // Debug.Log("패턴3 : 양손 치기 발동");
        anim.SetInteger("AniStep", 4);
        //StartCoroutine(MakeRange());
        
    }
    void Pattern4()
    {
       // Debug.Log("패턴4 : 몬스터 소환 발동");
        anim.SetInteger("AniStep", 5);
        MonsterSpawn();

    }
    //IEnumerator MakeRange()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    Boss = GameObject.Find("BossCenter").transform;
    //    Instantiate(attackRange, Boss.position, Quaternion.identity);
    //}
    void MakeRange()
    {
        //Boss = GameObject.Find("BossCenter").transform;
        //Instantiate(attackRange, Boss.position+new Vector3(0,0,9.0f), Quaternion.identity);
        attackRange.SetActive(true);
        StartCoroutine(HideRange());


    }
    IEnumerator HideRange()
    {
        yield return new WaitForSeconds(1.5f);
        attackRange.SetActive(false);
    }
    void MonsterSpawn()
    {
           
        spawn1.GetComponent<BossSummonMonsterPoint>().pointsSpawn();
        spawn2.GetComponent<BossSummonMonsterPoint>().pointsSpawn();
        spawn3.GetComponent<BossSummonMonsterPoint>().pointsSpawn();
        spawn4.GetComponent<BossSummonMonsterPoint>().pointsSpawn();
    }
    

}
