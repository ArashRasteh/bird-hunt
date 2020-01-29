using UnityEngine;
using System.Collections;

public class FPCamera : MonoBehaviour {

	public GameObject rotationEmpty;
	public Vector2 startingLookDirection;
	public float zoomedRotationDampener = 5F;
	public float rotationSpeed = 20F;

	private CursorLockMode wantedMode;
	private float targetXRot;
	private float targetYRot;
	private Quaternion target;
	private PlayerShooting shootingScript;


	// Use this for initialization
	void Start () {
		//initailize variables
		wantedMode = CursorLockMode.Locked;
		targetXRot = startingLookDirection.x;
		targetYRot = startingLookDirection.y;
		shootingScript = GetComponent<PlayerShooting> ();
	}



	// Update is called once per frame
	void Update () {


		if(Cursor.lockState == CursorLockMode.Locked) 
		{
			if (!shootingScript.IsZoomed) {
				targetXRot -= Input.GetAxis ("Mouse Y");
				targetYRot += Input.GetAxis ("Mouse X");
			} else {

				targetXRot -= Input.GetAxis ("Mouse Y")/zoomedRotationDampener;
				targetYRot += Input.GetAxis ("Mouse X")/zoomedRotationDampener;
			}

			//create rotation bounding box
			if (targetYRot < -50)
				targetYRot = -50;
			else if (targetYRot > 50)
				targetYRot = 50;
			
			if (targetXRot < -50)
				targetXRot = -50;
			else if (targetXRot > 50)
				targetXRot = 50;

			target = Quaternion.Euler (targetXRot, targetYRot, 0);
			rotationEmpty.transform.rotation = Quaternion.Slerp(rotationEmpty.transform.rotation, target, Time.smoothDeltaTime * rotationSpeed);
			//rotationEmpty.transform.rotation = target;			
		}
	}


	// Apply requested cursor state
	void SetCursorState ()
	{
		Cursor.lockState = wantedMode;
		// Hide cursor when locking
		Cursor.visible = (CursorLockMode.Locked != wantedMode);
	}

	void OnGUI ()
	{
		GUILayout.BeginVertical ();
		// Release cursor on escape keypress
		if (Input.GetKey (KeyCode.Escape))
			Cursor.lockState = wantedMode = CursorLockMode.None;

		switch (Cursor.lockState)
		{
		case CursorLockMode.None:
			GUILayout.Label ("Cursor is normal");
			if (GUILayout.Button ("Lock cursor"))
				wantedMode = CursorLockMode.Locked;
			break;
		case CursorLockMode.Locked:
			GUILayout.Label ("Cursor is locked");
			if (GUILayout.Button ("Unlock cursor"))
				wantedMode = CursorLockMode.None;
			break;
		}

		GUILayout.EndVertical ();

		SetCursorState ();
	}







}



