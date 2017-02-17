using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	public GameObject explosion;
	private GameManager gameManager;

	void Start ()
	{
		GameObject gameManagerObject = GameObject.FindWithTag ("GameManager");
		if (gameManagerObject != null) {
			gameManager = gameManagerObject.GetComponent<GameManager> ();
			Debug.Log ("Find");
		} else {
			Debug.Log ("Cannot Find");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Hazard") 
		{
			Instantiate(explosion, other.transform.position, other.transform.rotation);
			gameManager.DecreaseLife();
		}
		Destroy (other.gameObject);
	}
}
