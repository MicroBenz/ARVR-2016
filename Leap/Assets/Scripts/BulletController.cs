using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
		GameObject gameManagerObject = GameObject.FindWithTag ("GameManager");
		if (gameManagerObject != null) {
			gameManager = gameManagerObject.GetComponent<GameManager> ();
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "BulletBoundary") {
			Debug.Log ("BOOM");
		}
		else if (other.tag == "Hazard") {
			Debug.Log ("ADD SCORE!");
			Destroy (gameObject);
			// gameManager.AddScore (1);
		}
		else
			return;
	}
}
