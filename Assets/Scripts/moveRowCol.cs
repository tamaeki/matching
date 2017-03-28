using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRowCol : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	public bool moving = false;

	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			MoveThings ();
	}

	void MoveThings(){
		//gets x y coordinates of where mouse clicked
		//check axis
		//let player move slightly one way without moving tiles b/c what if they're bad at directions
	}

	void OnMouseDown() {

		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, 
				Input.mousePosition.y, 
				screenPoint.z));
		moving = true;

	}

	void OnMouseDrag(){
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp(){
		moving = false;
	}


}