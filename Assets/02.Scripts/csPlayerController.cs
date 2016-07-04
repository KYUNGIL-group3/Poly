using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csPlayerController : MonoBehaviour {

	public float walkSpeed = 3.0f;
	public float gravity = 20.0f;
	public float jumpSpeed = 8.0f;
	public float skillmovespeed = 0.02f;
	private Vector3 velocity;

	CharacterController controller = null;
	Animator anim = null;

	bool ismove = true;
	Vector3[] pointpos;


	public bool isAttack = false;

	public csRunFollow cameraFollow;



	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		cameraFollow = GameObject.Find ("FollowChar").GetComponent<csRunFollow> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (ismove) {
			if (!isAttack) {
				velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			}
			else{
				velocity = new Vector3 (0, 0, 0);
			}


				//velocity = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0, CrossPlatformInputManager.GetAxis ("Vertical"));
				velocity *= walkSpeed;

				if (CrossPlatformInputManager.GetButton ("Attack")) {
					anim.SetBool ("isAttack", true);
					anim.SetBool ("isMove", false);
					isAttack = true;
				} else {
					anim.SetBool ("isAttack", false);
					if (velocity.magnitude > 0) {
						anim.SetBool ("isMove", true);
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

		ismove = false;
		cameraFollow.enabled = false;
		gameObject.layer = 11;

		pointpos = new Vector3[20];
		for (int a = 0; a < vec.Count; a++) {
			GameObject pointobj = vec [a] as GameObject;
			pointpos [a] = pointobj.transform.position;
		}

		for (int a = 1; a < pointpos.Length; a++) {
			
			//Vector3 dir = (Vector3)pointpos [a] - (Vector3)pointpos [a - 1];
			//dir.Normalize ();
			transform.LookAt ((Vector3)pointpos [a]);
			transform.position = (Vector3)pointpos [a];

			yield return new WaitForSeconds (skillmovespeed);
		}

		Time.timeScale = 1.0f;
		gameObject.layer = 9;
		cameraFollow.enabled = true;
		ismove = true;
		anim.SetBool ("isSkill", false);
	}

	void OkMove()
	{
		isAttack = false;
	}
}
