using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {


	public TextMesh t1 = null;
	public TextMesh t2 = null;
	public TextMesh t3 = null;
	public TextMesh t4 = null;

	void Awake(){
		t1.color = new Color(t1.color.r, t1.color.g, t1.color.b,0f);
		t2.color = new Color(t2.color.r, t2.color.g, t2.color.b,0f);
		t3.color = new Color(t3.color.r, t3.color.g, t3.color.b,0f);
		t4.color = new Color(t4.color.r, t4.color.g, t4.color.b,0f);
	}

	// Use this for initialization
	void Start () {
		
	}

	float totalTime = 0f;

	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;

		if (totalTime > 0f && totalTime < 2f) {
			t1.color = new Color (t1.color.r, t1.color.g, t1.color.b, totalTime - 0f);
		} else if (totalTime > 6f) {
			t1.color = new Color (t1.color.r, t1.color.g, t1.color.b, 1f - (totalTime - 6f));
		}

		if (totalTime > 2f && totalTime < 4f) {
			t2.color = new Color (t2.color.r, t2.color.g, t2.color.b, totalTime - 2f);
		} else if (totalTime > 8f) {
			t2.color = new Color (t2.color.r, t2.color.g, t2.color.b, 1f - (totalTime - 8f));
		}

		if (totalTime > 4f && totalTime < 6f) {
			t3.color = new Color (t3.color.r, t3.color.g, t3.color.b, totalTime - 4f);
		} else if (totalTime > 10f) {
			t3.color = new Color (t3.color.r, t3.color.g, t3.color.b, 1f - (totalTime - 10f));
		}

		if (totalTime > 6f && totalTime < 8f) {
			t4.color = new Color (t4.color.r, t4.color.g, t4.color.b, totalTime - 6f);
		} else if (totalTime > 12f) {
			t4.color = new Color (t4.color.r, t4.color.g, t4.color.b, 1f - (totalTime - 12f));
		}

		if (totalTime > 13f) {
			PlayerPrefs.SetInt ("currentSorryCount", -1);
			SceneManager.LoadScene ("SorryEh_Menu");
		}
	}
}
