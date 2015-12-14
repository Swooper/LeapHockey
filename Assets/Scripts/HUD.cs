using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	private Text pScore;
	private Text cScore;
	private Image micImage1;
	private Image micImage2;
	// Use this for initialization
	void Start () {
		pScore = GameObject.Find("PlayerScoreText").GetComponent<Text>();
		cScore = GameObject.Find("OpponentScoreText").GetComponent<Text>();
		micImage1 = GameObject.Find("MicImage1").GetComponent<Image>();
		micImage2 = GameObject.Find("MicImage2").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(getPlayerMicStatus(1)) {
			micImage1.enabled = true;
		}
		else {
			micImage1.enabled = false;
		}
		if(getPlayerMicStatus(2)) {
			micImage2.enabled = true;
		}
		else {
			micImage2.enabled = false;
		}			
	}

	public void UpdateScore(int playerScore, int computerScore) {
		pScore.text = "" + playerScore;
		cScore.text = "" + computerScore;
	}

	private bool getPlayerMicStatus(int player) {
		return Microphone.IsRecording(Microphone.devices[player-1]);
	}
}
