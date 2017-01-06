using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private BoardManager boardManager;
	private StateMachine stateMachine;
	public Text PlayerTurn;
	public Text GamePhase;
	private int teamTurn;


	void Start(){
		boardManager = GameObject.Find ("Board").GetComponent<BoardManager> ();
		stateMachine = GameObject.Find ("Board").GetComponent<StateMachine> ();
		teamTurn = 1;
		PlayerTurn.text = "PLAYER " + teamTurn.ToString();
	}

	void Update(){
		Debug.Log (stateMachine.currentState);
		if (Input.GetMouseButtonDown (0)) {
			switch (stateMachine.currentState) {
			case "waiting_to_move":
				waitingToMove ();
				break;
			case "selecting_move_target":
				selectMoveTarget ();
				break;
			case "waiting_to_attack":
				waitingToAttack ();	
				break;
			case "selecting_attack_target":
				selectingAttackTarget ();	
				break;
			}
		}
	}

	public void skipPhase(){
		if (stateMachine.currentState == "waiting_to_attack") {
			switchTurn ();
		}
		stateMachine.skipPhase ();
	}

	// STATES

	private void waitingToMove(){
		
		Vector3 pos = pressDown (Input.mousePosition);
		if (Piece.Exists (pos.x, pos.z)) {
			selectPieceToMove (pos);
			stateMachine.selectUnitToMove ();
		}// else do nothing

	}

	private void selectMoveTarget(){
		
		Highlight.ClearHighlights ();

		Vector3 pos = pressDown (Input.mousePosition);

		if (Piece.Exists (pos.x, pos.z)) {
			selectPieceToMove (pos);
			stateMachine.selectUnitToMove ();
		} else {
			if (movePiece (pos)) {
				stateMachine.moveUnitToTarget ();
				boardManager.selectedPiece = null;
			}

		}
	}

	private void waitingToAttack(){
		// attack phase code
		Vector3 pos = pressDown (Input.mousePosition);

		if (Piece.Exists (pos.x, pos.z)) {
			selectPieceToAttack (pos);
			stateMachine.selectUnitToAttack ();
		}// else do nothing
	}

	private void selectingAttackTarget(){
		Highlight.ClearHighlights ();
		Vector3 pos = pressDown (Input.mousePosition);

		if (attack (pos)) {
			stateMachine.attack ();
			switchTurn ();
			boardManager.selectedPiece = null;
		} else {
			stateMachine.cancelAttack ();
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
		
	private void selectPieceToMove(Vector3 pos){

		Piece selected = Piece.GetPiece (pos.x, pos.z);

		boardManager.selectedPiece = selected;

		// Highlight all possible moves
		foreach(Vector3 possibleMove in boardManager.selectedPiece.possibleMoves()){
			if (Highlight.Exists (possibleMove.x, possibleMove.z)) {
				Highlight highlight = Highlight.GetHighlight (possibleMove.x, possibleMove.z);
				highlight.gameObject.SetActive (true);
			}

		}
	}

	private void selectPieceToAttack(Vector3 pos){

		Piece selected = Piece.GetPiece (pos.x, pos.z);

		boardManager.selectedPiece = selected;

		// Highlight all possible moves
		foreach(Vector3 possibleAttack in boardManager.selectedPiece.possibleAttacks()){
			if (Highlight.Exists (possibleAttack.x, possibleAttack.z)) {
				Highlight highlight = Highlight.GetHighlight (possibleAttack.x, possibleAttack.z);
				highlight.gameObject.SetActive (true);
			}
		}
	}

	private bool movePiece(Vector3 pos){
		Vector3 pieceCentre = new Vector3 (pos.x + 0.5f, 0, pos.z + 0.5f);
		if (boardManager.selectedPiece.possibleMoves ().Contains (pieceCentre)) {
			boardManager.selectedPiece.MoveTo (pos.x, pos.z);
			return true;

		} else {
			return false;
		}

	}

	private bool attack(Vector3 target){
		Vector3 pieceCentre = new Vector3 (target.x + 0.5f, 0, target.z + 0.5f);

		Piece selected = boardManager.selectedPiece;

		if (selected.possibleAttacks ().Contains (pieceCentre)) {
			selected.attack (target);
			return true;
		} else {
			return false;
		}
		
	}

	private void switchTurn(){
		if (teamTurn == 1) {
			teamTurn = 2;
		} else {
			teamTurn = 1;
		}

		PlayerTurn.text = "PLAYER " + teamTurn.ToString();
	}
}