using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorry_Trigger : MonoBehaviour {

	public GameObject sorry_wall_text = null;
	float sorryWallTimerLimit = 0.5f;

	public GameObject sorry_person_text = null;
	float sorryPersonTimerLimit = 0.5f;

	public Transform alignTextWith = null;

	public TextMesh sorryCountText = null;
	int sorryCount = 0;

	// Use this for initialization
	void Start () {
		if (sorry_wall_text != null)
			sorry_wall_text.GetComponent<Renderer> ().enabled = false;
		if (sorry_person_text != null)
			sorry_person_text.GetComponent<Renderer> ().enabled = false;

		sorryCount = GameManager.startSorryCount;

		if (sorryCountText != null)
			sorryCountText.text = "Sorry Count : " + sorryCount;
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
		} else if (c.tag == "EndLevel") {
			EndGame (true);
		}
	}
		


	float sorryWallTimer = 0f;
	void SorryWall(bool saySorry){

		if (sorry_wall_text == null)
			return;
			
		sorryWallTimer -= Time.deltaTime;

		if (saySorry) {
			sorryWallTimer = sorryWallTimerLimit;
			sorryCount++;
			if (sorryCountText != null)
				sorryCountText.text = "Sorry Count : " + sorryCount;
		}

		if (sorryWallTimer > 0f) {
			//sorry_wall_text.transform.LookAt (Camera.main.transform.position);
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
			sorryCount++;
			if (sorryCountText != null)
				sorryCountText.text = "Sorry Count : " + sorryCount;
		}

		if (sorryPersonTimer > 0f) {
			if (alignTextWith != null) {
				//sorry_person_text.transform.LookAt (alignTextWith);
			}
			sorry_person_text.GetComponent<Renderer> ().enabled = true;
		} else {
			sorry_person_text.GetComponent<Renderer> ().enabled = false;
		}
	}

	void EndGame (bool entered){
		//TODO: ...

		Debug.Log ("You won!");

		GameManager.isEnding = true;
		GameManager.finalSorryCount = sorryCount;
	}
}
