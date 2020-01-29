using UnityEngine;
using System.Collections;

public class CubeMoveTest : MonoBehaviour {
	


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(0, 2f*Time.deltaTime, 0);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(0, -2f*Time.deltaTime, 0);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(-2f*Time.deltaTime, 0, 0);
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(2f*Time.deltaTime, 0, 0);
		}

		float xColorPos = (transform.position.x + 7.2f)/15.6f;

		float yColorPos = (transform.position.y - 2.9f)/10f;

		GetComponent<Renderer> ().material.color = new Vector4(xColorPos, yColorPos, 0, 1);

	}
}
