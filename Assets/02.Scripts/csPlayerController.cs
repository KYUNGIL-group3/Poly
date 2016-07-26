using UnityEngine;
using System.Collections;
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
                           
				velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
                if(audio.isPlaying)
                {

                }
                else
                {
                    audio.Play();
                }
                //velocity = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0, CrossPlatformInputManager.GetAxis ("Vertical"));
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
               
                isaaaa = true;
				StartCoroutine (isaaab ());
			} 

			if (CrossPlatformInputManager.GetButton ("Attack")) {
                
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
		GameManager.Instance ().isTimeControl = true;
		anim.SetBool ("isSkill", true);
		GameObject moveskillobj = GameObject.Find ("MoveSkillObj");
		isSkill = true;
		isAttack = true;
		ismove = false;
		cameraFollow.enabled = false;
		gameObject.layer = 11;

		pointpos = new Vector3[vec.Count];
        
		for (int a = 0; a < vec.Count; a++) {
			if (vec.Count == 1) {
				
			} else {
				GameObject pointobj = vec [a] as GameObject;
				pointpos [a] = pointobj.transform.position;
			}
		}

		for (int a = 1; a < pointpos.Length; a++) {

			Vector3 dir = ((Vector3)pointpos [a-1] + (Vector3)pointpos [a]) /2.0f;



			//GameObject goTemp = Instantiate (SkillDamage, dir,Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f))) as GameObject;
			
			//dir.Normalize ();
			transform.LookAt ((Vector3)pointpos [a]);

			GameObject goTemp = Instantiate (SkillDamage, dir + new Vector3 (0.0f, 1.0f, 0.0f),
				                    transform.rotation) as GameObject;

			transform.position = (Vector3)pointpos [a];

			//float speed = 0.001f;
			//float step = speed * Time.deltaTime;
			//transform.position = Vector3.MoveTowards (transform.position, (Vector3)pointpos [a], step);

			yield return new WaitForSeconds (skillmovespeed);
		}

		Time.timeScale = 1.0f;
		gameObject.layer = 9;
		cameraFollow.enabled = true;
		ismove = true;
		isSkill = false;
		isAttack = false;

		Transform cameraPos = GameObject.Find ("Main Camera").transform;
		//cameraPos.position += new Vector3 (0.0f, -5.0f, 3.0f);

		cameraPos.gameObject.GetComponent<SmoothFollow>().height = 7;
		anim.SetBool ("isSkill", false);
		GameManager.Instance ().isTimeControl = false;
//		moveskillobj.GetComponent<BoxCollider> ().enabled = false;
		//yield return new WaitForSeconds (1.0f);
		//moveskillobj.GetComponent<TrailRenderer> ().enabled = false;
		//Time.timeScale = 1.0f;
	}
	void SetLookVelocity()
	{
		velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		transform.LookAt (transform.position + velocity);
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

    IEnumerator shake()
	{
		float shake = 0.3f;
		float shakeAmount = 1.5f;
		float decreaseFactor = 1.0f;
		Vector3 originalPos;

		originalPos = MainCamera.position;

		while (shake > 0) {

			MainCamera.position = originalPos + Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;
			yield return new WaitForSeconds (0.01f);
		}

		MainCamera.position = originalPos;

	}

}
