using UnityEngine;
using System.Collections;

public class csEnemyMove : MonoBehaviour {
	enum SPIDERSTATE
	{
		NONE=-1,
		IDLE=0,
		MOVE,
		ATTACK,
		DAMAGE,
		DEAD
	}
	SPIDERSTATE spiderState=SPIDERSTATE.IDLE;

	float stateTime=0.0f;
	public float idleStateMaxTime = 0.5f;
	Transform target;

	public float speed = 5.0f;
	public float rotationSpeed=10.0f;
	public float attackableRange = 0.1f;
	public float attackStateMaxTime=2.0f;
	//public GameObject deadObj = null;

	int hp=5;
	//PlayerState playerState;
	CharacterController playerController;

	void Awake()
	{
		InitSpider ();
	}

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Player").transform;
		playerController = GetComponent<CharacterController> ();
		//playerState = target.GetComponent<PlayerState> ();

	}
	
	// Update is called once per frame
	void Update () {
		switch (spiderState) {
		case SPIDERSTATE.IDLE:
			stateTime += Time.deltaTime;
			if (stateTime > idleStateMaxTime) {
				stateTime = 0.0f;
				spiderState=SPIDERSTATE.MOVE;
			}
			break;
		case SPIDERSTATE.MOVE:
			//GetComponent<Animation> ().Play ("walk");
			float distance = (target.position - transform.position).magnitude;
			if (distance < attackableRange) {
				spiderState = SPIDERSTATE.ATTACK;
				stateTime = attackStateMaxTime;
			} else {
				Vector3 dir = target.position - transform.position;
				dir.y = 0.0f;
				dir.Normalize ();
				playerController.SimpleMove (dir * speed);
				transform.rotation=Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (dir), rotationSpeed * Time.deltaTime);
			}
			break;
		case SPIDERSTATE.ATTACK:
			stateTime += Time.deltaTime;
			if (stateTime > attackStateMaxTime) {
				stateTime = 0.0f;
				//GetComponent<Animation> ().Play ("attack_Melee");
				//GetComponent<Animation> ().PlayQueued ("iddle", QueueMode.CompleteOthers);

			}
			float distance1 = (target.position - transform.position).magnitude;
			if (distance1 > attackableRange) {
				spiderState = SPIDERSTATE.MOVE;
			}
			break;
		case SPIDERSTATE.DAMAGE:
			--hp;
			//GetComponent<Animation> () ["damage"].speed = 0.5f;
			//GetComponent<Animation> ().Play ("damage");
			//GetComponent<Animation> ().PlayQueued ("iddle", QueueMode.CompleteOthers);
			stateTime = 0.0f;
			spiderState = SPIDERSTATE.IDLE;
			if (hp <= 0) {
				spiderState = SPIDERSTATE.DEAD;
				Destroy (gameObject);
			}
			break;
		case SPIDERSTATE.DEAD:
			spiderState = SPIDERSTATE.NONE;
			break;
		}
	}

//	void OnCollisionEnter(Collision coll)
//	{
//		Debug.Log (coll.gameObject.name);
//		if (spiderState == SPIDERSTATE.NONE || spiderState == SPIDERSTATE.DEAD)
//			return;
//		if(coll.gameObject.tag!="WEAPON")
//			return;
//		spiderState = SPIDERSTATE.DAMAGE;
//	}

	void OnTriggerEnter(Collider coll)
	{
		if (spiderState == SPIDERSTATE.NONE || spiderState == SPIDERSTATE.DEAD)
			return;
		if(coll.gameObject.tag!="WEAPON")
			return;
		Vector3 dir = target.position - transform.position;
		dir.y = 0.0f;
		dir.Normalize ();
		playerController.SimpleMove (dir * -speed * 1.0f);
		spiderState = SPIDERSTATE.DAMAGE;
	}

	void InitSpider()
	{
		
		spiderState = SPIDERSTATE.IDLE;
		//GetComponent<Animation> ().Play ("iddle");
	}
}
