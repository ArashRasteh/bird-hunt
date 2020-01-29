using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public static int _GlobalScore;

	public GameObject scoreTextUI;



	// Use this for initialization
	void Start () {
		_GlobalScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScore ();
	}

	void UpdateScore () {
		if (_GlobalScore > 999999)
			_GlobalScore = 0;

		Text scoreTextScript = scoreTextUI.GetComponent<Text> ();
		string scoreString = "";
		for (int i = 0; i < 6-_GlobalScore.ToString ().Length; i++) {
			scoreString += "0";
		}
		scoreString += _GlobalScore.ToString ();
		scoreTextScript.text = scoreString + "\nSCORE";

	}
}
