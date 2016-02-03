using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private float speed;
	private float jumpPower;
	private Rigidbody rb3d;
	private float horizontal;
	private float vertical;
	private float mouseY;
	private Transform cam;
	private Vector3 camForward;
	private Vector3 moveDirection;
	private float playerAboveLevel = 0.5f;

	void Awake ()
	{
		cam = Camera.main.transform;
	}
		
	void Start () 
	{
		rb3d = GetComponent<Rigidbody> ();	

		//Set startup position
		rb3d.MovePosition(new Vector3(10.0f, playerAboveLevel, 0.0f));
	}

	void LateUpdate () 
	{
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");

		//Get main camera's position and apply to this game object
		camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
		moveDirection = (vertical * camForward + horizontal * cam.right).normalized;

		rb3d.AddForce(moveDirection * moveFaster());
	}
		
	void Update ()
	{
		playerJump ();
	}

	private float moveFaster ()
	{
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 10;
		} else {
			speed = 2;
		}	
		return speed;
	}

	private void playerJump ()
	{
		if (Input.GetKey (KeyCode.Space)) {
			jumpPower = 0.5f;
			rb3d.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

		}
	}
}
