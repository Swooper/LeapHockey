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

	//public float loudness;
	private float MicLoudness;
	private string _device;
	private AudioClip _clipRecord;
	private int _sampleWindow;
	public bool _isInitialized = false;
	private AudioSource audio;
	float[] spectrum;
	
	void Awake () {
		boundary.xMin = -1.5f;
		boundary.xMax = 1.5f;
		boundary.zMin = -4.85f;
		boundary.zMax = -2.65f;
		speed = 15.0f;
		body = GetComponent<Rigidbody>();
		_clipRecord = new AudioClip();
		_sampleWindow = 256;
		audio = GetComponent<AudioSource>();
		audio.clip = _clipRecord;
		spectrum = new float[256];
		Time.timeScale = 0.6f;
	}

	private bool getHorizontal() {
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		int i = 1;
		while (i < spectrum.Length-1) {
			Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
			Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.yellow);
			i++;
		}
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

		//Debug.Log (buffer);
		//Debug.Log ("x : " + xvalue);
		if(xvalue > 4) {
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
			if (transform.position.z > -4) {
				transform.position += Vector3.back * Time.deltaTime;
			} else if (transform.position.z < -4) {
				transform.position += Vector3.forward * Time.deltaTime;
			}
		} else {

			MicLoudness = LevelMax ();
			float loudness = MicLoudness;
			float moveVertical = loudness;

			// Horizontal movement
			float moveHorizontal = 0.0f;
			if (getHorizontal ()) {
				moveHorizontal = 0.1f;
			} else {
				moveHorizontal = -0.1f;
			}
			//if there is no noize the paddle floats straight back
			if (loudness < 0.01) {
				moveVertical = -0.1f;
				moveHorizontal = 0f;
			}


			//Putting it together
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			body.velocity = movement * speed;

			body.position = new Vector3 (
			Mathf.Clamp (body.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (body.position.z, boundary.zMin, boundary.zMax)
			);
			loudness = 0f;
		}
	}

	public float  LevelMax()
	{
		float levelMax = 0;
		float[] waveData = new float[_sampleWindow];
		//Debug.Log (waveData);
		int micPosition = Microphone.GetPosition(Microphone.devices [1])-(_sampleWindow+1); // null means the first microphone
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
	//void OnEnable() {
	//	Debug.Log ("enablep");
	//	InitMic();
	//	_isInitialized=true;
	//}
	
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
				Debug.Log ("focusp");
				InitMic();
				_isInitialized=true;
			}
		} */     
		if (!focus)
		{
			Debug.Log("Pausep");
			StopMicrophone();
			//Debug.Log("Stop Mic");
			_isInitialized=false;
			
		}
	}
	public void InitMic() {
		//foreach (string device in Microphone.devices) {
		//	Debug.Log ("Name: " + device);
		//}
		//Debug.Log (_device);
		if(_device == null) _device = Microphone.devices[0];
		//
		//
		//Debug.Log (_device);
		//int min;
		//int max;
		//Microphone.GetDeviceCaps (Microphone.devices [1], out min, out max);
		//Debug.Log (Microphone.devices[1]);
		//Debug.Log (min);
		//Debug.Log (max);

		_clipRecord = Microphone.Start(Microphone.devices [1], true, 999, 44100);
		//while (!(Microphone.GetPosition(Microphone.devices [1])>0)) {
		//}
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
		Microphone.End(Microphone.devices [1]);
		//GetComponent<AudioSource> ().PlayOneShot (_clipRecord);
	}
}
