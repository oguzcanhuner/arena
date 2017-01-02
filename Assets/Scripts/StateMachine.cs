using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
	public string currentState;
	private Dictionary<StateTransition, string> stateTransitions;

	void Start (){
		// create a dictionary where the keys are an array of [State, Action] and the value is State.

		stateTransitions = new Dictionary<StateTransition, string>();

		stateTransitions[ new StateTransition("waiting_to_move", "select_unit_to_move")] = "selecting_move_target";
		stateTransitions[ new StateTransition("selecting_move_target", "select_unit_to_move")] = "selecting_move_target";
		stateTransitions[ new StateTransition("selecting_move_target", "move_unit_to_target")] = "waiting_to_attack";
		stateTransitions[ new StateTransition("selecting_move_target", "cancel_movement")] = "waiting_to_move";

		// initial state
		currentState = "waiting_to_move";
	}

	public void selectUnitToMove(){
		performEvent("select_unit_to_move");
	}
	public void moveUnitToTarget(){
		performEvent("move_unit_to_target");
	}


	private void performEvent(string e){
		StateTransition transition = new StateTransition (currentState, e);
		string newState = stateTransitions [transition];
		if (newState != null) {
			currentState = newState;
		}
	}

	private class StateTransition{
		public string initialState;
		public string action;

		public StateTransition(string initialState, string action){
			this.initialState = initialState;
			this.action = action;
		}


		// override the dictionary key comparison function
		public override int GetHashCode()
		{
			return 17 + 31 * initialState.GetHashCode() + 31 * action.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			StateTransition other = obj as StateTransition;
			return other != null && this.initialState == other.initialState && this.action == other.action;
		}
	}


	// Creste a new class which holds a transition
	// override the function which compares two dictionary keys, and implement it on the state transition class
	
}