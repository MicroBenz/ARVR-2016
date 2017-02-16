using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject hazard;
	public GameObject bullet;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int gameLife;

	public GUIText scoreText;
	// public GUIText restartText;
	// public GUIText gameOverText;
	public GUIText lifeText;

	// private bool gameOver;
	// private bool restart;
	private int score;
	private int life;

	// Use this for initialization
	void Start () {
		// gameOver = false;
		// restart = false;
		// restartText.text = "";
		// gameOverText.text = "";
		score = 0;
		life = gameLife;
		UpdateScore ();
		UpdateLife ();
		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
		// if (restart) {
		// 	if (Input.GetKeyDown (KeyCode.R)) {
		// 		Application.LoadLevel (Application.loadedLevel);
		// 	}
		// }
		if (Input.GetButtonDown("Fire1")) {
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			Vector3 mouseWorldPoint = ray.origin + (ray.direction * 2);
			Vector3 v = Input.mousePosition;
			v.z = 5;
			v = Camera.main.ScreenToWorldPoint (v);
			Instantiate (bullet, v, Camera.main.transform.rotation);
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

			// if (gameOver) {
			// 	restartText.text = "Press R for restart";
			// 	restart = true;
			// 	break;
			// }
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	public void DecreaseLife (int newLifeValue)
	{
		life -= newLifeValue;
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
}
