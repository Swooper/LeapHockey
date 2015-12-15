using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private int player1Score;
	private int player2Score;
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
		player1Score = 0;
		player2Score = 0;
		winScore = 5;

		hud = GameObject.Find("GUICanvas").GetComponent<HUD>();

		playerStartPos = GameObject.Find("PlayerStartPos");
		enemyStartPos = GameObject.Find("EnemyStartPos");
		playerPuckStart = GameObject.Find("PlayerPuckStart");
		enemyPuckStart = GameObject.Find("EnemyPuckStart");

		puck = GameObject.Instantiate(puckPrefab);
		player = GameObject.Instantiate(playerPrefab);
		enemy = GameObject.Instantiate(enemyPrefab);
		puck.GetComponent<Puck> ().player1obj = player;
		puck.GetComponent<Puck> ().player2obj = enemy;

		ResetPositions(true);
	}

	public void AddScore(bool player) {
		if(player) {
			player1Score++;
			if(player1Score >= winScore) {
				// Show victory screen, end match
			}
		}
		else {
			player2Score++;
			if(player2Score >= winScore) {
				// Show defeat screen, end match
			}
		}
		hud.UpdateScore(player1Score, player2Score);
		ResetPositions(player);
	}
	void Update()
	{
		if(Input.GetKeyDown (KeyCode.P))
			puck.GetComponent<Puck> ().init ();
	}
	// playerSide true if the puck should spawn on the player's side,
	// false if it should be on the other side
	private void ResetPositions(bool playerSide) {
		player.transform.position = playerStartPos.transform.position;
		enemy.transform.position = enemyStartPos.transform.position;
		puck.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		if(playerSide) {
			puck.transform.position = playerPuckStart.transform.position;
		}
		else {
			puck.transform.position = enemyPuckStart.transform.position;
		}
	}
}
