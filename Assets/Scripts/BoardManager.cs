using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
	public int boardSize;
	public Transform tilePrefab;
	public Transform highlightPrefab;
	public Transform piecePrefab;

	public Piece selectedPiece;

	void Start(){
		generateBoard ();
		generateHighlights ();
		generatePieces ();
	}

	private void generateBoard(){
		GameObject parent = GameObject.Find ("Tiles");

		for (int z = 0; z < boardSize; z++) {
			for (int x = 0; x < boardSize; x++) {
				
				Transform created = (Transform)Instantiate (tilePrefab, new Vector3 (x, 0, z), Quaternion.identity);
				created.SetParent (parent.GetComponent<Transform>());

				Tile.AddTile (created.GetComponent<Tile> ());
			}
		}
	}

	private void generateHighlights(){
		GameObject parent = GameObject.Find ("Highlights");

		for (int z = 0; z < boardSize; z++) {
			for (int x = 0; x < boardSize; x++) {

				Transform created = (Transform)Instantiate (highlightPrefab, new Vector3 (x, 0.1f, z), Quaternion.identity);
	
				created.SetParent (parent.GetComponent<Transform>());

				Highlight.AddHighlight (created.GetComponent<Highlight> ());
			}
		}
	}

	private void generatePieces(){
		GameObject parent = GameObject.Find ("Pieces");


		Transform created1 = (Transform)Instantiate (piecePrefab, new Vector3 (1, 1, 0), Quaternion.identity);
		created1.SetParent (parent.GetComponent<Transform>());

		Transform created2 = (Transform)Instantiate (piecePrefab, new Vector3 (3, 1, 0), Quaternion.identity);
		created2.SetParent (parent.GetComponent<Transform>());

		Transform created3 = (Transform)Instantiate (piecePrefab, new Vector3 (5, 1, 0), Quaternion.identity);
		created2.SetParent (parent.GetComponent<Transform>());

		Transform created4 = (Transform)Instantiate (piecePrefab, new Vector3 (7, 1, 0), Quaternion.identity);
		created2.SetParent (parent.GetComponent<Transform>());

		Piece.AddPiece (created1.GetComponent<Piece> ());
		Piece.AddPiece (created2.GetComponent<Piece> ());
		Piece.AddPiece (created3.GetComponent<Piece> ());
		Piece.AddPiece (created4.GetComponent<Piece> ());


	}

}
