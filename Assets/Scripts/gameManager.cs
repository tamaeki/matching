using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {


	public tile tilePrefab;
	public int type;
	GameObject fake;
	public GameObject[] strokes = {
	};

	public tile[,]Grid;
	public int GRIDSIZE_X = 5;
	public int GRIDSIZE_Y= 5;

	public int GRIDSIZEUNIT = 2;
	public Vector3 gridPosition;
	public int x, y, xPos, yPos, xPos2, yPos2;

	public AudioSource match;
	private bool updated = false;
	// Use this for initialization
	void Start () {
		createGrid (GRIDSIZE_X, GRIDSIZE_Y);
		x = (int)gameObject.transform.position.x/2;
		y = (int)gameObject.transform.position.y/2;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("GameManager").GetComponent<swap> ().swapped == true) {
			UpdateGrid ();
			checkMatches ();
		}
		if (Input.GetKeyDown (KeyCode.Space))
			checkMatches ();

	}

	//this is wrong for some reason. i think.
	void UpdateGrid(){
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (Grid [i, j] != null) {

					int a = (int)Grid [i, j].gameObject.transform.position.x / 2;
					int b = (int)Grid [i, j].gameObject.transform.position.y / 2;


					if (i != a || j != b) {
						GameObject temp = Grid [i, j].gameObject;
						Grid [i, j] = Grid [a, b].gameObject.GetComponent<tile> ();
						Grid [a, b] = temp.GetComponent<tile> ();
					}
				}else if(Grid [i, j] == null){
					fake = new GameObject ("gravity temp");
					Vector2 temp = new Vector2 (i * 2, j * 2);
					Instantiate (fake, temp, Quaternion.identity);
					falling ();
					Debug.Log (temp);
					Grid [i, j + 1].gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1;
//					Grid [i, j] = Grid [i, j + 1].gameObject.GetComponent<tile> ();
//					Grid [i, j + 1] = null;
					Debug.Log (Grid [i, j + 1].gameObject.transform.position);
					if (Grid [i, j + 1].gameObject.transform.position.y ==
					    temp.y) {
						Debug.Log ("hello gravity?");
						Grid [i, j + 1].gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
					}
//					Vector2 temp = Grid [i, j + 1].gameObject.transform.position;
//					temp.y -= 2;
//					Grid [i, j + 1].gameObject.transform.position = temp;
				}

			}
		}
		GameObject.Find ("GameManager").GetComponent<swap> ().swapped = false;
		updated = true;
	}
	void falling(){
		Debug.Log ("can you stop falling wtf");
	}
	void createGrid(int x, int y){
		Grid = new tile[x, y];

		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				gridPosition = new Vector3 (i * GRIDSIZEUNIT, j * GRIDSIZEUNIT,0);
				Grid [i,j] = Instantiate(strokes[Random.Range (0, strokes.Length)], gridPosition, Quaternion.identity).GetComponent<tile>();
				//Grid [i,j].color = tileS [Random.Range (0, tileColors.Length)];
				//Grid [i,j].GetComponent<Renderer>().material.color = Grid[i,j].color;
			}
		}
	}



	void checkMatches ()
	{
		
		//check vertical. this works but the horizontal part doesnt....tragic.
		for (int i = 0; i < 5; i++) {
			List <GameObject> goodLuck = new List<GameObject> ();
			for (int j = 0; j < 5; j++) {
				
				int k = j;
				while (k < 5 && k > 0) {
					if (Grid [i, j].gameObject == null)
						continue;
					if (Grid [i, k].GetComponent<tile> ().ha ==
						Grid [i, k - 1].GetComponent<tile> ().ha) {
						if (k == 1) {
							goodLuck.Add (Grid [i, k].gameObject);
							goodLuck.Add (Grid [i, k - 1].gameObject);
						} else {
							goodLuck.Add (Grid [i, k].gameObject);

						}
					} else if (goodLuck.Count == 3) {
						matchSFX ();
						for (int a = 0; a < goodLuck.Count; a++) {
							Destroy (goodLuck [a]);
						}
						goodLuck.Clear ();
						break;
					} else if (goodLuck.Count >=4){
						matchSFX ();
						makeSpecialTile ();
						for (int a = 0; a < goodLuck.Count; a++) {
							Destroy (goodLuck [a]);
						}
						goodLuck.Clear ();
						break;
					}else {
						goodLuck.Clear ();
						goodLuck.Add (Grid [i, k].gameObject);
					}
					k++;
				}
			
				if (goodLuck.Count >= 3) {
					for (int a = 0; a < goodLuck.Count; a++) {
						Destroy (goodLuck [a]);
					}
				} else {
					goodLuck = new List<GameObject> ();
					//goodLuck.Clear();
				}

			}
		}
		//weakjt

		//check horizontal
		for (int i = 0; i < 5; i++) {
			List <GameObject> goodLuck = new List<GameObject> ();
			for (int j = 0; j < 5; j++) {
				int k = i;
				while (k < 5 && k > 0) {
					if (Grid [k, j].gameObject == null)
						continue;
						if (Grid [k, j].GetComponent<tile>().ha ==
						Grid [k-1, j].GetComponent<tile>().ha) {
						if (k == 1) {
							goodLuck.Add (Grid [k, j].gameObject);
							goodLuck.Add (Grid [k-1,j].gameObject);
						} else {
							goodLuck.Add (Grid [k, j].gameObject);

						}
					} else if (goodLuck.Count >= 3) {
						matchSFX ();
						for (int a = 0; a < goodLuck.Count; a++) {
							Destroy (goodLuck [a]);
						}
						goodLuck.Clear ();
						break;
					} else {
						goodLuck.Clear ();
						goodLuck.Add (Grid [k, j].gameObject);
					}
					k++;
				}

				if (goodLuck.Count >= 3) {
					for (int a = 0; a < goodLuck.Count; a++) {
						Destroy (goodLuck [a]);
					}
				} else {
					goodLuck = new List<GameObject> ();
					//goodLuck.Clear();
				}
			}
		}
	}
	void makeSpecialTile(){
		Debug.Log ("we did it fam");
		//Debug.Log (GameObject.Find ("GameManager").GetComponent<tile> ().ha);
	}
	void matchSFX(){
		match.Play ();
	}
}
	