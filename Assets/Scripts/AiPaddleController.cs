using UnityEngine;
using System.Collections;

public class AiPaddleController : MonoBehaviour {
	public float speed = 15f;
	//private Rigidbody body;
	public float puckXPos;
	public float puckZPos;
	// Use this for initialization
	void Start () {
		 puckXPos = GameObject.Find ("Puck").transform.position.x;
		 puckZPos = GameObject.Find ("Puck").transform.position.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		float opponentXPos = transform.position.x;
		float opponentZPos = transform.position.z;

		if (puckXPos != opponentXPos) {
			if (opponentXPos > puckXPos)
				transform.position += Vector3.left * Time.deltaTime;
			else if (opponentXPos < puckXPos)
			{
				transform.position += Vector3.right * Time.deltaTime;
			}
		}
		if (puckZPos > 0) {
			//Vector3 movement = new Vector3(0.0f, 0.0f, -1f);

			//body.velocity = movement * speed;
			transform.position += Vector3.back * speed * Time.deltaTime;
			//transform.position += Vector3.back * Time.deltaTime;
		}

//		Debug.Log ("xpos :" + opponentXPos);
//		Debug.Log ("x : " + puckXPos);
//		Debug.Log ("z : " + puckZPos);
	}
}
