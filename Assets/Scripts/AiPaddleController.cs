using UnityEngine;
using System.Collections;

public class AiPaddleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float puckXPos = GameObject.Find ("Puck").transform.position.x;
		float puckZPos = GameObject.Find ("Puck").transform.position.z;
		float opponentXPos = transform.position.x;
		if (puckXPos != opponentXPos) {
			if (puckXPos > 0)
				transform.position += Vector3.right * Time.deltaTime;
			else if (puckXPos < 0)
			{
				transform.position += Vector3.left * Time.deltaTime;
			}
		}


		Debug.Log ("xpos :" + opponentXPos);
		Debug.Log ("x : " + puckXPos);
		Debug.Log ("z : " + puckZPos);
	}
}
