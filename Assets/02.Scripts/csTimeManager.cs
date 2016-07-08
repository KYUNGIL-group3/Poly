using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csTimeManager : MonoBehaviour {

	public bool timestop = false;

	public float timestoplimit = 0;



	GameObject SkillMoveObj;
	public ArrayList Vec3ArrayList = new ArrayList ();

	public GameObject skillmanager;
	public GameObject player;
	public Transform cameraPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (CrossPlatformInputManager.GetButtonDown ("Skill") && timestop==false) {
			timestop = true;
			Time.timeScale = 0.001f;
			player.layer = 2;
			cameraPos.position += new Vector3 (0.0f, 10.0f, -8.0f);
			Instantiate (skillmanager, transform.position, Quaternion.identity);

		}else if (CrossPlatformInputManager.GetButtonDown ("Skill") && timestop) {
			timestop = false;
			Time.timeScale = 0.2f;
			GameObject PointManager = GameObject.FindWithTag ("PointManager");


			StartCoroutine(player.GetComponent<csPlayerController>().StartArrayMove(Vec3ArrayList));
			Vec3ArrayList.Clear ();
			Destroy (PointManager,1.0f);
		}

		if (timestop) {
			if (Vec3ArrayList.Count == 20) {
				timestop = false;
				Time.timeScale = 0.2f;
				GameObject PointManager = GameObject.FindWithTag ("PointManager");

				StartCoroutine(player.GetComponent<csPlayerController>().StartArrayMove(Vec3ArrayList));
				Vec3ArrayList.Clear ();
				Destroy (PointManager,1.0f);
			}

			if (Input.GetButton ("Fire1")) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll (ray);
				for (int i = 0; i < hits.Length; i++) {
					RaycastHit hit = hits [i];
					if (hit.transform.tag.Equals ("Point")) {
						//Debug.Log (Vector3.Distance (SkillMoveObj.transform.position, hit.transform.position));
						//Debug.Log (hit.transform.position);

						SkillMoveObj = GameObject.Find("SkillMoveObj");
						if (Vector3.Distance (SkillMoveObj.transform.position, hit.transform.position) < 1.5f) {
							
							if (Vec3ArrayList.Contains (hit.transform.gameObject)) {
							
							} else {
								Vec3ArrayList.Add (hit.transform.gameObject);
								SkillMoveObj.transform.position = hit.transform.position;
								hit.transform.gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 255.0f, 255.0f, 255.0f);
							}
						}

					}
				}
			}

		}
	}
}
