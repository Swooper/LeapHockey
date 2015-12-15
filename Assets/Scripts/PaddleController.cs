using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}
public class PaddleController : MonoBehaviour {

	private Rigidbody body;
	private GameObject model;

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
		boundary.xMin = -2.0f;
		boundary.xMax = 1.5f;
		boundary.zMin = -4.85f;
		boundary.zMax = -2.85f;
		speed = 15.0f;
		model = GameObject.Find("PaddleModel");
		body = model.GetComponent<Rigidbody>();
		_clipRecord = new AudioClip();
		_sampleWindow = 256;
		audio = GetComponent<AudioSource>();
		audio.clip = _clipRecord;
		spectrum = new float[512];
		Time.timeScale = 0.6f;
	}

	// Update is called once per physics frame
	void FixedUpdate () {
		// Vertical movement
		if (_isInitialized == false) {
			if (model.transform.position.x > -0.5f) {
				model.transform.position += Vector3.left * Time.deltaTime;
			}
			else if (model.transform.position.x < -0.5f) {
				model.transform.position += Vector3.right * Time.deltaTime;
			}
			if (model.transform.position.z > -4) {
				model.transform.position += Vector3.back * Time.deltaTime;
			}
			else if (model.transform.position.z < -4) {
				model.transform.position += Vector3.forward * Time.deltaTime;
			}
		}
		else {
			MicLoudness = LevelMax ();
			float moveVertical = MicLoudness;

			// Horizontal movement
			float moveHorizontal = 0.0f;
			if (getHorizontal ()) {
				moveHorizontal = 0.1f;
			}
			else {
				moveHorizontal = -0.1f;
			}
			//if there is no noise the paddle floats straight back
			if (MicLoudness < 0.01) {
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
		}
	}

	private bool getHorizontal() {
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		int xvalue =0;
		string buffer = "";
		float maxfreq = 0f;
		for (int x = 0; x < spectrum.Length; x++) {
			spectrum[x] = spectrum[x] * 100;
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
		
	public float  LevelMax()
	{
		float levelMax = 0;
		float[] waveData = new float[_sampleWindow];
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
	
	//stop mic when loading a new level or quit application
	void OnDisable() {
		StopMicrophone();
	}
	
	void OnDestroy() {
		StopMicrophone();
	}
	
	
	// make sure the mic gets started & stopped when application gets focused
	void OnApplicationFocus(bool focus) {  
		if (!focus) {
			Debug.Log("Pausep");
			StopMicrophone();
			_isInitialized=false;
		}
	}

	public void InitMic() {
		_clipRecord = Microphone.Start(Microphone.devices [1], true, 999, 44100);
		while (!(Microphone.GetPosition(Microphone.devices [1])>0)) {
		}
		GetComponent<AudioSource> ().PlayOneShot (_clipRecord);

	}

	public void StopMicrophone() {
		Microphone.End(Microphone.devices [1]);
	}
}
