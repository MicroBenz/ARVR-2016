using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int gameLife;
	// public int scoreValue;
	public int hazardDamage;
	public int winScore;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText lifeText;
	public GUIText winText;

	private bool gameOver;
	private bool restart;
	private bool win;
	private int score;
	private int life;

	// Use this for initialization
	void Start () {
		gameOver = false;
		restart = false;
		win = false;
		restartText.text = "";
		gameOverText.text = "";
		winText.text = "";
		score = 0;
		life = gameLife;
		UpdateScore ();
		UpdateLife ();
		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
		if (score == winScore) 
		{
			Win ();
		}
		else if(life == 0)
		{
			GameOver ();
		}
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (win) {
				Restart ();
				break;
			}

			if (gameOver) {
				Restart ();
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	public void DecreaseLife ()
	{
		life -= hazardDamage;
		UpdateLife ();
	}

	void UpdateScore() 
	{
		scoreText.text = "Score: " + score;
	}

	void UpdateLife()
	{
		lifeText.text = "Life: " + life;
	}

	void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	void Restart()
	{
		restartText.text = "Press R for restart";
		restart = true;
	}

	void Win()
	{
		winText.text = "Win !!!";
		win = true;
	}

	public bool isGameOver () 
	{
		return gameOver;
	}

	public bool isWin()
	{
		return win;
	}
		
}
