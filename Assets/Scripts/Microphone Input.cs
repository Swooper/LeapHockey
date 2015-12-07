using UnityEngine;
using System.Collections;


public class Microphone Input: MonoBehaviour {
	
	//[RequireComponent(typeof(AudioSource))]
	
	public AudioClip c;
	
	void Start() {
		foreach (string device in Microphone.devices) {
			Debug.Log("Name: " + device);
		}
	}
	
	/*void Start() {
		//AudioClip Micinput= Microphone.Start (null, true, 10, 44100);

		AudioSource aud = GetComponent<AudioSource>();
		aud.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
		aud.Play();
	}*/
	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.T)) {
			
			if(Microphone.IsRecording("Built-in Microphone")){
				StopRecording();
			}
			
			else{
				StartRecording();
			}
		}
		
		
	}
	void StartRecording()
	{
		c = Microphone.Start ("Built-in Microphone",true, 300, 44100);
		while (!(Microphone.GetPosition("Built-in Microphone")>0)) {
		}
		GetComponent<AudioSource> ().PlayOneShot (c);
		
		
	}
	
	void StopRecording()
	{
		Microphone.End ("Built-in Microphone");
		GetComponent<AudioSource> ().Stop ();
		
	}
	
}
