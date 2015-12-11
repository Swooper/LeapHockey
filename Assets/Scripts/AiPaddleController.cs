using UnityEngine;
using System.Collections;

public class AiPaddleController : MonoBehaviour {
	public float speed = 40f;
	//private Rigidbody body;
	public float puckXPos;
	public float puckZPos;

	private GameObject puck;
	// Use this for initialization
	void Start () {
		puck = GameObject.Find("Puck(Clone)");
	}

	IEnumerable puckSmash()
	{
		yield return new WaitForSeconds (1f);
		while (transform.position.z != 4f) {
			transform.position += Vector3.forward * speed * Time.deltaTime;
		}

	}
	// Update is called once per frame
	void FixedUpdate () {

		puckXPos = puck.transform.position.x;
		puckZPos = puck.transform.position.z;
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
		if (puckZPos > 2.5) {
			if(puck.GetComponent<Rigidbody>().velocity == Vector3.zero)
			{StartCoroutine ("puckSmash");

			}
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
