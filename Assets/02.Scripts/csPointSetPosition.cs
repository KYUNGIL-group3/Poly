using UnityEngine;
using System.Collections;

public class csPointSetPosition : MonoBehaviour {

	public GameObject pointsprite;
	public int pointcount;
	float del = 0.0f;
	bool once = false;
	int layerMask = 1 << 14;
	GameObject SkillMoveObj;
	Transform[] pointRender;
	// Use this for initialization
	void Start () {
		pointcount++;
		if (pointcount > 20) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (once) {
			return;
		}

		if (del > 0.001f) {
			once = true;
			RaycastHit hit;

			if (Physics.Raycast (transform.position, Vector3.forward, out hit, 1.0f, 1 << 14)
				|| Physics.Raycast (transform.position, Vector3.forward, out hit, 1.0f, 1 << 13)) {

			} else {
				GameObject goTemp = Instantiate (pointsprite, transform.position + Vector3.forward * 1.0f,
					Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f))) as GameObject;
				goTemp.transform.parent = gameObject.transform.parent;
				goTemp.GetComponent<csPointSetPosition> ().pointcount = this.pointcount;
			}


			if (Physics.Raycast (transform.position, Vector3.back, out hit, 1.0f, 1 << 14)
				|| Physics.Raycast (transform.position, Vector3.back, out hit, 1.0f, 1 << 13)) {

			} else {
				GameObject goTemp = Instantiate (pointsprite, transform.position + Vector3.back * 1.0f,
					Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f))) as GameObject;
				goTemp.transform.parent = gameObject.transform.parent;
				goTemp.GetComponent<csPointSetPosition> ().pointcount = this.pointcount;
			}

			if (Physics.Raycast (transform.position, Vector3.right, out hit, 1.0f, 1 << 14)
				||Physics.Raycast (transform.position, Vector3.right, out hit, 1.0f, 1 << 13)) {

			} else {
				GameObject goTemp = Instantiate (pointsprite, transform.position + Vector3.right * 1.0f,
					Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f))) as GameObject;
				goTemp.transform.parent = gameObject.transform.parent;
				goTemp.GetComponent<csPointSetPosition> ().pointcount = this.pointcount;
			}

			if (Physics.Raycast (transform.position, Vector3.left, out hit, 1.0f, 1 << 14)
				||Physics.Raycast (transform.position, Vector3.left, out hit, 1.0f, 1 << 13)) {

			} else {
				GameObject goTemp = Instantiate (pointsprite, transform.position + Vector3.left * 1.0f,
					Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f))) as GameObject;
				goTemp.transform.parent = gameObject.transform.parent;
				goTemp.GetComponent<csPointSetPosition> ().pointcount = this.pointcount;
			}

		} else {
			del += Time.unscaledDeltaTime;
		}

		if (once) {
			SkillMoveObj = GameObject.Find("SkillMoveObj");
			pointRender = GetComponentsInChildren<Transform> ();
			if (Vector3.Distance
				(new Vector3 (SkillMoveObj.transform.position.x, 0.0f, SkillMoveObj.transform.position.z),
					new Vector3 (gameObject.transform.position.x, 0.0f, gameObject.transform.position.z)) < 1.5f) {
				if (pointcount != 1) {
					pointRender [1].gameObject.GetComponent<SpriteRenderer> ().color = new Color (255.0f, 255.0f, 255.0f, 0.5f);
					StartCoroutine (PointColorSet ());
				}
			}
			return;
		}


	}

	IEnumerator PointColorSet()
	{
		yield return new WaitForSeconds (0.0005f);
		pointRender = GetComponentsInChildren<Transform> ();
		pointRender [1].gameObject.GetComponent<SpriteRenderer> ().color = new Color (255.0f, 255.0f, 255.0f, 1.0f);
		yield return new WaitForSeconds (0.0005f);
		pointRender = GetComponentsInChildren<Transform> ();
		pointRender [1].gameObject.GetComponent<SpriteRenderer> ().color = new Color (255.0f, 255.0f, 255.0f, 0.5f);
		yield return new WaitForSeconds (0.0005f);
		pointRender = GetComponentsInChildren<Transform> ();
		pointRender [1].gameObject.GetComponent<SpriteRenderer> ().color = new Color (255.0f, 255.0f, 255.0f, 1.0f);
		yield return new WaitForSeconds (0.0005f);
		pointRender = GetComponentsInChildren<Transform> ();
		pointRender [1].gameObject.GetComponent<SpriteRenderer> ().color = new Color (255.0f, 255.0f, 255.0f, 0.5f);
		yield return new WaitForSeconds (0.0005f);
		pointRender = GetComponentsInChildren<Transform> ();
		pointRender [1].gameObject.GetComponent<SpriteRenderer> ().color = new Color (255.0f, 255.0f, 255.0f, 1.0f);
	}
}
