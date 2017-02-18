using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosionBullet;
	public GameObject explosionBomb;
	// public GameObject playerExplosion;
	private GameManager gameManager;

	void Start()
	{
		GameObject gameManagerObject = GameObject.FindWithTag ("GameManager");
		if (gameManagerObject != null) {
			gameManager = gameManagerObject.GetComponent<GameManager> ();
		} else {
			Debug.Log ("Cannot Find");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary") {
			return ;
		}
		if (other.tag == "Bullet") {
			Instantiate(explosionBullet, transform.position, transform.rotation);
		}
		else if(other.tag == "Bomb") {
			Instantiate(explosionBomb, transform.position, transform.rotation);
		}
		// if (other.tag == "Player") {
		// 	Instantiate(playerExplosion, other.transform.position, other.transform.rotation) ;
		// 	gameManager.GameOver ();
		// }
		gameManager.AddScore (1);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
