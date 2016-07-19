using UnityEngine;
using System.Collections;

public class csHeal : MonoBehaviour
{
    Transform obj;
    bool once = true;
    float rotspeed = 1.5f;
    float rottime = 0.0f;
    public float rotmaxtime = 0.7f;
    // Use this for initialization
    void Start()
    {
        obj = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.Rotate(new Vector3(0.0f, rotspeed, 0.0f));
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
