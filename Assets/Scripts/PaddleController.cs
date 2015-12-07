using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	float moveZ = 0.0f;
	float moveX = 0.0f;
	float sensitivityZ = 4f;
	float sensitivityX = 4f;
	// Update is called once per frame
	void FixedUpdate () {

		moveZ += Input.GetAxis("Mouse Y") * sensitivityZ;
		if (Input.GetAxis("Mouse Y") != 0f){
			transform.Translate(Vector3.forward * moveZ);
		}
		moveZ = 0.0f;
		
		moveX += Input.GetAxis("Mouse X") * sensitivityX;
		if( Input.GetAxis("Mouse X") != 0f){
			//if((transform.localPosition.x >= -1.5f && Input.GetAxis("Mouse X") <= 0.0f)
			  // || (transform.localPosition.x <= 1.5f && Input.GetAxis("Mouse X") >= 0.0f) ){
			transform.Translate (Vector3.right * moveX);
			//}
		}
		moveX = 0.0f;

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
