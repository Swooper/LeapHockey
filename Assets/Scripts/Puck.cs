using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {
	private float currZPos;
	private float lastZPos;
	private float maxSpeed = 10f;
	public GameObject player1obj;
	public GameObject player2obj;
	public PaddleController player1;
	public OpponentController player2;
	// Use this for initialization
	bool PuckDirection(){
		currZPos = transform.position.z;
		if(currZPos > lastZPos) {
			//print("moving forward");
			return true;
		} 
		else 
			return false;
		
	}

	IEnumerator switchmices()
	{	yield return new WaitForSeconds(1f);
		Debug.Log ("switch");
		player1.InitMic ();
		player1._isInitialized = true;
		Debug.Log ("switchdone");



	}

	public void InitPlayer1(){
		if (player2._isInitialized == true) {
			player2.StopMicrophone ();
			player2._isInitialized = false;
		}
		player1.InitMic ();
		player1._isInitialized = true;
	}

	public void InitPlayer2(){
		Debug.Log ("initplayer2");
		if (player1._isInitialized == true) {
			player1.StopMicrophone ();
			player1._isInitialized = false;
		}
		player2.InitMic ();
		player2._isInitialized = true;
	
	}

	void Start () {
		Physics.IgnoreCollision(this.GetComponent<MeshCollider>(), GameObject.Find ("NeutralZone").GetComponent<BoxCollider>());
		player1 = player1obj.GetComponent<PaddleController> ();
		player2 = player2obj.GetComponent<OpponentController> ();
		currZPos = transform.position.z;
		lastZPos = transform.position.z;

	}
	public void init(){
		StartCoroutine (switchmices());

	}
	// Update is called once per frame
	void FixedUpdate () {
		currZPos = transform.position.z;

		if (currZPos > -2.65f && player1._isInitialized == true && PuckDirection() == true) {
	
			InitPlayer2();
		}
		if (currZPos < 2.65f && player2._isInitialized == true && PuckDirection() == false) {
		
			InitPlayer1();
		}
		if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
		{
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
		}

		lastZPos = currZPos;
	}


	


}
