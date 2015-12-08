using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private int playerScore;
	private int enemyScore;
	private int winScore;

	private HUD hud;

	// Use this for initialization
	void Start () {
		playerScore = 0;
		enemyScore = 0;
		winScore = 5;

		hud = GameObject.Find("GUICanvas").GetComponent<HUD>();
	}

	public void AddScore(bool player) {
		if(player) {
			playerScore++;
			if(playerScore >= winScore) {
				// Show victory screen, end match
			}
		}
		else {
			enemyScore++;
			if(enemyScore >= winScore) {
				// Show defeat screen, end match
			}
		}
		hud.UpdateScore(playerScore, enemyScore);
	}
}
