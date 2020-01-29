using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {


	public GameObject gunPrefab;
	public GameObject BlackScreen;
	public GameObject MuzzleFlash;
	public GameObject CrossHair;
	public float zoomSpeed = 5f;
	public float zoomedOut = 40f;
	public float zoomedIn = 20f;
	public float muzzleFlashDuration = .1f;
	public bool IsZoomed {
		get {return isZoomed;}
	}

	private Camera mainCamera;
	private float timeOfLastShot;
	private bool muzzleFlashOn;
	private bool isZoomed = false;
	private bool changeZoom = false;
	private bool zoomIn = false;
	private Vector3 gunZoomedPosition;
	private Quaternion gunZoomedRotation;
	private Vector3 gunZoomedOutPosition;
	private Quaternion gunZoomedOutRotation;

	// Use this for initialization
	void Start () {
		mainCamera = gameObject.GetComponent<Camera> ();
		gunZoomedPosition = new Vector3 (0.0014f, -0.1027f, 0.332f);
		gunZoomedRotation = Quaternion.Euler(0, 0, 0);
		gunZoomedOutPosition = gunPrefab.transform.localPosition;
		gunZoomedOutRotation = gunPrefab.transform.localRotation;
		BlackScreen.SetActive (false);
		muzzleFlashOn = false;
	
	}

	// Update is called once per frame
	void Update () {
		//Debug.DrawRay (transform.position, transform.forward*100);

		if(Input.GetKeyDown(KeyCode.Mouse0)) //left click shoots
		{
			timeOfLastShot = Time.time;
			muzzleFlashOn = true;

			TargetMovement targetScript;
			RaycastHit hit;
			Ray shootingRay = new Ray (transform.position, transform.forward);
			if (Physics.Raycast(shootingRay, out hit, 100)) {
				if (hit.collider.tag == "target") {
					//Debug.Log ("Target hit");
					targetScript = hit.collider.GetComponent("TargetMovement") as TargetMovement;
					targetScript.IsDead = true;
					hit.collider.enabled = false;
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.Mouse1)) //right click controls zoom
		{
			if (isZoomed == false || zoomIn == false) {
				changeZoom = true;
				zoomIn = true;
			} else if (isZoomed == true || zoomIn == true) {
				changeZoom = true;
				zoomIn = false;
			}

		}

		if (changeZoom) {
			ChangeZoom ();
		}
		if (muzzleFlashOn) { //while muzzleFlashOn is on
			BlackScreen.SetActive (true);
			if ((Time.time - timeOfLastShot) > muzzleFlashDuration) {
				muzzleFlashOn = false;
				BlackScreen.SetActive (false);
			}
		}
	}



	//Zoom Controls
	void ChangeZoom ()
	{
		if (zoomIn == true) {
			//zoomIn
			isZoomed = true;
			CrossHair.SetActive (false);
			mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomedIn, Time.smoothDeltaTime * zoomSpeed);
			gunPrefab.transform.localRotation = Quaternion.Slerp (gunPrefab.transform.localRotation, gunZoomedRotation,
				Time.smoothDeltaTime * zoomSpeed * 3);
			gunPrefab.transform.localPosition = Vector3.Slerp (gunPrefab.transform.localPosition, gunZoomedPosition,
				Time.smoothDeltaTime * zoomSpeed * 3);
			if (Mathf.Round (mainCamera.fieldOfView) == zoomedIn) {
				changeZoom = false;
			}

		} else {
			//zoomOut
			isZoomed = false;
			CrossHair.SetActive (true);
			mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomedOut, Time.smoothDeltaTime * zoomSpeed);
			gunPrefab.transform.localRotation = Quaternion.Slerp (gunPrefab.transform.localRotation, gunZoomedOutRotation,
				Time.smoothDeltaTime * zoomSpeed * 3);
			gunPrefab.transform.localPosition = Vector3.Slerp (gunPrefab.transform.localPosition, gunZoomedOutPosition,
				Time.smoothDeltaTime * zoomSpeed * 3);
			if (Mathf.Round (mainCamera.fieldOfView) == zoomedOut) {
				changeZoom = false;
			}
		}
	}


}
