using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Hazard") {
			Destroy (gameObject);
		}
		else
			return;
	}
}
