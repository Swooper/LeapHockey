using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {
	private float currZPos;
	private float lastZPos;
	
	public GameObject player1obj;
	public GameObject player2obj;
	public PaddleController player1;
	public OpponentController player2;
	// Use this for initialization
	bool checkIfPuckIsNotMoving(){
		currZPos = transform.position.z;
		if (currZPos == lastZPos) {
			//print ("Not moving");
			return true;
		} 
		else 
			return false;
	}
	bool puckDirection(){
		currZPos = transform.position.z;
		if(currZPos > lastZPos) {
			//print("moving forward");
			return true;
		} 
		else 
			return false;
		
	}
	IEnumerator switchmices()
	{	Debug.Log ("switch");
		player1.InitMic ();
		player1._isInitialized = true;
		Debug.Log ("1");
		yield return new WaitForSeconds(4f);
		player1.StopMicrophone ();
		player1._isInitialized = false;
		Debug.Log ("1stopped");

		yield return new WaitForSeconds(2f);
		player2.InitMic ();
		player2._isInitialized = true;
		Debug.Log ("2on");
		yield return new WaitForSeconds(4f);
		player2.StopMicrophone ();
		player2._isInitialized = false;
		Debug.Log ("2off");


		//	player1._isInitialized = true;
		//	player1._isInitialized = true;
	}

	void Start () {
		Physics.IgnoreCollision(this.GetComponent<MeshCollider>(), GameObject.Find ("NeutralZone").GetComponent<BoxCollider>());
		player1 = player1obj.GetComponent<PaddleController> ();
		player2 = player2obj.GetComponent<OpponentController> ();

	}
	public void init(){
		StartCoroutine (switchmices());

	}
	// Update is called once per frame
	void FixedUpdate () {


		//currZPos = transform.position.z;
		//if (checkIfPuckIsNotMoving ()) {
		//
		//}
		//if (currZPos <= 0f && player1._isInitialized == false) {
		//	player1.InitMic ();
		//	player1._isInitialized = true;
		//} else if(player1._isInitialized == true) {
		//	player1.StopMicrophone ();
		//	player1._isInitialized = false;
		//}
		//lastZPos = currZPos;
	}


	


}
