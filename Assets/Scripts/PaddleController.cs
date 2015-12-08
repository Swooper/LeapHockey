using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}
public class PaddleController : MonoBehaviour {

	private Rigidbody body;

	public float speed;
	public Boundary boundary;

	void Start () {
		boundary.xMin = -1.5f;
		boundary.xMax = 1.5f;
		boundary.zMin = -4.85f;
		boundary.zMax = -2.65f;
		speed = 15.0f;
		body = this.GetComponent<Rigidbody>();
	}

	// Update is called once per physics frame
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
	}
}
