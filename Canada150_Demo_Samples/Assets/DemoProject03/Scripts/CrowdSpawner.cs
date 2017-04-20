using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour {

	public List<GameObject> spawnPrefabs = new List<GameObject>();
	List<GameObject> crowd = new List<GameObject>();

	public float spawnTime = 0.5f;

	// Use this for initialization
	void Start () {
		foreach (Transform t in this.transform) {
			//spawnPrefabs
			spawnPrefabs.Add(t.gameObject);
		}
	}

	float totalTime = 0f;

	// Update is called once per frame
	void Update () {

		totalTime += Time.fixedDeltaTime;

		if (totalTime > spawnTime) {
			totalTime = 0f;
			Debug.Log ("spawing?");
			int index = Random.Range (0, spawnPrefabs.Count - 1);
			GameObject newPerson 
				= (GameObject)Instantiate
				(spawnPrefabs [index], spawnPrefabs[index].transform.position, spawnPrefabs[index].transform.rotation);
			newPerson.SetActive (true);
			crowd.Add (newPerson);
			Debug.Log ("crowd size = " + crowd.Count);
		}

	}
}
