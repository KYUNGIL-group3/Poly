using UnityEngine;
using System.Collections;

public class csParticleStart : MonoBehaviour {

	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
		Destroy (gameObject, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		ps.Simulate (Time.unscaledDeltaTime, true, false);
	}
}
