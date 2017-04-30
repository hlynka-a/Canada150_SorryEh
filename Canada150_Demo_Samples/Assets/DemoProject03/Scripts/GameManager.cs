using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public TextMesh startText = null;
	public Player_Move_CC_02 playerMove = null;

	float totalTime = 0;
	public static bool isStarting;
	public static bool isEnding;
	public static int finalSorryCount = 0;
	public static int startSorryCount = 0;

	public string nextLevelLoad = "";
	public string escLoad = "";

	void Awake(){
		startSorryCount = PlayerPrefs.GetInt ("currentSorryCount", 0);
	}

	// Use this for initialization
	void Start () {
		playerMove.enableMove = false;
		isStarting = true;
		isEnding = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (isStarting) {
			totalTime += Time.deltaTime;
			if (totalTime >= 6) {
				startText.text = "";
				playerMove.enableMove = true;
				isStarting = false;
				totalTime = 0f;
			} else if (totalTime >= 4) {
				startText.text = "1";
			} else if (totalTime >= 2) {
				startText.text = "2";
			} else if (totalTime >= 0) {
				startText.text = "3";
			}
		}

		if (isEnding) {
			totalTime += Time.deltaTime;
			if (totalTime >= 3) {
				startText.text = "";
				// LOAD NEXT LEVEL
				PlayerPrefs.SetInt ("currentSorryCount", finalSorryCount);
				SceneManager.LoadScene (nextLevelLoad);
			} else {
				startText.text = "END LEVEL";
				playerMove.enableMove = false;
			}
		}

		if (Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.Q)) {
			PlayerPrefs.SetInt ("currentSorryCount", -1);
			SceneManager.LoadScene (escLoad);
		}
	}
}
