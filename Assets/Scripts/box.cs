using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {

	private float left, right;
	// Use this for initialization
	void Start () {
		left = 10;
		right = 10;
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x <= -1) {
			transform.position = new Vector3(transform.position.x+right, transform.position.y, transform.position.z);
		}
		if (transform.position.x >= 9) {
			transform.position = new Vector3(transform.position.x-left, transform.position.y, transform.position.z);	
		}
		if (transform.position.y >= 9) {
			transform.position = new Vector3(transform.position.x, transform.position.y-left, transform.position.z);
		}
		if (transform.position.y <= -1) {
			transform.position = new Vector3(transform.position.x, transform.position.y+right, transform.position.z);
		}
	}
}
