using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public TextMesh instructions = null;
	public TextMesh lowSorry = null;
	public TextMesh highSorry = null;

	public TextMesh enterNameInstructions = null;
	public TextMesh yourNameIs = null;
	public TextMesh enterName = null;

	public TextMesh currentSorry = null;

	int lowSorryScore = -1;
	int highSorryScore = -1;
	int lastGameScore = -1;

	string lowSorryName = "";
	string highSorryName = "";

	/*
	state = 1 << main menu, press enter to play game
	state = 2 << you beat high score, enter username
	state = 3 << you beat low score, enter username
	*/
	int state = 1;

	// Use this for initialization
	void Start () {

		int currentSorryCount = PlayerPrefs.GetInt ("currentSorryCount", -1);

		if (currentSorryCount == -1){
			currentSorry.text = "Current score : not played yet";
		} else {
			currentSorry.text = "Current score : " + currentSorryCount;
		}

		lowSorryScore = PlayerPrefs.GetInt ("lowSorryCount", -1);
		highSorryScore = PlayerPrefs.GetInt ("highSorryCount", -1);
		lowSorryName = PlayerPrefs.GetString ("lowSorryName", "");
		highSorryName = PlayerPrefs.GetString ("highSorryName", "");
		lastGameScore = currentSorryCount;

		UpdateState (1);

		if (currentSorryCount != -1) {
			if (lowSorryScore == -1) {
				// also load scene to allow user to enter username of their score
				lowSorryScore = currentSorryCount;
				highSorryScore = currentSorryCount;
				UpdateState (2);
			} else {
				if (currentSorryCount > highSorryScore) {
					//also load scene to allow user to enter username of their score
					highSorryScore = currentSorryCount;
					UpdateState (2);
				}
				if (currentSorryCount < lowSorryScore) {
					//also load scene to allow user to enter username of their score
					lowSorryScore = currentSorryCount;
					UpdateState (3);
				}
			}
		}
			
		PlayerPrefs.SetInt ("lowSorryCount", lowSorryScore);
		PlayerPrefs.SetInt ("highSorryCount", highSorryScore);

		PlayerPrefs.SetInt ("currentSorryCount", -1);


		UpdateMainMenuUI ();
	}

	void UpdateState(int s){
		state = s;
		if (s == 1) {
			instructions.gameObject.SetActive (true);
			lowSorry.gameObject.SetActive (true);
			highSorry.gameObject.SetActive (true);

			enterNameInstructions.gameObject.SetActive (false);
			yourNameIs.gameObject.SetActive (false);
			enterName.gameObject.SetActive (false);
		} else if (s == 2 || s == 3) {
			instructions.gameObject.SetActive (false);
			lowSorry.gameObject.SetActive (false);
			highSorry.gameObject.SetActive (false);

			enterNameInstructions.gameObject.SetActive (true);
			yourNameIs.gameObject.SetActive (true);
			enterName.gameObject.SetActive (true);
		} 

	}

	void UpdateMainMenuUI(){
		if (lowSorry != null) {
			if (lowSorryScore != -1) {
				lowSorry.text = "Best Score : " + lowSorryName + " : " + lowSorryScore;
			} else {
				lowSorry.text = "Best Score : " + "not played yet";
			}
		}

		if (highSorry != null) {
			if (highSorryScore != -1) {
				highSorry.text = "Worst Score : " + highSorryName + " : " + highSorryScore;
			} else {
				highSorry.text = "Worst Score : " + "not played yet";
			}
		}
	}
	
	// Update is called once per frame
	float totalScoreTime = 0f;
	void Update () {

		if (state == 1) {
			if (Input.GetKey (KeyCode.Space)) {
				PlayerPrefs.SetInt ("currentSorryCount", 0);
				SceneManager.LoadScene ("SorryEh_Level01");
			}

			if (Input.GetKey (KeyCode.Z)) {
				PlayerPrefs.SetInt ("lowSorryCount", -1);
				PlayerPrefs.SetInt ("highSorryCount", -1);
				lowSorryScore = -1;
				highSorryScore = -1;
				PlayerPrefs.SetString ("lowSorryName", "");
				PlayerPrefs.SetString ("highSorryName", "");
				highSorryName = "";
				lowSorryName = "";
				UpdateMainMenuUI ();
			}
		} else if (state == 2 || state == 3) {
			totalScoreTime += Time.deltaTime;
			if (Input.GetKey (KeyCode.Return) && totalScoreTime > 0.1f) {
				totalScoreTime = 0f;
				string newName = enterName.text;
				if (state == 2 || highSorryScore == lowSorryScore) {
					PlayerPrefs.SetString ("highSorryName", newName);
					highSorryName = newName;
				} 
				if (state == 3 || lowSorryScore == highSorryScore) {
					PlayerPrefs.SetString ("lowSorryName", newName);
					lowSorryName = newName;
				}
				UpdateState (1);
				UpdateMainMenuUI ();
			} else if (Input.GetKey (KeyCode.Backspace) && totalScoreTime > 0.1) {
				totalScoreTime = 0f;
				enterName.text = enterName.text.Substring (0, Mathf.Max(enterName.text.Length - 1,0));
			} else if (totalScoreTime > 0.1f) {
				
				foreach (char c in Input.inputString){
					enterName.text = enterName.text + c;
					totalScoreTime = 0f;
				}
			}
		}


	}
}
