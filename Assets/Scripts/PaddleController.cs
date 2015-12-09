using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}
public class PaddleController : MonoBehaviour {

	public float MicLoudness;
	
	private string _device;
	private Rigidbody body;

	public float speed;
	public Boundary boundary;
	//public float loudness;

	void InitMic(){
		if(_device == null) _device = Microphone.devices[0];
		_clipRecord = Microphone.Start(_device, true, 999, 44100);
	}
	void StopMicrophone()
	{
		Microphone.End(_device);
	}
	
	
	AudioClip _clipRecord = new AudioClip();
	int _sampleWindow = 128;


	void Start () {
		boundary.xMin = -1.5f;
		boundary.xMax = 1.5f;
		boundary.zMin = -4.85f;
		boundary.zMax = -2.65f;
		speed = 15.0f;
		body = this.GetComponent<Rigidbody>();
	}
	public float  LevelMax()
	{
		float levelMax = 0;
		float[] waveData = new float[_sampleWindow];
		Debug.Log (waveData);
		int micPosition = Microphone.GetPosition(null)-(_sampleWindow+1); // null means the first microphone
		if (micPosition < 0) return 0;
		_clipRecord.GetData(waveData, micPosition);
		// Getting a peak on the last 128 samples
		for (int i = 0; i < _sampleWindow; i++) {
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak) {
				levelMax = wavePeak;
			}
		}
		
		return levelMax;
	}

	// Update is called once per physics frame
	void FixedUpdate () {
		MicLoudness = LevelMax ();
		float loudness = MicLoudness;
		loudness = Mathf.Pow (loudness, 10);
		loudness = loudness * 100;


		float moveHorizontal = Input.GetAxis("Mouse X");
		//float moveVertical = Input.GetAxis("Mouse Y");

		float moveVertical = loudness;
		if (loudness == 0) {
			moveVertical = -0.1f;
		
		}
		//Debug.Log ("Loudness :" + loudness);

		Debug.Log (loudness);

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		body.velocity = movement * speed;

		body.position = new Vector3(
			Mathf.Clamp (body.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);
	}
	bool _isInitialized;
	// start mic when scene starts
	void OnEnable()
	{
		InitMic();
		_isInitialized=true;
	}
	
	//stop mic when loading a new level or quit application
	void OnDisable()
	{
		StopMicrophone();
	}
	
	void OnDestroy()
	{
		StopMicrophone();
	}
	
	
	// make sure the mic gets started & stopped when application gets focused
	void OnApplicationFocus(bool focus) {
		if (focus)
		{
			//Debug.Log("Focus");
			
			if(!_isInitialized){
				//Debug.Log("Init Mic");
				InitMic();
				_isInitialized=true;
			}
		}      
		if (!focus)
		{
			//Debug.Log("Pause");
			StopMicrophone();
			//Debug.Log("Stop Mic");
			_isInitialized=false;
			
		}
	}
}
