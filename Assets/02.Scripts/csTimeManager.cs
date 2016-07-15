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
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		
		if (CrossPlatformInputManager.GetButtonDown ("Skill") && timestop==false
			&& GameManager.Instance().gauge >=5 ) {
			GameManager.Instance ().isTimeControl = true;
			timestop = true;
			Time.timeScale = 0.001f;

			Transform[] temp = player.GetComponentsInChildren<Transform> ();
			for (int i = 0; i < temp.Length; i++) {
				temp [i].gameObject.layer = 2;
			}
			//cameraPos.position += new Vector3 (0.0f, 5.0f, -3.0f);
			cameraPos.gameObject.GetComponent<SmoothFollow>().height = 15;
			Vec3ArrayList = new ArrayList ();
			Instantiate (skillmanager, transform.position, Quaternion.identity);



		}else if (CrossPlatformInputManager.GetButtonDown ("Skill") && timestop) {
			timestop = false;

			GameManager.Instance ().isTimeControl = false;
			Time.timeScale = 0.2f;
			GameObject PointManager = GameObject.FindWithTag ("PointManager");


			StartCoroutine(player.GetComponent<csPlayerController>().StartArrayMove(Vec3ArrayList));
			Vec3ArrayList.Clear ();

			Destroy (PointManager);
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
								hit.transform.gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 255.0f, 255.0f, 255.0f);
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
}
