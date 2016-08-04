using UnityEngine;
using System.Collections;

public class csBossSlash : MonoBehaviour {
	public int bulletdamage;
	bool once = true;
	// Use this for initialization
	void Start () {
		//오브젝트가 부모로부터 떨어졌을때만 2초 뒤에 삭제함
		if(gameObject.transform.parent == null)
			Destroy(gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col)
	{
		if (gameObject.transform.parent != null) {
			return;
		}

		if (col.gameObject.layer == 13) {

			Destroy (gameObject);
		}
		if (col.gameObject.tag == "Player") {
			AudioManager.Instance().PlayArrowHitSound();    //화살 피격 사운드
			if (once)
			{
				once = false;
				GameManager.Instance().PlayerHealth(bulletdamage);
			}

			Destroy (gameObject);
		}


	}
}
