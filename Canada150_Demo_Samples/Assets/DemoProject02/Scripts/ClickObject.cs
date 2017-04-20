using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour {

	public int goToStateOnClick = 0;

	Transform cam;

	float totalTime = 0f;

	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {

		totalTime += Time.deltaTime*2;

		this.transform.localScale 
		= new Vector3 (Vector3.Distance (this.transform.position, cam.position)*0.2f*((Mathf.Sin(totalTime)+2f)*0.5f),
			Vector3.Distance (this.transform.position, cam.position)*0.2f*((Mathf.Sin(totalTime)+2)*0.5f),
							0f);
		this.transform.LookAt ((2*this.transform.position) - cam.position);
	}

	void OnMouseDown(){
		//Debug.Log ("CLICKED");
		StateMachineManager.nextState = goToStateOnClick;
	}
}
