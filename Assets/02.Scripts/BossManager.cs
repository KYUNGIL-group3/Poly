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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void CreateBoxCollider1()
    {
        Instantiate(Battackfield1, AttackFiledpostion1.position, Quaternion.identity);
    }
    void CreateBoxCollider2()
    {
        Instantiate(Battackfield2, AttackFiledpostion1.position, Quaternion.identity);
    }
    void CreateBoxCollider3()
    {
        Instantiate(Battackfield3, AttackFiledpostion1.position, Quaternion.identity);
    }
        
}
