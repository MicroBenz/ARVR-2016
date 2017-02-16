using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.up * speed;
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "BulletBoundary") {
			Debug.Log ("BOOM");
		}
		else
			return;
	}
}
