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
    public float idleStateMaxTime = 1.0f;   //대기시간,경직시간
    public float checkMoveDistance = 20.0f; //몬스터 시야
    public float attackableRange = 20.0f;    //공격범위
    public float attackStateMaxTime = 2.0f; //공격대기시간
    public float checkAttackDistance = 20.0f; //견제공격 범위
    public int monsterAttackPoint;  //몬스터 공격력
    
    public GameObject attackfield;
    public GameObject attackfield2;
   
    public Transform atkpoint;
    public Transform atkpoint2;
    
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
    public int mHp;
    private int maxmHp;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("CharCenter").transform;
        enemyController = GetComponent<CharacterController>();
        obj = gameObject.GetComponentsInChildren<Transform>();
       
        anim = GetComponent<Animator>();
        monsterAttackPoint = GetComponent<csBossMonster>().monsterAttackPoint;
        Debug.Log(monsterAttackPoint);


    }

    // Update is called once per frame
    void Update()
    {


        if (GameManager.Instance().isGameOver)
        {
            return;
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

                if (stateTime > attackStateMaxTime)
                {
                    stateTime = 0.0f;
                    if (distance < attackableRange + 0.5f)
                    {
                        
                        anim.SetInteger("AniStep", 1);
                        //transform.LookAt(player.parent.transform);
                    }
                       
                   
                }

                break;

            case STATE.DAMAGE:


                if (mHp <= 0)
                {
                    state = STATE.DEAD;
                    GameManager.Instance().SkillGauge(1);
                    break;
                }

                stateTime += Time.deltaTime;
                //knockbackCount-=Time.deltaTime*20.0f;
                //if (knockbackCount > 0)
                //{
                //    gameObject.GetComponent<CharacterController>().Move(new Vector3(0.0f, 0.0f, -(knockbackCount) * Time.deltaTime));

                //}

                //knockbackCount = 10.0f;
                if (stateTime > idleStateMaxTime)
                {
                    stateTime = 0.0f;
                    state = STATE.IDLE;
                }


                break;

            case STATE.DEAD:
                state = STATE.NONE;

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


        stateTime = 0.0f;
        mHp -= WeaponAttackPoint;

        state = STATE.DAMAGE;
    }
   
    void MakeCollider()
    {
        Vector3 setPos = new Vector3(atkpoint.position.x, 0, atkpoint.position.z-1.0f);


        GameObject AttackFieldObj = Instantiate(attackfield, setPos, Quaternion.identity) as GameObject;

        AttackFieldObj.GetComponent<csAttackField>().AttackPower = monsterAttackPoint;


    }
    void MakeCollider2()
    {

        Vector3 setPos2 = new Vector3(atkpoint2.position.x, 0, atkpoint2.position.z-1.0f);


        GameObject AttackFieldObj2 = Instantiate(attackfield2, setPos2, Quaternion.identity) as GameObject;

        AttackFieldObj2.GetComponent<csAttackField>().AttackPower = monsterAttackPoint;

    }
   
}
