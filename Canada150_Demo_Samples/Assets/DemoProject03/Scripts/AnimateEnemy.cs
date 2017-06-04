using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemy : MonoBehaviour {

	public List<Material> animateTexture = new List<Material>();

	int whichEnemy = 0;
	int aniIndex = 0;

	Renderer ren = null;

	// Use this for initialization
	void Start () {
		whichEnemy = Random.Range (0, 4);
		ren = this.GetComponent<Renderer> ();

		aniIndex = whichEnemy * 4;
	}

	float totalTime = 0f;

	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;

		if (totalTime >= 0.08f) {
			totalTime = 0f;

			aniIndex++;
			if (aniIndex % 4 == 0 && aniIndex > 0) {
				aniIndex = whichEnemy * 4;
			}

			if (aniIndex >= animateTexture.Count) {
				Debug.Log ("error here = " + aniIndex + " " + animateTexture.Count + " " + this.name);
				return;
			}

			ren.sharedMaterial = animateTexture [aniIndex];
		}
	}
}
