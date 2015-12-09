using UnityEngine;
using System.Collections;

public class PlayerGlass : MonoBehaviour {

	ScoreManager scoreMan;
	// Use this for initialization
	void Start () {
		scoreMan = GameObject.Find("GameManager").GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		if(col.collider.tag == "Puck") {
			scoreMan.AddScore(false);
		}
	}
}
