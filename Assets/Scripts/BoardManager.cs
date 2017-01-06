using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
	public int boardSize;
	public Transform tilePrefab;
	public Transform highlightPrefab;
	public Transform piecePrefab;
	public TextMesh StatusTextPrefab;

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

		List<PieceData> pieces = new List<PieceData>();

		// Player 1
		pieces.Add (new PieceData(new Vector3 (1, 0.15f, 0), 1));
		pieces.Add (new PieceData(new Vector3 (3, 0.15f, 0), 1));
		pieces.Add (new PieceData(new Vector3 (5, 0.15f, 0), 1));
		pieces.Add (new PieceData(new Vector3 (7, 0.15f, 0), 1));

		// Player 2
		pieces.Add (new PieceData(new Vector3 (1, 0.15f, boardSize - 1), 2));
		pieces.Add (new PieceData(new Vector3 (3, 0.15f, boardSize - 1), 2));
		pieces.Add (new PieceData(new Vector3 (5, 0.15f, boardSize - 1), 2));
		pieces.Add (new PieceData(new Vector3 (7, 0.15f, boardSize - 1), 2));


		// NOTE - create an empty status text above each piece

		foreach (PieceData pieceData in pieces) {
			GameObject pieceContainer = new GameObject ();
			pieceContainer.transform.position = new Vector3(pieceData.position.x, 0, pieceData.position.z);

			Transform piece;
			if (pieceData.team == 1) {
				piece = (Transform)Instantiate (piecePrefab, pieceData.position, Quaternion.identity);
			} else {
				piece = (Transform)Instantiate (piecePrefab, pieceData.position, Quaternion.Euler(new Vector3(0, 180, 0)));
			}

			// rotate 180 degrees

			piece.SetParent (pieceContainer.transform);
			pieceContainer.transform.SetParent (parent.transform);

			TextMesh text = (TextMesh)Instantiate (StatusTextPrefab, new Vector3(0, 1.5f), Quaternion.Euler(new Vector3(0, 90)));
			text.transform.SetParent (pieceContainer.transform);
			piece.GetComponent<Piece> ().StatusText = text;
			piece.GetComponent<Piece> ().team = pieceData.team;


			Piece.AddPiece (piece.GetComponent<Piece> ());
		}

	}

	private class PieceData {
		public Vector3 position;
		public int team;

		public PieceData(Vector3 position, int team){
			this.position = position;
			this.team = team;
		}
	}

}
