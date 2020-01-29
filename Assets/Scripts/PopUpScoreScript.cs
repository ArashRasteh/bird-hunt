using UnityEngine;
using System.Collections;

public class PopUpScoreScript : MonoBehaviour {
	public float maxTimeAlive = 1f;

	private float scaleModifier = 1f;
	private float timeBorn;
	private float timeAlive;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, maxTimeAlive);
		timeBorn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive = Time.time - timeBorn;

		if (timeAlive > 1f && timeAlive < 1.5f) {
			transform.Translate (0, 0, -Time.smoothDeltaTime * 12);
			scaleModifier += Time.smoothDeltaTime;
			transform.localScale = new Vector3 (scaleModifier, scaleModifier, scaleModifier);
		} else if (timeAlive >= 1.5f) {
			scaleModifier *= 0.9f;
			transform.localScale = new Vector3 (scaleModifier, scaleModifier, scaleModifier);
		}


	}
}
