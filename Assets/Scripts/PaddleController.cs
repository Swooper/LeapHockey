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
	private bool _isInitialized;
	private AudioSource audio;
	float[] spectrum = new float[256];


	void Awake () {
		boundary.xMin = -1.5f;
		boundary.xMax = 1.5f;
		boundary.zMin = -4.85f;
		boundary.zMax = -2.65f;
		speed = 15.0f;
		body = GetComponent<Rigidbody>();
		_clipRecord = new AudioClip();
		_sampleWindow = 128;
		audio = GetComponent<AudioSource>();
		audio.clip = _clipRecord;
	}

	void Update() {

		//while (!(Microphone.GetPosition(_device)>0)) {
		//}

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
			if (spectrum[x]>maxfreq)
			{
				maxfreq = spectrum[x];
				xvalue = x;
			}
			if(spectrum[x] < 0.0001f)
			{
				spectrum[x] = 0;
			}

				buffer = buffer + spectrum[x] + " ";	
		}
		Debug.Log (maxfreq);
		Debug.Log ("x : " + xvalue);
		//Debug.Log (buffer);
	}
	// Update is called once per physics frame
	void FixedUpdate () {
		MicLoudness = LevelMax ();
		float loudness = MicLoudness;
		//loudness = Mathf.Pow (loudness, 10);
		//loudness = loudness * 10;

		//float moveHorizontal = Input.GetAxis("Mouse X");
		//float moveVertical = Input.GetAxis("Mouse Y");

		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Debug.Log (moveHorizontal);

		float moveVertical = loudness;
		if (loudness < 0.01) {
			moveVertical = -0.1f;
		
		}
		Debug.Log ("Loudness :" + loudness);

		//Debug.Log (loudness);

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		body.velocity = movement * speed;

		body.position = new Vector3(
			Mathf.Clamp (body.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);
		loudness = 0f;
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

	// start mic when scene starts
	void OnEnable() {
		InitMic();
		_isInitialized=true;
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
	void InitMic() {
		if(_device == null) _device = Microphone.devices[0];
		_clipRecord = Microphone.Start(_device, true, 999, 44100);
		while (!(Microphone.GetPosition(_device)>0)) {
		}
		GetComponent<AudioSource> ().PlayOneShot (_clipRecord);

	}
	void StopMicrophone() {
		Microphone.End(_device);
	}
}
