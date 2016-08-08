using UnityEngine;
using System.Collections;

public class BossManager : MonoBehaviour {
    static BossManager _instance = null;
    public Transform AttackFiledpostion1;
    public Transform AttackFiledpostion2;
    public Transform AttackFiledpostion3;

    public GameObject Battackfield1;
    public GameObject Battackfield2;
    public GameObject Battackfield3;
    public GameObject attackRange;

    public static BossManager Instance()
    {
        return _instance;
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        
    }
    // Use this for initialization
    void Start () {
        Instantiate(attackRange, AttackFiledpostion1.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //패턴2
    void MakeRange1()
    {

        attackRange.SetActive(true);
        StartCoroutine(HideRange1());

    }
    IEnumerator HideRange1()
    {
        yield return new WaitForSeconds(1.5f);
        attackRange.SetActive(false);
    }
    //패턴3
    void MakeRange2()
    {

        attackRange.SetActive(true);
        StartCoroutine(HideRange2());

    }
    IEnumerator HideRange2()
    {
        yield return new WaitForSeconds(1.5f);
        attackRange.SetActive(false);
    }

    void CreateBoxCollider1()
    {
        Instantiate(Battackfield1, AttackFiledpostion1.position, Quaternion.identity);
    }
    void CreateBoxCollider2()
    {
        Instantiate(Battackfield2, AttackFiledpostion2.position, Quaternion.identity);
    }
    void CreateBoxCollider3()
    {
        Instantiate(Battackfield3, AttackFiledpostion3.position, Quaternion.identity);
    }
   

}
