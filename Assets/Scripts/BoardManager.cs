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

				GameObject tileContainer = new GameObject ();
				tileContainer.transform.position = new Vector3 (x, 0, z);
				Transform tile = (Transform)Instantiate (tilePrefab, tileContainer.transform.position, Quaternion.identity);

				tile.SetParent (tileContainer.transform);
				tileContainer.transform.SetParent (parent.transform);

				Tile.AddTile (tile.GetComponent<Tile> ());
			}
		}

	}

	private void generateHighlights(){
		GameObject parent = GameObject.Find ("Highlights");

		for (int z = 0; z < boardSize; z++) {
			for (int x = 0; x < boardSize; x++) {

				GameObject highlightContainer = new GameObject ();
				Vector3 position = new Vector3 (x, 0, z);
				highlightContainer.transform.position = position;
				Transform highlight = (Transform)Instantiate (highlightPrefab, new Vector3(position.x, 0.1f, position.z), Quaternion.identity);

				highlight.SetParent (highlightContainer.transform);
				highlightContainer.transform.SetParent (parent.transform);

				Highlight.AddHighlight (highlight.GetComponent<Highlight> ());
			}
		}
	}

	private void generatePieces(){
		GameObject parent = GameObject.Find ("Pieces");

		// create an empty parent class for each piece
		// attach that parent to the Pieces object

		List<Vector3> positions = new List<Vector3>();

		positions.Add (new Vector3 (1, 1, 0));
		positions.Add (new Vector3 (3, 1, 0));
		positions.Add (new Vector3 (5, 1, 0));
		positions.Add (new Vector3 (7, 1, 0));

		foreach (Vector3 position in positions) {
			GameObject pieceContainer = new GameObject ();
			pieceContainer.transform.position = new Vector3(position.x, 0, position.z);

			Transform piece = (Transform)Instantiate (piecePrefab, pieceContainer.transform.position, Quaternion.identity);

			piece.SetParent (pieceContainer.transform);
			pieceContainer.transform.SetParent (parent.transform);
			piece.position = new Vector3 (0, position.y, 0);


			Piece.AddPiece (piece.GetComponent<Piece> ());
		}

	}

}
