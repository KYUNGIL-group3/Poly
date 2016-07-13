using UnityEngine;
using System.Collections;

public class csBullet : MonoBehaviour {
	public int bulletdamage = 30;
    bool once = true;
    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
            if (once)
            {
                once = false;
                GameManager.Instance().PlayerHealth(bulletdamage);
            }
			Destroy (gameObject);
		}
	}
}
