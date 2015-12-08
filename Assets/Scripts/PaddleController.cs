using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}
public class PaddleController : MonoBehaviour {

//	private float moveZ;
//	private float moveX;
//	private float sensitivityZ;
//	private float sensitivityX;

	private Rigidbody body;

	public float speed;
	public Boundary boundary;

	void Start () {
//		moveZ = 0.0f;
//		moveX = 0.0f;
//		sensitivityZ = 0.3f;
//		sensitivityX = 0.3f;

		boundary.xMin = -1.5f;
		boundary.xMax = 1.5f;
		boundary.zMin = -4.85f;
		boundary.zMax = -2.65f;
		speed = 15.0f;
		body = this.GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Mouse X");
		float moveVertical = Input.GetAxis("Mouse Y");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		body.velocity = movement * speed;

		body.position = new Vector3(
			Mathf.Clamp (body.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);


		/*moveZ += Input.GetAxis("Mouse Y") * sensitivityZ;
		if (Input.GetAxis("Mouse Y") != 0f){
			//transform.Translate(Vector3.forward * moveZ);
			body.MovePosition(Vector3.forward * moveZ);
		}
		moveZ = 0.0f;
		
		moveX += Input.GetAxis("Mouse X") * sensitivityX;
		if( Input.GetAxis("Mouse X") != 0f){
			//transform.Translate (Vector3.right * moveX);
			body.MovePosition(Vector3.right * moveX);
		}
		moveX = 0.0f;*/
	}
}
