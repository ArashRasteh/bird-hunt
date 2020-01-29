using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModeA : MonoBehaviour {
	public GameObject blackTarget;
	public GameObject blueTarget;
	public GameObject redTarget;
	public float timeBeforeFlyAway = 10f;

	List<TargetMovement> targetScripts = new List<TargetMovement> ();

	private int currentRound;
	private int currentScoreGroup;
	private int[] firstScoreGroupRound = { 1, 6, 11, 16, 21 }; //the first round Score Group Applies
	private int[,] scoreSheet = {
		{ 500, 800, 1000, 1000, 1000 }, //black
		{ 1000, 1500, 2000, 2000, 2000 }, //blue Or discs
		{ 1500, 2400, 3000, 3000, 3000 }, //red
		{ 10000, 10000, 15000, 20000, 30000 },}; //bonus

	//for purpose of building a demo
	private float timeBetweenBirds = 3f;
	private float lastBirdTime = -3f;


	// Use this for initialization
	void Start () { 
		currentScoreGroup = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) {
			CreateRandomBird ();
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			timeBetweenBirds = 0;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Debug.Log (targetScripts.Count);
		}

		//for purpose of building a demo
		if (Time.time - lastBirdTime > timeBetweenBirds) {
			CreateRandomBird ();
			lastBirdTime = Time.time;
			if (timeBetweenBirds > 1.5f)
				timeBetweenBirds -= .01f;
		}

		for (int i = 0; i < targetScripts.Count; i++) {
			if (targetScripts [i].IsDead || targetScripts [i].flewAway) {
				targetScripts.Remove (targetScripts [i]);
			}
		}

	}

	void CreateRandomBird()
	{
		GameObject initObject;

		int score;
		int randomTarget = Random.Range (0, 3);
		switch (randomTarget) 
		{
		case 0:
			initObject = blackTarget;
			score = scoreSheet [randomTarget, currentScoreGroup];
			break;
		case 1:
			initObject = blueTarget;
			score = scoreSheet [randomTarget, currentScoreGroup];
			break;
		case 2:
			initObject = redTarget;
			score = scoreSheet [randomTarget, currentScoreGroup]; 
			break;
		default:
			initObject = blackTarget;
			throw new UnityException ("Random Range for Switch Broken");
		}

		CreateBird (initObject, score);
	}

	void CreateBird (GameObject initObject, int score) {
		Vector3 targetInitPos = new Vector3 (Random.Range (-11, 12), 0, 25 + (Random.Range (-500, 500) /1000f));
		initObject = GameObject.Instantiate (initObject, targetInitPos, Quaternion.identity) as GameObject;
		targetScripts.Add(initObject.GetComponent<TargetMovement> ());
		targetScripts[targetScripts.Count - 1].timeBeforeFlyAway = timeBeforeFlyAway;
		targetScripts [targetScripts.Count - 1].score = score;
	}
}
