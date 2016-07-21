using UnityEngine;
using System.Collections;

public class PieceSelect : MonoBehaviour {

	private BoardManager manager;

	void Start(){
		manager = GameObject.Find ("Board").GetComponent<BoardManager> ();
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Highlight.ClearHighlights ();
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 25.0f)) {				
				Vector3 pos = hit.transform.position;
				if (Piece.Exists (pos.x, pos.z)) {
					selectPiece (pos);
				} else {
					movePiece (pos);
				}
			}
		}

	}

	private void selectPiece(Vector3 pos){
		Piece selected = Piece.GetPiece (pos.x, pos.z);

		if (manager.selectedPiece == selected) {
			manager.selectedPiece = null;
		} else {
			manager.selectedPiece = selected;
			Debug.Log ("selected and highlighting");
			foreach(Vector3 move in manager.selectedPiece.possibleMoves()){
				if (Highlight.Exists (move.x, move.z)) {
					Highlight highlight = Highlight.GetHighlight (move.x, move.z);
					highlight.gameObject.SetActive (true);
				}

			}
		}
	}

	private void movePiece(Vector3 pos){
		if (manager.selectedPiece) {
			Vector3 pieceCentre = new Vector3 (pos.x + 0.5f, 0, pos.z + 0.5f);
			if (manager.selectedPiece.possibleMoves ().Contains (pieceCentre)) {
				manager.selectedPiece.MoveTo (pos.x, pos.z);
			}
			manager.selectedPiece = null;
		}
	}
}
