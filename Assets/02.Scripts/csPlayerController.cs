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

	csCameraFollow cameraFollow;



	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		cameraFollow = GameObject.Find ("GameManager").GetComponent<csCameraFollow> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance ().isGameOver)
			return;

		if (GameManager.Instance ().isGameClear)
			return;

		if (ismove) {
			if (!isAttack) {
				velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				//velocity = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0, CrossPlatformInputManager.GetAxis ("Vertical"));
			}
			else{
				velocity = new Vector3 (0, 0, 0);
			}


				velocity *= walkSpeed;

			if (CrossPlatformInputManager.GetButton ("Attack")) {
				anim.SetBool ("isAttack", true);
				anim.SetBool ("isMove", false);
				isidle = false;
				isAttack = true;
			} else {
				anim.SetBool ("isAttack", false);
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

		moveskillobj.GetComponent<TrailRenderer> ().enabled = true;
		moveskillobj.GetComponent<BoxCollider> ().enabled = true;

		pointpos = new Vector3[vec.Count];
        GameManager.Instance().useSkillGauge(vec.Count);
		for (int a = 0; a < vec.Count; a++) {
			GameObject pointobj = vec [a] as GameObject;
			pointpos [a] = pointobj.transform.position;
		}

		for (int a = 1; a < pointpos.Length; a++) {
			
			//Vector3 dir = (Vector3)pointpos [a] - (Vector3)pointpos [a - 1];
			//dir.Normalize ();
			transform.LookAt ((Vector3)pointpos [a]);

			transform.position = (Vector3)pointpos [a];

			//float speed = 0.001f;
			//float step = speed * Time.deltaTime;
			//transform.position = Vector3.MoveTowards (transform.position, (Vector3)pointpos [a], step);

			yield return new WaitForSeconds (skillmovespeed);
		}

		Time.timeScale = 0.2f;
		gameObject.layer = 9;
		cameraFollow.enabled = true;
		ismove = true;
		isSkill = false;
		isAttack = false;

		Transform cameraPos = GameObject.Find ("Main Camera").transform;
		cameraPos.position += new Vector3 (0.0f, -10.0f, 8.0f);
		anim.SetBool ("isSkill", false);

		moveskillobj.GetComponent<BoxCollider> ().enabled = false;
		yield return new WaitForSeconds (1.0f);
		moveskillobj.GetComponent<TrailRenderer> ().enabled = false;
		Time.timeScale = 1.0f;
	}

	void OkMove()
	{
		isidle = true;
		isAttack = false;
	}
    public void DamageEF()
    {
        Instantiate(DamageFx, transform.position, transform.rotation);
    }
}
