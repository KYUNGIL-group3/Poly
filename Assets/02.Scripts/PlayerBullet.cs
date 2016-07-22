using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    public int bulletdamage = 40;
    bool once = true;
    // Use this for initialization
    void Start()
    {
        //오브젝트가 부모로부터 떨어졌을때만 2초 뒤에 삭제함
        if (gameObject.transform.parent == null)
            Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (gameObject.transform.parent != null)
        {
            return;
        }

        if (col.gameObject.layer == 13)
        {

            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            if (once)
            {
                once = false;
                if (col.gameObject.GetComponent<csEnemy1>())
                    col.gameObject.GetComponent<csEnemy1>().Damage(bulletdamage);
                if (col.gameObject.GetComponent<csEnemy2>())
                    col.gameObject.GetComponent<csEnemy2>().Damage(bulletdamage);
                if (col.gameObject.GetComponent<csHardMonster>())
                    col.gameObject.GetComponent<csHardMonster>().Damage(bulletdamage);
                if (col.gameObject.GetComponent<csHardMonster2>())
                    col.gameObject.GetComponent<csHardMonster2>().Damage(bulletdamage);
                if (col.gameObject.GetComponent<csBossMonster>())
                    col.gameObject.GetComponent<csBossMonster>().Damage(bulletdamage);

            }

            Destroy(gameObject);
        }
    }
}
