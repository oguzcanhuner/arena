using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private BoardManager boardManager;
	private StateMachine stateMachine;

	void Start(){
		boardManager = GameObject.Find ("Board").GetComponent<BoardManager> ();
		stateMachine = GameObject.Find ("Board").GetComponent<StateMachine> ();
	}

	void Update(){
		switch (stateMachine.currentState) {
		case "waiting_to_move":
			waitingToMove();
			break;
		case "selecting_move_target":
			selectMoveTarget();
			break;
		}



	}

	// STATES

	private void waitingToMove(){
		if (Input.GetMouseButtonDown (0)) {
			Vector3 pos = pressDown (Input.mousePosition);

			if (Piece.Exists (pos.x, pos.z)) {
				selectPiece (pos);
				stateMachine.selectUnitToMove ();
			}// else do nothing
		}
	}
	private void selectMoveTarget(){
		if (Input.GetMouseButtonDown (0)) {
			Highlight.ClearHighlights ();

			Vector3 pos = pressDown (Input.mousePosition);

			if (Piece.Exists (pos.x, pos.z)) {
				selectPiece (pos);
				stateMachine.selectUnitToMove ();
			} else {
				movePiece (pos);
				stateMachine.moveUnitToTarget ();
			}
		}
	}



	// PRIVATE FUNCTIONS

	private Vector3 pressDown(Vector3 mousePosition){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (mousePosition);
		if (Physics.Raycast (ray, out hit, 25.0f)) {				
			return hit.transform.position;
		} else {
			return new Vector3 (0.0f, 0.0f, 0.0f);
		}
	}
		
	private void selectPiece(Vector3 pos){

		Piece selected = Piece.GetPiece (pos.x, pos.z);

		boardManager.selectedPiece = selected;
		Debug.Log ("selected and highlighting");

		// Highlight all possible moves
		foreach(Vector3 possibleMove in boardManager.selectedPiece.possibleMoves()){
			if (Highlight.Exists (possibleMove.x, possibleMove.z)) {
				Highlight highlight = Highlight.GetHighlight (possibleMove.x, possibleMove.z);
				highlight.gameObject.SetActive (true);
			}

		}
	}

	private void movePiece(Vector3 pos){
		if (boardManager.selectedPiece) {
			Vector3 pieceCentre = new Vector3 (pos.x + 0.5f, 0, pos.z + 0.5f);
			if (boardManager.selectedPiece.possibleMoves ().Contains (pieceCentre)) {
				boardManager.selectedPiece.MoveTo (pos.x, pos.z);
			}
			boardManager.selectedPiece = null;
		}
	}


}