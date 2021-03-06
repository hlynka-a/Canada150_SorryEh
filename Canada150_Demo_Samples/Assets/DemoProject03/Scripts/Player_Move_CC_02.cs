using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_CC_02 : MonoBehaviour {

	private CharacterController cc = null;
	private const float gravityConst = -0.5f;
	private float gravity = gravityConst;

	public bool enableMove = true;
	public bool enableMoveX = true;
	public bool enableMoveY = true;
	public bool enableMoveZ = true;

	public float speed = 1.0f;
	public float jumpHeight = 10f;

	public KeyCode leftKey = KeyCode.LeftArrow;
	public KeyCode rightKey = KeyCode.RightArrow;
	public KeyCode forwardKey = KeyCode.UpArrow;
	public KeyCode backwardKey = KeyCode.DownArrow;
	public KeyCode jumpKey = KeyCode.Space;

	public Transform relativeToMovement = null;

	public AnimateCharacter ani = null;

	// Use this for initialization
	void Start () {
		cc = this.GetComponent<CharacterController> ();

		ani = this.GetComponent<AnimateCharacter> ();

		Debug.Log ("starting player-move script...");
	}
	
	// Update is called once per frame
	void Update () {
		if (cc != null && enableMove == true) {

			MoveCharacterController (GetInput ());

		}
	}

	Vector3 GetInput(){
		Vector3 returnInput = Vector3.zero;

		if (enableMoveX)
			returnInput += GetInputX ();
		if (enableMoveZ)
			returnInput += GetInputZ ();

		// update movement, consistent speed in any direction, and relative to camera rotation
		returnInput.Normalize ();
		returnInput = returnInput * speed;
		if (relativeToMovement != null) {
			returnInput = relativeToMovement.TransformDirection (returnInput);
		}

		if (enableMoveY)
			returnInput += GetInputY ();


		return returnInput;
	}

	Vector3 GetInputX(){
		Vector3 returnInput = Vector3.zero;

		if (Input.GetKey(leftKey))
			returnInput += new Vector3 (1f * speed * Time.deltaTime, 0, 0);
		if (Input.GetKey(rightKey))
			returnInput += new Vector3 (- 1f * speed * Time.deltaTime, 0, 0);

		return returnInput;
	}

	Vector3 GetInputY(){
		Vector3 returnInput = Vector3.zero;

		if (Input.GetKey(jumpKey) && cc.isGrounded) {
			gravity = jumpHeight;
		} else {
			gravity = Mathf.Max (gravityConst, gravity + gravityConst * Time.deltaTime);
		}

		returnInput += new Vector3 (0, gravity, 0);

		return returnInput;
	}

	Vector3 GetInputZ(){
		Vector3 returnInput = Vector3.zero;

		if (Input.GetKey(forwardKey))
			returnInput += new Vector3 (0, 0, - 1f * speed * Time.deltaTime);
		if (Input.GetKey(backwardKey))
			returnInput += new Vector3 (0, 0, 1f * speed * Time.deltaTime);

		return returnInput;
	}

	void MoveCharacterController(Vector3 moveDirection){
		//Debug.Log ("moving in this direction : " + moveDirection);
		cc.Move (moveDirection);

		if (ani != null) {
			//Debug.Log (moveDirection.x + " " + moveDirection.y + " " + moveDirection.z);
			if (moveDirection.x != 0 || moveDirection.z != 0) {
				ani.SetAnimationState (1);
			} else {
				ani.SetAnimationState (0);
			}
		}
	}


}