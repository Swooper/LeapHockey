using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private int playerScore;
	private int enemyScore;
	private int winScore;

	private HUD hud;

	private GameObject player;
	private GameObject enemy;
	private GameObject puck;
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public GameObject puckPrefab;
	private GameObject playerStartPos;
	private GameObject playerPuckStart;
	private GameObject enemyStartPos;
	private GameObject enemyPuckStart;

	// Use this for initialization
	void Start () {
		playerScore = 0;
		enemyScore = 0;
		winScore = 5;

		hud = GameObject.Find("GUICanvas").GetComponent<HUD>();

		playerStartPos = GameObject.Find("PlayerStartPos");
		enemyStartPos = GameObject.Find("EnemyStartPos");
		playerPuckStart = GameObject.Find("PlayerPuckStart");
		enemyPuckStart = GameObject.Find("EnemyPuckStart");

		puck = GameObject.Instantiate(puckPrefab);
		player = GameObject.Instantiate(playerPrefab);
		enemy = GameObject.Instantiate(enemyPrefab);

		ResetPositions(true);
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
		ResetPositions(player);
	}

	// playerSide true if the puck should spawn on the player's side,
	// false if it should be on the other side
	private void ResetPositions(bool playerSide) {
		player.transform.position = playerStartPos.transform.position;
		enemy.transform.position = enemyStartPos.transform.position;
		if(playerSide) {
			puck.transform.position = playerPuckStart.transform.position;
		}
		else {
			puck.transform.position = enemyPuckStart.transform.position;
		}
	}
}
