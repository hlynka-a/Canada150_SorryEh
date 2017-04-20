using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_FirstPerson : MonoBehaviour {

	Transform cam = null;

	float lastMouseX = 0f;
	float lastMouseY = 0f;

	public float speed = 1f;

	public bool allowUserMovement = true;

	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
		lastMouseX = Input.mousePosition.x;
		lastMouseY = Input.mousePosition.y;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (cam != null && allowUserMovement) {
			MoveCamera(MouseInput ());
		}
	}

	Vector2 MouseInput(){
		Vector2 returnInput = Vector2.zero;

		returnInput += new Vector2(MouseInputX(),MouseInputY());

		return returnInput;
	}

	float MouseInputX(){
		float returnInput = 0f;

		//returnInput = Input.mousePosition.x - lastMouseX;
		//lastMouseX = Input.mousePosition.x;

		returnInput = Input.GetAxis ("Mouse X");

		return returnInput;
	}

	float MouseInputY(){
		float returnInput = 0f;

		//returnInput = Input.mousePosition.y - lastMouseY;
		//lastMouseY = Input.mousePosition.y;

		returnInput = Input.GetAxis ("Mouse Y");

		return returnInput;
	}
		
	void MoveCamera(Vector2 input){

		input = input * Time.deltaTime * speed;

		//restrict vertical top/bottom angle to avoid strange behavior
		cam.eulerAngles = new Vector3 
			(returnClamp(cam.eulerAngles.x - input.y, -80f, 80f), 
				cam.eulerAngles.y + input.x, 
				0);

	}

	float returnClamp(float value, float min, float max){
		float returnValue = value;

		if (returnValue >= 180)
			returnValue -= 360;

		if (returnValue >= max)
			returnValue = max;
		else if (returnValue <= min)
			returnValue = min;

		return returnValue;
	}


}
