using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool playerIdle = false;

	private float jumpPower = 0.50f;
	private float playerAboveLevel = 0.5f;
	private float speed;
	private Rigidbody rb3d;
	private float horizontal;
	private float vertical;
	private float mouseY;
	private Transform cam;
	private Vector3 camForward;
	private Vector3 moveDirection;

	void Awake ()
	{
		cam = Camera.main.transform;
	}
		
	void Start () 
	{
		rb3d = GetComponent<Rigidbody> ();	

		//Set startup position
		rb3d.MovePosition(new Vector3(0.0f, playerAboveLevel, 0.0f));
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
		checkPlayerIdle ();
		playerJump ();

	}

	private float moveFaster ()
	{
		if (Input.GetKey (KeyCode.LeftShift)) 
			speed = 10;
		else
			speed = 2;
		return speed;
	}


	//Used so rounded objects like sphere's and helps stop them from rolling when no buttons pressed (hackish)
	private void checkPlayerIdle()
	{
		if (!Input.GetKey (KeyCode.W) || !Input.GetKey (KeyCode.A)
		    || !Input.GetKey (KeyCode.S) || !Input.GetKey (KeyCode.D)
		    || !Input.GetKey (KeyCode.Space))
			rb3d.Sleep();
	}

	private void playerJump ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
			rb3d.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
		
	}
}
