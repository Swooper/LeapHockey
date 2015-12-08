using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	private Text pScore;
	private Text cScore;
	// Use this for initialization
	void Start () {
		pScore = GameObject.Find("PlayerScoreText").GetComponent<Text>();
		cScore = GameObject.Find("ComputerScoreText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateScore(int playerScore, int computerScore) {
		pScore.text = "" + playerScore;
		cScore.text = "" + computerScore;
	}
}
