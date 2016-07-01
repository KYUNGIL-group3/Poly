using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csTimeManager : MonoBehaviour {

	public bool timestop = false;

	public float timestoplimit = 0;



	public ArrayList Vec3ArrayList = new ArrayList ();

	public GameObject skillmanager;
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (CrossPlatformInputManager.GetButtonDown ("Skill") && timestop==false) {
			timestop = true;
			Time.timeScale = 0.0f;

			Instantiate (skillmanager, player.transform.position, Quaternion.identity);

		}else if (CrossPlatformInputManager.GetButtonDown ("Skill") && timestop) {
			timestop = false;
			Time.timeScale = 1.0f;
			GameObject PointManager = GameObject.FindWithTag ("PointManager");
			Destroy (PointManager);
		}

		if (timestop) {
			if (Input.GetButton ("Fire1")) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll (ray);
				for (int i = 0; i < hits.Length; i++) {
					RaycastHit hit = hits [i];
					if (hit.transform.tag.Equals ("Point")) {
						if (Vec3ArrayList.Contains (hit.transform.gameObject)) {
						} else {
							Debug.Log ("Copy");
							Vec3ArrayList.Add (hit.transform.gameObject);
							hit.transform.gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 255.0f, 255.0f, 255.0f);
						}
					}
				}
			}
			if (Vec3ArrayList.Count == 20) {
				timestop = false;
				Time.timeScale = 0.2f;
				GameObject PointManager = GameObject.FindWithTag ("PointManager");

				StartCoroutine(player.GetComponent<csPlayerController>().StartArrayMove(Vec3ArrayList));
				Vec3ArrayList.Clear ();
				Destroy (PointManager,3.0f);
			}
		}
	}
}
