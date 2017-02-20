using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageController : MonoBehaviour {

	public GameObject explosionBullet;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		GameObject gameManagerObject = GameObject.FindWithTag ("GameManager");
		if (gameManagerObject != null) {
			gameManager = gameManagerObject.GetComponent<GameManager> ();
		} else {
			Debug.Log ("Cannot Find");
		}
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
		int i = 0;
		while (i < hitColliders.Length) {
			if (hitColliders [i].tag == "Hazard") {
				Debug.Log ("Destroy Effect");
				Destroy (hitColliders [i].gameObject);
				Instantiate(explosionBullet, hitColliders [i].transform.position, hitColliders [i].transform.rotation);
				if (!gameManager.isGameOver() && !gameManager.isWin()) {
					gameManager.AddScore (1);
				}
			}
			i++;
		}
	}

}
