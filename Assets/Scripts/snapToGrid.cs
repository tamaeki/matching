using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGrid : MonoBehaviour {

	public const float GRIDUNIT = 2f;
	bool dragging = false;

	void Update () {
		// click to drag
		if (!dragging && Input.GetMouseButtonDown(0)) {
			dragging = true;
		} else if (Input.GetMouseButtonUp(0)) {
			dragging = false;
			Snap(); // when we let go, snap to grid
		}

	}

	// get the delta away from nearest grid unit and snap to upper bound or lower bound
	public void Snap() {
		float x, y, z;

		if (transform.position.x % GRIDUNIT >= GRIDUNIT / 2) {
			x = transform.position.x - transform.position.x % GRIDUNIT + GRIDUNIT;
		} else {
			x = transform.position.x - transform.position.x % GRIDUNIT;
		}

		if (transform.position.y % GRIDUNIT >= GRIDUNIT / 2) {
			y = transform.position.y - transform.position.y % GRIDUNIT + GRIDUNIT;
		} else {
			y = transform.position.y - transform.position.y % GRIDUNIT;
		}

		if (transform.position.z % GRIDUNIT >= GRIDUNIT / 2) {
			z = transform.position.z - transform.position.z % GRIDUNIT + GRIDUNIT;
		} else {
			z = transform.position.z - transform.position.z % GRIDUNIT;
		}

		transform.position = new Vector3(x, y, z);
	
	}
}
