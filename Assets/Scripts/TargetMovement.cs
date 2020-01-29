using UnityEngine;
using System.Collections;

public class TargetMovement : MonoBehaviour {
	public float targetSpeed = 5f;
	public Vector3 direction;
	public float deadFloatTime = 0.166f;
	public float timeBeforeFlyAway = 5f;
	public int score;
	public bool flewAway;
	public GameObject popUpScore;

	public bool IsDead
	{
		get {
			return isDead;
		}
		set {
			isDead = value;
			UIManager._GlobalScore += score;
			timeOfDeath = Time.time;
			Vector3 tempScorePosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1f);
			GameObject tempScore = Instantiate (popUpScore, tempScorePosition, Quaternion.identity) as GameObject;
			TextMesh[] tempScoreTexts = tempScore.GetComponentsInChildren<TextMesh> ();

			foreach (TextMesh scoreText in tempScoreTexts)
			{
				scoreText.text = score.ToString ();
			}
		}
	}
	public float TimeOfDeath{get { return timeOfDeath; }} 

	protected bool isDead;
	protected bool justReleased;
	protected float timeOfDeath;
	protected float timeOfBirth;


	// Use this for initialization
	void Awake () {
		direction.x = (Random.Range (-1000, 1001)) / 1000f;
		direction.y = (Random.Range (200, 1001)) / 1000f;
		direction.z = 0;
		direction.Normalize();
		justReleased = true;
		isDead = false;
		flewAway = false;
		timeOfBirth = Time.time;
		timeBeforeFlyAway = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		float curXPos = transform.position.x;
		float curYPos = transform.position.y;
		float timeBeenAlive = Time.time - timeOfBirth;

		if (!isDead) {
			if (timeBeenAlive > timeBeforeFlyAway) {
				if (direction.y < 0.2) {
					direction.y = (Random.Range (200, 1001)) / 1000f;
				}
				if (direction.z == 0) {
					direction.z = (Random.Range (0, 500)) / 1000f;
					direction.Normalize ();
				}
				if (timeBeenAlive > (timeBeforeFlyAway + 14f)) {
					flewAway = true;
				}
				if (timeBeenAlive > (timeBeforeFlyAway + 15f)) {
					Destroy (gameObject);
				}
			} else if (justReleased) {
				if (curYPos >= 2)
					justReleased = false;
			} else if (curXPos < -15) {
				direction.x *= -1f;
				transform.Translate (.1f, 0, 0);
			} else if (curXPos > 15) {
				direction.x *= -1f;
				transform.Translate (-.1f, 0, 0);
			} else if (curYPos < 2) {
				direction.y *= -1f;
				transform.Translate (0, 0.1f, 0);
			} else if (curYPos > 25) {
				direction.y *= -1f;
				transform.Translate (0, -0.1f, 0);
			} 

			transform.Translate (direction.x * targetSpeed * Time.smoothDeltaTime,
				direction.y * targetSpeed * Time.smoothDeltaTime, direction.z * targetSpeed * Time.smoothDeltaTime);
		}
		else {
			if ((Time.time - timeOfDeath) > deadFloatTime) {
				transform.Translate (0,
					-1 * targetSpeed * Time.smoothDeltaTime, 0);
				//Debug.Log (curYPos);
				if (curYPos < 0) {
					Destroy (gameObject);
				}
			}
		}
	}
}
