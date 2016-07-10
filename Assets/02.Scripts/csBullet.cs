using UnityEngine;
using System.Collections;

public class csBullet : MonoBehaviour {
    float power = 3000.0f;
    Vector3 velocity = new Vector3(0.0f, 0.3f, 0.5f);
    // Use this for initialization
    void Start () {
        velocity = this.transform.forward + this.transform.up / 4;
        velocity = velocity * power;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
