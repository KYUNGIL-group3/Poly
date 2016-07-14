using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csPlayerController : MonoBehaviour {

	public float walkSpeed = 3.0f;
	public float gravity = 20.0f;
	public float jumpSpeed = 8.0f;
	public float skillmovespeed = 0.5f;
    public GameObject DamageFx;
	private Vector3 velocity;

	CharacterController controller = null;
	Animator anim = null;

	Vector3[] pointpos;


	public bool ismove = true;
	public bool isidle = true;
	public bool isAttack = false;
	public bool isSkill = false;
	public GameObject SkillDamage;

	csCameraFollow cameraFollow;

	bool DeadAction = true;

	bool isaaaa = false;



	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		cameraFollow = GameObject.Find ("GameManager").GetComponent<csCameraFollow> ();

	}
	
	// Update is called once per frame
	void Update () {
//		if (isaaaa) {
//			isAttack = true;
//			anim.SetBool ("isAttack", true);
//		}

		if (GameManager.Instance ().isGameOver) {
			if (DeadAction) {
				DeadAction = false;

				Transform[] obj = gameObject.GetComponentsInChildren<Transform> ();
				gameObject.GetComponent<Animator> ().enabled = false;

				for (int i = 1; i < obj.Length; i++) {
					obj [i].gameObject.AddComponent<Rigidbody> ();
					obj [i].gameObject.AddComponent<BoxCollider> ();
					obj [i].gameObject.GetComponent<BoxCollider> ().size = new Vector3 (0.2f, 0.2f, 0.2f);
					obj [i].parent = null;
				}

			}

			return;
		}

		if (GameManager.Instance ().isGameClear)
			return;

		if (ismove) {
			if (!isAttack) {
				//velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				velocity = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0, CrossPlatformInputManager.GetAxis ("Vertical"));
			}
			else{
				velocity = new Vector3 (0, 0, 0);
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
			} else {
				if (!isaaaa) {
					anim.SetBool ("isAttack", false);
				}
				if (velocity.magnitude > 0) {
					anim.SetBool ("isMove", true);
					isidle = false;
					transform.LookAt (transform.position + velocity);
				} else {
					anim.SetBool ("isMove", false);
				}

			}
			velocity.y -= (gravity);
			controller.Move (velocity * Time.deltaTime);
		}
	}

	public IEnumerator StartArrayMove(ArrayList vec)
	{
		anim.SetBool ("isSkill", true);
		GameObject moveskillobj = GameObject.Find ("MoveSkillObj");
		isSkill = true;
		isAttack = true;
		ismove = false;
		cameraFollow.enabled = false;
		gameObject.layer = 11;

		pointpos = new Vector3[vec.Count];
        
		for (int a = 0; a < vec.Count; a++) {
			GameObject pointobj = vec [a] as GameObject;
			pointpos [a] = pointobj.transform.position;
		}

		for (int a = 1; a < pointpos.Length; a++) {

			Vector3 dir = ((Vector3)pointpos [a-1] + (Vector3)pointpos [a]) /2.0f;



			//GameObject goTemp = Instantiate (SkillDamage, dir,Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f))) as GameObject;
			
			//dir.Normalize ();
			transform.LookAt ((Vector3)pointpos [a]);

			GameObject goTemp = Instantiate (SkillDamage, dir + new Vector3(0.0f,1.0f,0.0f),
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
		cameraPos.position += new Vector3 (0.0f, -5.0f, 3.0f);
		anim.SetBool ("isSkill", false);

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
		yield return new WaitForSeconds (0.5f);
		isaaaa = false;
	}

	void NullAttack()
	{
		GameObject weapon = GameObject.FindWithTag ("WEAPON");
		weapon.GetComponent<csWeapon> ().targetreset ();
	}
}
