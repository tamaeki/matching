using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swap : MonoBehaviour {

	private GameObject activeTile = null;
	private GameObject secondTile = null;
	public GameObject border;
	public AudioSource select;
	GameObject fake, fake1;
	public int xPos, yPos, xPos2, yPos2;
	public bool FirstTile, swapped, moving = false;
	Vector3 temp1, temp2;

	void Start () {
		select.time = 1.80f;
	}
	
	// Update is called once per frame
	void Update () {

		if (activeTile != null && secondTile != null) {
			if (xPos == xPos2 && yPos + 1 == yPos2) {
				moving = true;
				swapTiles (activeTile, secondTile);
				FirstTile = false;
				activeTile = null;
				secondTile = null;
				Debug.Log (swapped);
			} else if (xPos == xPos2 && yPos - 1 == yPos2) {
				moving = true;
				swapTiles (activeTile, secondTile);
				FirstTile = false;
				activeTile = null;
				secondTile = null;
			} else if (xPos + 1 == xPos2 && yPos == yPos2) {
				moving = true;
				swapTiles (activeTile, secondTile);
				FirstTile = false;
				activeTile = null;
				secondTile = null;
			} else if (xPos - 1 == xPos2 && yPos == yPos2) {
				moving = true;
				swapTiles (activeTile, secondTile);
				FirstTile = false;
				activeTile = null;
				secondTile = null;
			} else {
				Debug.Log ("don't be an idiot");
				FirstTile = false;
				activeTile = null;
				secondTile = null;
				moving = false;
			}

		}

		if (FirstTile == false && Input.GetMouseButtonDown (0)) {
			swapped = false;
			Vector3 set1 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			xPos = (Mathf.RoundToInt((float)(set1.x/2)));
			yPos = (Mathf.RoundToInt((float)(set1.y/2)));
			Debug.Log (xPos);
			Debug.Log(yPos);
			setActiveTile ();
			FirstTile = true;
		}
		else if (FirstTile == true && Input.GetMouseButtonDown (0)){
			swapped = false;
			Vector3 set2 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			xPos2 = (Mathf.RoundToInt((float)(set2.x/2)));
			yPos2 = (Mathf.RoundToInt((float)(set2.y/2)));
			setSecondTile ();
			Debug.Log ("second set");
			}
	}

	void swapTiles(GameObject g1, GameObject g2){
		Destroy(GameObject.Find("border1(Clone)"));
		Destroy(GameObject.Find("border2(Clone)"));
		fake = new GameObject ("fake");
		fake1 = new GameObject ("fake1");
		fake.transform.position = new Vector3 (
			activeTile.transform.position.x, 
			activeTile.transform.position.y, 
			activeTile.transform.position.z);
		fake1.transform.position = new Vector3 (
			secondTile.transform.position.x, 
			secondTile.transform.position.y,
			secondTile.transform.position.z);
		
//		this bit works

//		Vector3 temp = new Vector3 (g1.transform.position.x, g1.transform.position.y, g1.transform.position.z);
//		g1.transform.position = g2.transform.position;
//		g2.transform.position = temp;
//		Debug.Log(g2.transform.position);

		StartCoroutine (Swapping (g1, g2));
	}

//  i have no idea why this code doesnt work.
//	IEnumerator SwappingTiles(GameObject g1, GameObject g2){
//		moving = true;
//
//		float duration = 2.0f;
//		float timer = 0.0f;
//
//		Vector3 g1_orig = g1.transform.position;
//		Vector3 g1_end = g2.transform.position;
//		Vector3 g2_orig = g2.transform.position;
//		Vector3 g2_end = g1.transform.position;
//
//		while(timer < duration){
//			Debug.Log ("hmmmm");
//			timer += Time.deltaTime;
//			g1.transform.position = Vector3.Lerp (g1_orig, g1_end, 2*Time.deltaTime);
//			g2.transform.position = Vector3.Lerp (g2_orig, g2_end, 2*Time.deltaTime);
//		}
//		g1.transform.position = g1_end;
//		g2.transform.position = g2_end;
//
//		moving = false;
//		yield return 0;
//	}

	IEnumerator Swapping(GameObject g1, GameObject g2){
		while (moving) {
			yield return new WaitForSeconds (0f);
			moveSlow (g1, g2);
		}
		Debug.Log ("it ends right");
	}

	void moveSlow(GameObject g1, GameObject g2){
		g1.transform.position = Vector3.MoveTowards (g1.transform.position, fake1.transform.position, 5*Time.deltaTime);
		g2.transform.position = Vector3.MoveTowards (g2.transform.position, fake.transform.position, 5*Time.deltaTime);

		if (fake1.transform.position == g1.transform.position) {
			Destroy (fake);
			Destroy (fake1);
			moving = false;
			swapped = true;
		}
	}
	void setActiveTile(){
		activeTile = GameObject.Find("GameManager").GetComponent<gameManager>().Grid
			[xPos, yPos].gameObject;
		Instantiate (border, activeTile.transform.position, Quaternion.identity);
		border.name = "border1";
		select.Play ();
	}

	void setSecondTile(){
		secondTile = GameObject.Find("GameManager").GetComponent<gameManager>().Grid
			[xPos2, yPos2].gameObject;
		Instantiate (border, secondTile.transform.position, Quaternion.identity);
		border.name = "border2";
		select.Play ();
	}

}


