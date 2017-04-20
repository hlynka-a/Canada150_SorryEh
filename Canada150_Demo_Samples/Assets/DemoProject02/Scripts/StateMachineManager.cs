using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour {

	public Transform [] stateCamera = new Transform[10];

	int currentState = 0;
	public static int nextState = 0;
	Transform nextCamera = null;
	Transform mainCamera = null;

	float totalTime = 0f;
	float timeToMove = 2f;
	Vector3 oldCamPosition = Vector3.zero;
	Quaternion oldCamRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
		currentState = 0;
		nextState = 0;
		mainCamera = Camera.main.transform;

		if (mainCamera.position != stateCamera [currentState].position
		    || mainCamera.rotation != stateCamera [currentState].rotation) {
			mainCamera.position = stateCamera [currentState].position;
			mainCamera.rotation = stateCamera [currentState].rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (currentState != nextState) {
			//move camera until in correct position
			//do not enable state objects or update current state until camera is positioned correctly
			DisableAllStates();

			if ((mainCamera.position != stateCamera [currentState].position
				|| mainCamera.rotation != stateCamera [currentState].rotation) 
				&& totalTime < timeToMove) {
				MoveCamera ();
			} else {
				currentState = nextState;
				oldCamPosition = Vector3.zero;
				oldCamRotation = Quaternion.identity;
				stateCamera [currentState].parent.gameObject.SetActive (true);
				totalTime = 0f;
			}
		}
	}

	void DisableAllStates(){
		foreach (Transform sC in stateCamera) {
			if (sC != null && sC.parent.gameObject.activeSelf == true) {
				sC.parent.gameObject.SetActive (false);
			}
		}
	}

	void MoveCamera(){
		totalTime += Time.deltaTime;

		if (totalTime >= timeToMove)
			totalTime = timeToMove;

		if (oldCamPosition == Vector3.zero)
			oldCamPosition = mainCamera.position;
		if (oldCamRotation == Quaternion.identity)
			oldCamRotation = mainCamera.rotation;

		mainCamera.position = Vector3.Lerp (oldCamPosition, stateCamera [nextState].position, totalTime / timeToMove);
		mainCamera.rotation = Quaternion.Lerp(oldCamRotation, stateCamera [nextState].rotation, totalTime / timeToMove);
	}
}
