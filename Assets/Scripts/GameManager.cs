using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private BoardManager boardManager;
	private StateMachine stateMachine;
	public Text GamePhase;

	void Start(){
		boardManager = GameObject.Find ("Board").GetComponent<BoardManager> ();
		stateMachine = GameObject.Find ("Board").GetComponent<StateMachine> ();
		GamePhase.text = "Select a piece to move";
	}

	void Update(){
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

	// STATES

	private void waitingToMove(){
		
		Vector3 pos = pressDown (Input.mousePosition);
		Debug.Log (pos);
		if (Piece.Exists (pos.x, pos.z)) {
			selectPieceToMove (pos);
			stateMachine.selectUnitToMove ();
			GamePhase.text = "Select a highlighted square to move to";
		}// else do nothing

	}

	private void selectMoveTarget(){
		
		Highlight.ClearHighlights ();

		Vector3 pos = pressDown (Input.mousePosition);

		if (Piece.Exists (pos.x, pos.z)) {
			selectPieceToMove (pos);
			stateMachine.selectUnitToMove ();
			GamePhase.text = "Select a piece to move";
		} else {
			movePiece (pos);
			stateMachine.moveUnitToTarget ();
			GamePhase.text = "Select a player to attack with";
		}
	}

	private void waitingToAttack(){
		// attack phase code
		Vector3 pos = pressDown (Input.mousePosition);

		if (Piece.Exists (pos.x, pos.z)) {
			selectPieceToAttack (pos);
			stateMachine.selectUnitToAttack ();
			GamePhase.text = "Select a highlighted space to a\tttack";
		}// else do nothing
	}

	private void selectingAttackTarget(){
		Highlight.ClearHighlights ();
		Vector3 pos = pressDown (Input.mousePosition);

		attack (pos);
		stateMachine.attack ();
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
		Debug.Log ("selected and highlighting move options");

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
		Debug.Log ("selected and highlighting attack options");

		// Highlight all possible moves
		foreach(Vector3 possibleAttack in boardManager.selectedPiece.possibleAttacks()){
			if (Highlight.Exists (possibleAttack.x, possibleAttack.z)) {
				Highlight highlight = Highlight.GetHighlight (possibleAttack.x, possibleAttack.z);
				highlight.gameObject.SetActive (true);
			}
		}
	}

	private void movePiece(Vector3 pos){
		Vector3 pieceCentre = new Vector3 (pos.x + 0.5f, 0, pos.z + 0.5f);
		if (boardManager.selectedPiece.possibleMoves ().Contains (pieceCentre)) {
			boardManager.selectedPiece.MoveTo (pos.x, pos.z);
		}
		boardManager.selectedPiece = null;
	}

	private void attack(Vector3 pos){
		if (Piece.Exists (pos.x, pos.z)) {
			Piece target = Piece.GetPiece (pos.x, pos.z);
			Piece selected = boardManager.selectedPiece;
			target.takeDamage (selected.attackValue);
//			Animator anim = selected.GetComponent<Animator> ();
//			anim.Play ("Attack");

		}
		boardManager.selectedPiece = null;
	}
}