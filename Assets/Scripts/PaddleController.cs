using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	var movedownY = 0.0;
	var sensitivityY = 1;
	// Update is called once per frame
	void Update () {

	movedownY += Input.GetAxis("Mouse Y") * sensitivityY;
	if (Input.GetAxis("Mouse Y") != 0){
		transform.Translate(Vector3.forward * movedownY);
	}
	movedownY = 0.0;
	







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
