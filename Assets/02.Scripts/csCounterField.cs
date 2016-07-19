using UnityEngine;
using System.Collections;

public class csCounterField : MonoBehaviour {
    public int AttackPower;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

            GameManager.Instance().PlayerHealth(AttackPower);
            Destroy(gameObject, 0.5f);
        }
    }
}
