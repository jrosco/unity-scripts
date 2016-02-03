using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private const float Y_ANGLE_MIN = 10.0f;
	private const float Y_ANGLE_MAX = 35.0f;

	private const float Y_ZOOM_MIN = 3.0f;
	private const float Y_ZOOM_MAX = 10.0f;

	//private const float X_ANGLE_MIN = -360.0f;
	//private const float X_ANGLE_MAX = 360.0f;

	public Transform cameraTransform;
	public GameObject targetPlayer;
	public bool enableZoomCamera = false;
	public float cameraDistance = 5.0f;
	public float sensitivity = 5.0f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;

	RaycastHit hit;

	// Use this for initialization
	void Start () 
	{
		//Cursor.visible = false;
	}

	void Update ()
	{
		ZoomCamera ();

		//TODO Add camera controllers when camera goes through objects .e.g walls 
		Ray cameraRayCast = new Ray (transform.position, Vector3.up * 10.0f);
		if (Physics.Raycast (cameraRayCast, out hit))
			Debug.Log ("RayCast Hit " + hit.distance);

		//TODO Add acmera controller when object in front of camera e.g. walls 
		Vector3 infront = transform.TransformDirection (transform.forward);
		if (Physics.Raycast (transform.position, infront, 1.0f)) 
			Debug.Log ("There is something in front of the object!");
	}	

	void LateUpdate () 
	{
		currentX += Input.GetAxis ("Mouse X") * sensitivity;
		currentY += Input.GetAxis ("Mouse Y") * sensitivity;

		currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
		//currentX = Mathf.Clamp (currentX, X_ANGLE_MIN, X_ANGLE_MAX);

		// Camera smoothing .. attempt
		//transform.position = Vector3.Lerp(targetPlayer.transform.position, transform.position,Time.deltaTime);

		Vector3 direction = new Vector3 (0, 0, - cameraDistance);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);

		cameraTransform.position = targetPlayer.transform.position + (rotation * direction);
		cameraTransform.LookAt (targetPlayer.transform.position);

	}

	private void ZoomCamera()
	{
		float mouseScroll = Input.GetAxis ("Mouse ScrollWheel");

		if (mouseScroll > 0 && enableZoomCamera == true)
			cameraDistance--; 
		else if(mouseScroll < 0 && enableZoomCamera == true)
			cameraDistance++;

		cameraDistance = Mathf.Clamp (cameraDistance, Y_ZOOM_MIN, Y_ZOOM_MAX);
	}
}
