﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;
	public float fireRate;
	private float nextFire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Vector3 mouseVector = Input.mousePosition;
			mouseVector.z = 5;
			mouseVector = Camera.main.ScreenToWorldPoint (mouseVector);
			Instantiate (bullet, mouseVector, bullet.transform.rotation);
		}
	}
}
