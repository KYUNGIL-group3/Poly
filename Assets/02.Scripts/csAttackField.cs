using UnityEngine;
using System.Collections;

public class csAttackField : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {

			GameManager.Instance ().PlayerHealth (30);
            Destroy(gameObject,0.5f);
        }
        
    }
}
