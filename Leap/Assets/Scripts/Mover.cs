using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;
	public Camera mainCamera;
	public GameObject crosshair;
	public GameObject generatedCrosshair;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = CalculateSpeedVector() * speed;
		SpawnHelperCrosshair ();
	}

	Vector3 CalculateSpeedVector() {
		return transform.forward;
		Vector3 rbPos = rb.position;
		Vector3 cameraPos = mainCamera.transform.position;
		Vector3 normalVector = new Vector3 (rbPos.x - cameraPos.x, 0.0f, Mathf.Abs (cameraPos.z - rbPos.z));
		return normalVector.normalized;
	}

	void SpawnHelperCrosshair () {
		Vector3 crosshairPos = new Vector3 (rb.position.x, rb.position.y, 0);
		this.generatedCrosshair = Instantiate (crosshair, crosshairPos, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (rb.gameObject);
		if (rb.gameObject == null) {
			Debug.Log ("ON DESTROY");
		}
	}

	void OnDestroy() {
		Destroy (this.generatedCrosshair);
	}

	public void DoDestroy()
	{
		Destroy (this.generatedCrosshair);
	}
}
