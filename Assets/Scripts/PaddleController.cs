using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	float movedownX = 0.0f;
	float sensitivityX = 1f;
	float movedownY = 0.0f;
	float sensitivityY = 1f;
	// Update is called once per frame
	void Update () {

	movedownX += Input.GetAxis("Mouse X") * sensitivityX;
	if (Input.GetAxis("Mouse X") != 0f){
		transform.Translate(Vector3.right * movedownX);
	}
	movedownX = 0.0f;
	
	movedownY += Input.GetAxis("Mouse Y") * sensitivityY;
	if (Input.GetAxis("Mouse Y") != 0f){
		transform.Translate(Vector3.forward * movedownY);
	}
	movedownY = 0.0f;






		/*float inputSpeed = Input.GetAxisRaw ("HumanPaddle");
		
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.forward * Time.deltaTime;
			
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.position += Vector3.back * Time.deltaTime;
			
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * Time.deltaTime;
			
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * Time.deltaTime;
			
		}*/

	}
}
