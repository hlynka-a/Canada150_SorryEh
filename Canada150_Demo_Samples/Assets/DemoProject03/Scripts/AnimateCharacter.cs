using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCharacter : MonoBehaviour {

	public List<Material> animateTexture = new List<Material> ();
	public GameObject animateObject = null;
	int aniIndex = 0;
	int aniState = 0;
	Renderer ren = null;

	// Use this for initialization
	void Start () {
		ren = animateObject.GetComponent<Renderer> ();
	}

	float totalTime = 0f;

	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;

		if (totalTime > 0.04f) {
			totalTime = 0f;

			if (aniState == 0) {
				aniIndex = 0;
			} else if (aniState == 1) {
				aniIndex++;
				if (aniIndex >= animateTexture.Count) {
					aniIndex = 0;
				}
			}
			ren.sharedMaterial = animateTexture [aniIndex];

		}
	}

	public void SetAnimationState(int newState){
		aniState = newState;
	}
}
