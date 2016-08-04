using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class csTimeManager : MonoBehaviour {

	public bool timestop = false;

	public float timestoplimit = 0;

	ArrayList Vec3ArrayList;

	GameObject SkillMoveObj;

	public GameObject skillmanager;
	public GameObject player;
	public Transform cameraPos;
	public GameObject MobileSingleStickControl;

	int returnGauge;

	bool once = true;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.Instance ().isGameOver) {
			return;
		}

		if (GameManager.Instance ().restartcount == 1) {
			player = GameObject.Find ("Player(Clone)");
		}

		if (CrossPlatformInputManager.GetButtonDown ("Skill") && timestop==false
			&& GameManager.Instance().gauge >=5 ) {
			once = true;
            AudioManager.Instance().PlayTimeSkillOnSound();
			MobileSingleStickControl.GetComponent<csITweenManagerButton> ().ActionStart ();
			GameManager.Instance ().isTimeControl = true;
			timestop = true;
			Time.timeScale = 0.0f;

			returnGauge = GameManager.Instance ().gauge;

//			Transform[] temp = player.GetComponentsInChildren<Transform> ();
//			for (int i = 0; i < temp.Length; i++) {
//				temp [i].gameObject.layer = 2;
//			}
			//cameraPos.position += new Vector3 (0.0f, 5.0f, -3.0f);
			cameraPos.gameObject.GetComponent<SmoothFollow>().height = 15;
			Vec3ArrayList = new ArrayList ();
			Instantiate (skillmanager, transform.position, Quaternion.identity);



		}else if (CrossPlatformInputManager.GetButtonDown ("Skill") && timestop) {
			timestop = false;
			GameObject PointManager = GameObject.FindWithTag ("PointManager");

			if (Vec3ArrayList.Count == 1) {
				timestop = false;
				MobileSingleStickControl.GetComponent<csITweenManagerButton> ().ActionStart ();
				GameManager.Instance ().gauge = returnGauge;
				GameManager.Instance ().isTimeControl = false;
				Time.timeScale = 1.0f;
				cameraPos.gameObject.GetComponent<SmoothFollow>().height = 7;
			} else {
				StartCoroutine (ActionStart ());
				StartCoroutine(player.GetComponent<csPlayerController>().StartArrayMove(Vec3ArrayList));
			}

			Vec3ArrayList.Clear ();
			Destroy (PointManager);
		}else if(CrossPlatformInputManager.GetButtonDown ("SkillCancel") && timestop)
		{
			timestop = false;
			MobileSingleStickControl.GetComponent<csITweenManagerButton> ().ActionStart ();
			GameManager.Instance ().gauge = returnGauge;
			GameManager.Instance ().isTimeControl = false;
			Time.timeScale = 1.0f;
			GameObject PointManager = GameObject.FindWithTag ("PointManager");
			Vec3ArrayList.Clear ();

			Destroy (PointManager);
//			Transform[] temp = player.GetComponentsInChildren<Transform> ();
//			for (int i = 0; i < temp.Length; i++) {
//				temp [i].gameObject.layer = 9;
//			}
			cameraPos.gameObject.GetComponent<SmoothFollow>().height = 7;
		}

		if (timestop) {

			if (Input.GetButton ("Fire1")) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll (ray);
				for (int i = 0; i < hits.Length; i++) {
					RaycastHit hit = hits [i];
					if (hit.transform.tag.Equals ("Point")) {

						SkillMoveObj = GameObject.Find("SkillMoveObj");

						if (Vector3.Distance
							(new Vector3 (SkillMoveObj.transform.position.x, 0.0f, SkillMoveObj.transform.position.z),
							    new Vector3 (hit.transform.position.x, 0.0f, hit.transform.position.z)) < 1.5f) {
							
							if (Vec3ArrayList.Contains (hit.transform.gameObject)) {
								

							} else if (GameManager.Instance ().gauge >= 5) {
								if (Vec3ArrayList.Count != 0) {
									GameManager.Instance ().gauge -= 5;
								}
							
								Vec3ArrayList.Add (hit.transform.gameObject);
								SkillMoveObj.transform.position = hit.transform.position;
								AudioManager.Instance ().PlaySkillPointSound ();
								hit.transform.gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 255.0f, 255.0f, 255.0f);
								if (once) {
									once = false;
									GameObject SkillManager = GameObject.Find ("SkillManager(Clone)");
									Transform[] pointRender = SkillManager.GetComponentsInChildren<Transform> ();
									for(int j = 0 ;j<pointRender.Length;j++)
									{
										if (pointRender [j].gameObject.GetComponent<SpriteRenderer> () != null) {
											if (Vec3ArrayList.Contains (pointRender [j].gameObject) == false) {
												pointRender [j].gameObject.GetComponent<SpriteRenderer> ().color = new Color (255.0f, 255.0f, 255.0f, 1.0f);
											}
										}
									}
								}
							}
						}
						break;
					}
				}
			}

		}
	}

	public void FirstPoint(GameObject obj)
	{
		Vec3ArrayList.Add (obj);
	}

	IEnumerator ActionStart()
	{
		yield return new WaitForSeconds (0.1f);
		MobileSingleStickControl.GetComponent<csITweenManagerButton> ().ActionStart ();
	}
}
