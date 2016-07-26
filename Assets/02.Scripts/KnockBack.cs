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
    void Knockback()
    {
        gameObject.GetComponent<CharacterController>().Move(new Vector3(0.0f, 0.0f, -2.0f));
    }

    void OnTriggerEnter(Collider col)   
    {
        if (col.gameObject.tag == "WEAPON")
        {
            //Debug.Log("접촉");
            Knockback();
        }

    }

}
