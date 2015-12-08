using UnityEngine;
using System.Collections;

public class OpponentGlass : MonoBehaviour {

	ScoreManager scoreMan;
	// Use this for initialization
	void Start () {
		scoreMan = GameObject.Find("GameManager").GetComponent<ScoreManager>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollision(Collider col) {
		if(col.tag == "Puck") {
			scoreMan.AddScore(true);
		}

	}
}
