using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class csPlayerController : MonoBehaviour {

	public float walkSpeed = 3.0f;
	public float gravity = 20.0f;
	public float jumpSpeed = 8.0f;
	public float skillmovespeed = 0.5f;
    public GameObject DamageFx;
    public GameObject bullet;
   
    public float reloadTime = 4.0f;
    public float reloadmaxTime = 5.0f;
    float knockbackCount = 10.0f;
    private Vector3 velocity;
    bool isKnockBacking = false;
    CharacterController controller = null;

	public GameObject Rhand;
   
	Animator anim = null;

	Vector3[] pointpos;
    //플레이어와 적의 거리는 항상 바뀌므로 고정된 벡터 값을 저장하는 변수 추가
    Vector3 nomalizedDistance;

	public bool ismove = true;
	public bool isidle = true;
	public bool isAttack = false;
	public bool isSkill = false;
	public GameObject SkillDamage;

	csCameraFollow cameraFollow;

	Transform MainCamera;

	bool DeadAction = true;

	bool isaaaa = false;


	GameObject[] parentlist;
	GameObject[] childlist;
    AudioSource audio;
	public GameObject swordSkillEffect1;
	public GameObject swordSkillEffect2;
	public GameObject hammerSkillEffect1;
	public GameObject hammerSkillEffect2;

	float eventmaxtime = 4.0f;
    // Use this for initialization
    void Start () {
        
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		cameraFollow = GameObject.Find ("GameManager").GetComponent<csCameraFollow> ();
		MainCamera = GameObject.Find ("Main Camera").transform;
		GameManager.Instance ().isGameOver = false;
        AudioManager.Instance().PlayPlayerRevivalSound();
        audio = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        //		if (isaaaa) {
        //			isAttack = true;
        //			anim.SetBool ("isAttack", true);
        //		}
       
        if (GameManager.Instance ().isTimeControl) {
			return;
		}
        
		if (GameManager.Instance ().isGameOver) {
			if (DeadAction) {
				DeadAction = false;

				Transform[] obj = gameObject.GetComponentsInChildren<Transform> ();
				gameObject.GetComponent<Animator> ().enabled = false;

//				for (int j = 1; j < obj.Length; j++) {
//					parentlist [j] = obj [j].parent.gameObject;
//					childlist [j] = obj [j].gameObject;
//				}

				for (int i = 1; i < obj.Length; i++) {
					obj [i].gameObject.AddComponent<Rigidbody> ();
					obj [i].gameObject.AddComponent<BoxCollider> ();
					obj [i].gameObject.GetComponent<BoxCollider> ().size = new Vector3 (0.2f, 0.2f, 0.2f);
					obj [i].parent = null;
					Destroy (obj [i].gameObject, 3.0f);
				}
                AudioManager.Instance().PlayFragmentBrokenSound();
				Destroy (gameObject);
				//StartCoroutine (par ());
			}

			return;
		}

		if (GameManager.Instance ().isGameClear) {

			anim.SetBool ("isMove", false);

			anim.SetBool ("isAttack", false);
			return;
		}

		if (ismove) {
            
            if (!isAttack) { 
                           
				//velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				velocity = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0, CrossPlatformInputManager.GetAxis ("Vertical"));
                if(audio.isPlaying)
                {

                }
                else
                {
                    audio.Play();
                }
            }
			else{
				velocity = new Vector3 (0, 0, 0);

			}
            if (isKnockBacking == true)
            {

                //gameObject.GetComponent<Animator>().enabled = false;
                KnockBack();
                return;
            }


            velocity *= walkSpeed;
			if (CrossPlatformInputManager.GetButtonDown ("Attack")) {
				//if (Input.GetKeyDown("j")) {
               
                isaaaa = true;
				StartCoroutine (isaaab ());
			} 

			if (CrossPlatformInputManager.GetButton ("Attack")) {
			//if (Input.GetKey("j")) {
                anim.SetBool ("isAttack", true);
				anim.SetBool ("isMove", false);
				isidle = false;
				isAttack = true;
                audio.Stop();
            } else {

				if (!isaaaa) {
					anim.SetBool ("isAttack", false);
				}
				if (velocity.magnitude > 0) {
					anim.SetBool ("isMove", true);
                    //AudioManager.Instance().PlayPlayerMoveSound();
					isidle = false;
					transform.LookAt (transform.position + velocity);
				} else {
					anim.SetBool ("isMove", false);
                    audio.Stop();

                }

			}
           
            velocity.y -= (gravity);
			controller.Move (velocity * Time.deltaTime);
		}
        

    }
    

	public IEnumerator StartArrayMove(ArrayList vec)
	{
        AudioManager.Instance().PlaySkillActiveSound();
		//GameManager.Instance ().isTimeControl = true;
		Debug.Log("1");
		anim.SetBool ("isMove", false);
		anim.SetBool ("isSkill", true);
		Debug.Log("2");
		gameObject.GetComponent<Animator> ().updateMode = AnimatorUpdateMode.UnscaledTime;
		GameObject moveskillobj = GameObject.Find ("MoveSkillObj");
		Debug.Log("3");
		isSkill = true;
		isAttack = true;
		ismove = false;
		cameraFollow.enabled = false;
		gameObject.layer = 11;
		pointpos = new Vector3[vec.Count];

		Debug.Log("4");
        
		for (int a = 0; a < vec.Count; a++) {
			if (vec.Count == 1) {
				
			} else {
				GameObject pointobj = vec [a] as GameObject;
				pointpos [a] = pointobj.transform.position;
			}
		}
		transform.LookAt ((Vector3)pointpos [1]);
		if (Rhand.GetComponent<csAddWeapon> ().WeaponNum == 0) {
			Instantiate (swordSkillEffect1, transform.position + new Vector3 (0.0f, 0.5f, 0.0f), Quaternion.identity);
		} else {
			transform.LookAt ((Vector3)pointpos [1]);


			Instantiate (swordSkillEffect1, transform.position + new Vector3 (0.0f, 0.5f, 0.0f), Quaternion.identity);
		}

		yield return WaitForRealTime (eventmaxtime);
		anim.SetBool ("isSkill", false);
		for (int a = 1; a < pointpos.Length; a++) {

			Vector3 dir = ((Vector3)pointpos [a-1] + (Vector3)pointpos [a]) /2.0f;



			//GameObject goTemp = Instantiate (SkillDamage, dir,Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f))) as GameObject;
			
			//dir.Normalize ();
			//transform.LookAt ((Vector3)pointpos [a]);

			GameObject goTemp = Instantiate (SkillDamage, dir + new Vector3 (0.0f, 1.0f, 0.0f),
				                    transform.rotation) as GameObject;
			Vector3 lookVec3 = (Vector3)pointpos [a];
			lookVec3.y = goTemp.transform.position.y;
			goTemp.transform.LookAt (lookVec3);
			if (Rhand.GetComponent<csAddWeapon> ().WeaponNum == 0) {

				transform.LookAt ((Vector3)pointpos [a]);
				transform.position = (Vector3)pointpos [a];
				Instantiate (swordSkillEffect2,
					lookVec3 + Random.insideUnitSphere * 1.0f,
					Quaternion.identity);
			} else if (Rhand.GetComponent<csAddWeapon> ().WeaponNum == 4) {
				//Vector3 lookVec3_1 = (Vector3)pointpos [1];
				//transform.LookAt (lookVec3_1);


				lookVec3.y = transform.position.y;
				Instantiate (hammerSkillEffect2,
					lookVec3,
					Quaternion.identity);
			} else {
				//Vector3 lookVec3_1 = (Vector3)pointpos [1];
				//transform.LookAt (lookVec3_1);


				Instantiate (swordSkillEffect2,
					lookVec3 + Random.insideUnitSphere * 1.0f,
					Quaternion.identity);
			}
			

			//float speed = 0.001f;
			//float step = speed * Time.deltaTime;
			//transform.position = Vector3.MoveTowards (transform.position, (Vector3)pointpos [a], step);

			//yield return new WaitForSeconds (skillmovespeed);
			yield return WaitForRealTime (0.01f);
		}

		yield return WaitForRealTime (2.0f);

		Time.timeScale = 1.0f;
		gameObject.layer = 9;
		cameraFollow.enabled = true;
		ismove = true;
		isSkill = false;
		isAttack = false;
		gameObject.GetComponent<Animator> ().updateMode = AnimatorUpdateMode.Normal;
		Transform cameraPos = GameObject.Find ("Main Camera").transform;
		//cameraPos.position += new Vector3 (0.0f, -5.0f, 3.0f);

		cameraPos.gameObject.GetComponent<SmoothFollow>().height = 7;
		GameManager.Instance ().isTimeControl = false;
//		moveskillobj.GetComponent<BoxCollider> ().enabled = false;
		//yield return new WaitForSeconds (1.0f);
		//moveskillobj.GetComponent<TrailRenderer> ().enabled = false;
		//Time.timeScale = 1.0f;
	}
	void SetLookVelocity()
	{
		//velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		velocity = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0, CrossPlatformInputManager.GetAxis ("Vertical"));

		//transform.LookAt (transform.position + velocity);

		if (velocity.magnitude > 0) {
			transform.LookAt (transform.position + velocity);
			return;
		}

		GameObject[] targets;

		targets = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject target in targets) {
			float targetDis = Vector3.Distance (transform.position, target.transform.position);

			if (targetDis <= 5) {
				Vector3 targetPos = target.transform.position;
				targetPos.y = transform.position.y;
				transform.LookAt (targetPos);
				break;
			}
		}

	}

	void OkMove()
	{
		isidle = true;
		isAttack = false;
	}
    public void DamageEF()
    {
        
		GameObject damageFxObj =  Instantiate(DamageFx, transform.position, transform.rotation) as GameObject;
		Destroy (damageFxObj, 1.5f);
    }

	IEnumerator isaaab()
	{
		yield return new WaitForSeconds (0.2f);
		isaaaa = false;
	}

	IEnumerator par()
	{
		yield return new WaitForSeconds (10.0f);
		//Transform[] obj = gameObject.GetComponentsInChildren<Transform> ();
		gameObject.GetComponent<Animator> ().enabled = true;

		for (int j = 1; j < childlist.Length; j++) {
			childlist [j].transform.parent = parentlist [j].transform;
		}

	}

	void NullAttack()
	{
		GameObject weapon = GameObject.FindWithTag ("WEAPON");
		weapon.GetComponent<csWeapon> ().targetreset ();
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "CounterField")
        {
            Vector3 distance = transform.position - col.transform.parent.position;
            distance.Normalize();
            nomalizedDistance = distance;
            
            isKnockBacking = true;
        }

    }
    void KnockBack()
    {
        knockbackCount -= Time.deltaTime * 20.0f;
        if (knockbackCount > 0)
        {
            //gameObject.GetComponent<CharacterController>().Move(new Vector3(0.0f, 0.0f, -(knockbackCount) * Time.deltaTime));
            gameObject.GetComponent<CharacterController>().Move(nomalizedDistance*15.0f*Time.deltaTime);
        }
        else {

            isKnockBacking = false;
            knockbackCount = 10.0f;
        }

    }
    void Fire()
    {
        AudioManager.Instance().PlayArrowShotSound();
        Transform firePos;
        Transform firePos2;
        //firePos = GameObject.FindWithTag("firePos1").transform;
        //firePos2 = GameObject.FindWithTag("firePos2").transform;
        firePos = GameObject.Find("firePos1").transform;
        firePos2 = GameObject.Find("firePos2").transform;

        GameObject bullettemp = Instantiate(bullet, firePos.position, firePos.rotation)as GameObject;
        
        Vector3 dir = firePos.position - firePos2.position;

        dir.Normalize();
        dir.y = 0.0f;
        bullettemp.gameObject.GetComponent<Rigidbody>().AddForce(dir * 900.0f);
        //bullettemp.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 900.0f);

    }
    void PlayWeaponSwing()
    {
        AudioManager.Instance().PlayWeaponSwingSound();
    }
    void PlayWeaponSwing2()
    {
        AudioManager.Instance().PlayWeaponSwingSound2();
    }

	void shake()
	{
		StartCoroutine (GameManager.Instance ().shake (0.0f));
	}

	public static IEnumerator WaitForRealTime(float delay){
		while(true){
			float pauseEndTime = Time.realtimeSinceStartup + delay;
			while (Time.realtimeSinceStartup < pauseEndTime){
				yield return 0;
			}
			break;
		}
	}

}
