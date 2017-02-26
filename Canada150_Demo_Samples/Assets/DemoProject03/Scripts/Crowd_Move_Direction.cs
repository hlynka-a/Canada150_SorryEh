using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd_Move_Direction : MonoBehaviour {

	//public Vector3
	public float speed = 10f;
	float currentY = 0f;

	CharacterController cc = null;

	// Use this for initialization
	void Start () {
		cc = this.GetComponent<CharacterController> ();
		currentY = this.transform.position.y;
	}



	// Update is called once per frame
	void Update () {
	
		//this.transform.Translate(Vector3.forward*speed*Time.deltaTime);

		Vector3 moveDirection = Vector3.zero;

		moveDirection += this.transform.forward * speed * Time.deltaTime;
		moveDirection += -this.transform.up;

		if (transform.position.y != currentY) {
			moveDirection.y 
				= (currentY - transform.position.y) * 0.05f;
		}

		if (cc != null) {
			cc.Move (moveDirection);
		}
		
	}
}
