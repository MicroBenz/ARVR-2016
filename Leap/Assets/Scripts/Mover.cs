using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;
	public Camera mainCamera;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = CalculateSpeedVector() * speed;
	}

	Vector3 CalculateSpeedVector() {
		Vector3 rbPos = rb.position;
		Vector3 cameraPos = mainCamera.transform.position;
		Vector3 normalVector = new Vector3 (rbPos.x - cameraPos.x, 0.0f, Mathf.Abs (cameraPos.z - rbPos.z));
		return normalVector.normalized;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
