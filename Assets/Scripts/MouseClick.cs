using UnityEngine;
using System.Collections;

public class MouseClick: MonoBehaviour {
	private static Transform selectedPiece;
	// The number of spaces a piece can move

	void Start () {
	}

	void Update () {

	}

	void OnMouseDown () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			
			if (hit.transform.CompareTag ("Tile")) {
				if (selectedPiece) {
					move (selectedPiece, hit.transform.position);
				}
			}
			else if (hit.transform.CompareTag ("Piece")) {
				
				if (selectedPiece) {
					unselect (selectedPiece);
				} else {
					select (hit.transform);
				}
			} 
				
		}
	}

	void move(Transform piece, Vector3 target){
		unselect (piece);
		piece.position = new Vector3 (target.x, 0.5f, target.z);
	}

	void unselect(Transform piece){
		piece.GetComponent<Renderer>().material.color = new Color(0f,0.1f,0.1f);
		selectedPiece = null;

//		Transform[] adjacent = adjacentTiles (piece);
//		for (int i = 0; i < adjacent.Length; i++) {
//			adjacent[i].GetComponent<MeshRenderer>().material.color = new Color(0.1f, 0.1f, 0.1f);
//		}
	}

	void select(Transform piece){
		selectedPiece = piece;
		selectedPiece.GetComponent<Renderer>().material.color = new Color(0,1,1);

		// find tiles that the piece can move to
//		Transform[] adjacent = adjacentTiles (selectedPiece);
//		for (int i = 0; i < adjacent.Length; i++) {
//			adjacent[i].GetComponent<MeshRenderer>().material.color = new Color(0,1,3);
//		}
	}

//	private Transform[] adjacentTiles(Transform piece){
//		int x = (int)piece.position.x;
//		int z = (int)piece.position.z;
//		Transform[] adjacent = new Transform[8];
//
//		Transform[,] tiles = boardTiles ();
//
//
//		adjacent[0] = tiles [x - 1, z];
//		adjacent[1] = tiles [x, z - 1];
//		adjacent[2] = tiles [x - 1, z - 1];
//		adjacent[3] = tiles [x + 1, z];
//		adjacent[4] = tiles [x, z + 1];
//		adjacent[5] = tiles [x + 1, x + 1];
//		adjacent[6] = tiles [x - 1, x + 1];
//		adjacent[7] = tiles [x + 1, x - 1];
//
//		return adjacent;
//	}
//
//	private Transform[,] boardTiles(){
//		GameObject board = GameObject.Find("Board");
//		return board.GetComponent<GenerateBoard> ().tiles;
//	}
////
}
