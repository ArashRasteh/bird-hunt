using UnityEngine;
using System.Collections;

public class TargetAnimation : MonoBehaviour {
	public Transform spriteTransform;

	private Animator anim;
	private TargetMovement movementScript;
	private Vector3 lastDirection;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		movementScript = GetComponent<TargetMovement> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (movementScript.IsDead) {
			if ((Time.time - movementScript.TimeOfDeath) < movementScript.deadFloatTime)
				anim.SetBool ("justShot", true);
			else
				anim.SetBool ("fallingDown", true);
		} else {
			if (movementScript.direction.y >= 0.707106f) {
				anim.SetBool ("isFlyingUp", true);
			} else if (movementScript.direction.y < 0.707106f) {
				anim.SetBool ("isFlyingUp", false);
			}

			if (lastDirection.x == 0 || lastDirection.x != movementScript.direction.x) {
				if (movementScript.direction.x >= 0) {
					spriteTransform.localScale = new Vector3 (3.5f, 3.5f);
					lastDirection = movementScript.direction;
				} else if (movementScript.direction.x < 0) {
					spriteTransform.localScale = new Vector3 (-3.5f, 3.5f);
					lastDirection = movementScript.direction;
				}
			}
		}




	}
}
