using UnityEngine;
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
    public GameObject bullet;
    public Transform firePos;

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
    float rotspeed = 2.0f;
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
        
            for (int i = 1; i < obj.Length; i++)
            {
                if (obj[i].tag == "Body")
                {
                    obj[i].transform.Rotate(new Vector3(0.0f, rotspeed, 0.0f));
                }
            }

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

                anim.SetInteger("AniStep", 0);

                obj = gameObject.GetComponentsInChildren<Transform>();
                distance = Vector3.Distance(transform.position, player.position);

                if (reloadTime > attackStateMaxTime)
                {

                    reloadTime = 0.0f;
                    stateTime = 0.0f;
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
                    transform.LookAt(player.parent.transform);


                    enemyController.SimpleMove(dir * speed);
                }
                else if (distance < checkAttackDistance - 0.2f)
                {
                    Vector3 dir = player.position - transform.position;
                    dir.y = 0.0f;
                    dir.Normalize();
                    anim.SetInteger("AniStep", 1);
                    transform.LookAt(player.parent.transform);
                    enemyController.SimpleMove(-dir * speed);
                }
                else {
                    transform.LookAt(player.parent.transform);
                }



                break;

            case STATE.ATTACK:

                
                anim.SetInteger("AniStep", 2);
                transform.LookAt(player.parent.transform); 

                //if (stateTime > idleStateMaxTime)
                //{
                //    state = STATE.MOVE;
                //    stateTime = 0.0f;
                //}
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

				obj [i].gameObject.layer = 12;

			}
			obj [0].parent = null;
			obj [0].gameObject.layer = 12;
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

    void Fire()
    {

        GameObject bullettemp = Instantiate(bullet, firePos.position, Quaternion.identity) as GameObject;
        
        bullettemp.GetComponent<csBullet>().bulletdamage = monsterAttackPoint;


        Vector3 dir = player.position - bullettemp.transform.position;
        dir.Normalize();
        bullettemp.transform.LookAt(player);
        bullettemp.gameObject.GetComponent<Rigidbody>().AddForce(dir * 500.0f);

    }
}
