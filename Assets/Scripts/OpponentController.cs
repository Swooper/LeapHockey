using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary2
{
	public float xMin, xMax, zMin, zMax;
}
public class OpponentController : MonoBehaviour {
	
	private Rigidbody body;
	//private GameObject model;
	public float speed;
	public Boundary2 boundary2;
	
	//public float loudness;
	private float MicLoudness;
	private string _device;
	private AudioClip _clipRecord;
	private int _sampleWindow;
	public bool _isInitialized = false;
	private AudioSource audio;
	float[] spectrum;
	

	void Awake () {
		boundary2.xMin = 0.265f;
		boundary2.xMax = 3.250f;
		boundary2.zMin = 4.85f;
		boundary2.zMax = 2.65f;

		speed = 15.0f;
		body = transform.FindChild("PaddleModel2").gameObject.GetComponent<Rigidbody>();
		_clipRecord = new AudioClip();
		_sampleWindow = 128;
		audio = GetComponent<AudioSource>();
		audio.clip = _clipRecord;
		spectrum = new float[512];
	}
	
	private bool getHorizontal() {
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		int xvalue =0;
		string buffer = "";
		float maxfreq = 0f;
		for (int x = 0; x < spectrum.Length; x++) {

			if (spectrum[x]>maxfreq) {
				maxfreq = spectrum[x];
				xvalue = x;
			}
			if(spectrum[x] < 0.0001f) {
				spectrum[x] = 0;
			}
			buffer = buffer + spectrum[x] + " ";
		}

		if(xvalue > 18) {
			return true;
		}
		else {
			return false;
		}
	}
	
	// Update is called once per physics frame
	void FixedUpdate () {
		// Vertical movement

		if (_isInitialized == false) {
			if (transform.position.x > 0)
				transform.position += Vector3.left * Time.deltaTime;
			else if (transform.position.x < 0)
				transform.position += Vector3.right * Time.deltaTime;
			if (transform.position.z > 4)
			{
				transform.position += Vector3.back * Time.deltaTime;
			}
			else if (transform.position.z < 4)
			{
				transform.position += Vector3.forward * Time.deltaTime;
			}
		} 
		else {
			MicLoudness = LevelMax ();
			float moveVertical = MicLoudness * -1;

			// Horizontal movement
			float moveHorizontal = 0.0f;
			if (getHorizontal ()) {
				moveHorizontal = 0.1f;
			} else {
				moveHorizontal = -0.1f;
			}
			//if there is no noize the paddle floats straight back
			if (MicLoudness < 0.01) {
				moveVertical = 0.1f;
				moveHorizontal = 0f;
			}

			//Putting it together
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			body.velocity = movement * speed;
		
			body.position = new Vector3 (
				Mathf.Clamp (body.position.x, boundary2.xMin, boundary2.xMax),
				0.0f,
				Mathf.Clamp (body.position.z, boundary2.zMin, boundary2.zMax)
			);
		}
	}
	
	public float  LevelMax()
	{
		float levelMax = 0;
		float[] waveData = new float[_sampleWindow];
		//Debug.Log (waveData);
		int micPosition = Microphone.GetPosition(Microphone.devices [0])-(_sampleWindow+1); // null means the first microphone
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
	
	// start mic when scene starts
	/*void OnEnable() {
		Debug.Log ("enable");
		InitMic();
		_isInitialized=true;
	}*/
	
	//stop mic when loading a new level or quit application
	void OnDisable() {
		StopMicrophone();
	}
	
	void OnDestroy() {
		StopMicrophone();
	}
	
	
	// make sure the mic gets started & stopped when application gets focused
	void OnApplicationFocus(bool focus) {
		
		/*if (focus)
		{
			//Debug.Log("Focus");
			
			if(!_isInitialized){
				//Debug.Log("Init Mic");
				StartCoroutine("oppenentMicInit");
			
				Debug.Log ("focuso");
				InitMic();
				_isInitialized=true;
			}
		} */
		if (!focus)
		{
			//Debug.Log("Pauseo");
			StopMicrophone();
			//Debug.Log("Stop Mic");
			_isInitialized=false;

		}
	}
	public void InitMic() {
		_clipRecord = Microphone.Start(Microphone.devices [0], true, 999, 44100);
		while (!(Microphone.GetPosition(Microphone.devices [0])>0)) {
		}
		GetComponent<AudioSource> ().PlayOneShot (_clipRecord);
		
	}
	/*void Update(){
		if (!Microphone.IsRecording (Microphone.devices[1]))
			InitMic ();


	}
	/*void LateUpdate(){
			if (Microphone.IsRecording (Microphone.devices[1])) {
				StopMicrophone ();
			}
	}*/
	
	
	public void StopMicrophone() {
		Microphone.End(Microphone.devices [0]);
		//GetComponent<AudioSource> ().PlayOneShot (_clipRecord);
	}
}

