using UnityEngine;
using System.Collections;

public class csParticleDeleyStart : MonoBehaviour {

	ParticleSystem ps;
	public float deleytime = 3.5f;
	float statetime = 0.0f;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
		Destroy (gameObject, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		statetime += Time.unscaledDeltaTime;
		if (statetime > deleytime) {
			ps.Simulate (Time.unscaledDeltaTime, true, false);
		}
	}
}
