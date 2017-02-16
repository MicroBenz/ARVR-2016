using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	// public GameObject playerExplosion;
	public int scoreValue;
	private GameManager gameManager;

	void Start()
	{
		GameObject gameManagerObject = GameObject.FindWithTag ("GameManager");
		if (gameManagerObject != null) {
			gameManager = gameManagerObject.GetComponent<GameManager> ();
			Debug.Log ("Find");
		} else {
			Debug.Log ("Cannot Find");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary") {
			return ;
		}
		Instantiate(explosion, transform.position, transform.rotation);
		// Debug.Log ("Non-Boundary");
		
		// if (other.tag == "Player") {
		// 	Instantiate(playerExplosion, other.transform.position, other.transform.rotation) ;
		// 	gameManager.GameOver ();
		// }
		// gameManager.AddScore (scoreValue);
		// Destroy (other.gameObject);
		// Destroy (gameObject);
	}
}
