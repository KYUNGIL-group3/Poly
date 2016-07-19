using UnityEngine;
using System.Collections;

public class KnockBack : MonoBehaviour {
    //public GameObject Player;
    //bool isKnockbacking = false;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }
    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.tag == "CounterField")
    //    {
    //        Debug.Log("타겟 적중");
    //        isKnockbacking = true;
    //        Knockback(Player);
            
    //    }
    //}
    //void Knockback(GameObject other)
    //{
    //    Rigidbody rigid = other.gameObject.AddComponent<Rigidbody>();
    //    rigid.AddForce(-transform.forward,ForceMode.Impulse);
    //    if (isKnockbacking == true)
    //    {
    //        isKnockbacking = false;
    //        rigid=GetComponent<Rigidbody>();
    //        Destroy(rigid);
    //    }
        
        
    //}
    IEnumerator Knockback(GameObject other)
    {
        float startKnockback = Time.time;
        while (Time.time <= (startKnockback + .2))
        {
            Debug.Log("aaa");
            other.transform.Translate(0, 0, -20 * Time.deltaTime);
            yield return null;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "CounterField")
        {
            Debug.Log("접촉");
            StartCoroutine(Knockback(hit.gameObject));
        }

    }

}
