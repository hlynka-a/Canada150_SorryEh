using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorry_Trigger : MonoBehaviour {

	public GameObject sorry_wall_text = null;
	float sorryWallTimerLimit = 0.5f;

	public GameObject sorry_person_text = null;
	float sorryPersonTimerLimit = 0.5f;

	// Use this for initialization
	void Start () {
		if (sorry_wall_text != null)
			sorry_wall_text.GetComponent<Renderer> ().enabled = false;
		if (sorry_person_text != null)
			sorry_person_text.GetComponent<Renderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		SorryWall (false);
		SorryPerson (false);
	}

	void OnTriggerEnter (Collider c)
	{
		if (c.transform.parent == this)
			return;

		if (c.tag == "Wall") {
			//Debug.Log ("sorry wall");
			SorryWall (true);
		} else if (c.tag == "CollisionCrowd") {
			//Debug.Log ("sorry person");
			SorryPerson (true);
		}
	}
		


	float sorryWallTimer = 0f;
	void SorryWall(bool saySorry){

		if (sorry_wall_text == null)
			return;
			
		sorryWallTimer -= Time.deltaTime;

		if (saySorry) {
			sorryWallTimer = sorryWallTimerLimit;
		}

		if (sorryWallTimer > 0f) {
			sorry_wall_text.GetComponent<Renderer> ().enabled = true;
		} else {
			sorry_wall_text.GetComponent<Renderer> ().enabled = false;
		}
			
	}

	float sorryPersonTimer = 0f;
	void SorryPerson(bool saySorry){
		if (sorry_person_text == null)
			return;

		sorryPersonTimer -= Time.deltaTime;

		if (saySorry) {
			sorryPersonTimer = sorryPersonTimerLimit;
		}

		if (sorryPersonTimer > 0f) {
			sorry_person_text.GetComponent<Renderer> ().enabled = true;
		} else {
			sorry_person_text.GetComponent<Renderer> ().enabled = false;
		}
	}
}
