using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(this.GetComponent<MeshCollider>(), GameObject.Find ("NeutralZone").GetComponent<BoxCollider>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
