using UnityEngine;
using System.Collections;

public class AiPaddleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float puckXPos = GameObject.Find ("Puck").transform.position.x;
		float puckZPos = GameObject.Find ("Puck").transform.position.z;

		Debug.Log ("x : " + puckXPos);
		Debug.Log ("z : " + puckZPos);
	}
}
