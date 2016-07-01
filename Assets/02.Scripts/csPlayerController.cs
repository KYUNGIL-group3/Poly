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
	public float thisdis = 0.0f;
	public float maxdis = 100.0f;
	Vector3[] pointpos;


	public bool isAttack = false;



	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();

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
		ismove = false;

		gameObject.layer = 11;

		pointpos = new Vector3[20];
		for (int a = 0; a < vec.Count - 1; a++) {
			GameObject pointobj = vec [a] as GameObject;
			pointpos [a] = pointobj.transform.position;
		}

		for (int a = 0; a < pointpos.Length - 1; a++) {
			Vector3 dir = (Vector3)pointpos [a + 1] - (Vector3)pointpos [a];

			//float distance1 = Vector3.Distance ((Vector3)pointpos [a + 1], (Vector3)pointpos [a]);

			dir.Normalize ();
			if (dir != Vector3.zero) {
				Quaternion moveQtn = Quaternion.LookRotation (dir);

				transform.rotation = Quaternion.Lerp (transform.rotation,
					moveQtn,
					30.0f * Time.deltaTime);
			}

			transform.position = (Vector3)pointpos [a];
			yield return new WaitForSeconds (skillmovespeed);
		}

		Time.timeScale = 1.0f;
		gameObject.layer = 9;
		ismove = true;
	}

	void OkMove()
	{
		isAttack = false;
	}
}
