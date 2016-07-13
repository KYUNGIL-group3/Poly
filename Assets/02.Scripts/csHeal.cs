using UnityEngine;
using System.Collections;

public class csHeal : MonoBehaviour
{
    bool once = true;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //중복충돌로 인해 회복의 중첩을 막고자 bool타입 once추가
            if (once)
            {
                once = false;
                GameManager.Instance().RecoveryHealth(100);
                Destroy(gameObject);
            }
        }
    }
}
