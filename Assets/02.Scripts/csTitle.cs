using UnityEngine;
using System.Collections;

public class csTitle : MonoBehaviour {
    private Quaternion originalRotation;
    // Use this for initialization
    void Start () {
        originalRotation = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.AngleAxis(Mathf.Sin(2.0f * Time.time) * 20.0f, Vector3.up) *
        Quaternion.AngleAxis(Mathf.Sin(2.7f * Time.time) * 33.3f, Vector3.right) * originalRotation;



        if (Input.GetButtonDown("Jump"))
        {
            Application.LoadLevel("Stage1");
        }
    }
    void OnGUI()
    {
        int sw = Screen.width;
        int sh = Screen.height;
        GUI.Label(new Rect(sw/2-90, 0.8f * sh, sw, 0.4f * sh), "PRESS SPACE TO START");
    }
}
