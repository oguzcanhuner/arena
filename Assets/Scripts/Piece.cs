using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : Unit {
	private static Dictionary<Vector3, Piece> pieces = new Dictionary<Vector3, Piece> ();
	public float movementSpeed;
	public int movementRadius;
	public int attackRadius;

	public int hitpoints;
	public int attackValue;

	public static Piece GetPiece(Vector3 centre){
		centre = new Vector3 (centre.x, 0, centre.z);
		return pieces [centre];
	}

	public static Piece GetPiece(float x, float z){
		return pieces [normalize(x,z)];
	}

	public static void AddPiece(Piece piece){
		pieces.Add(piece.Centre(), piece);
	}

	public static bool Exists(float x, float z){
		return pieces.ContainsKey (normalize(x,z));
	}

	private static Vector3 normalize(float x, float z){
		x = Mathf.Floor (x) + 0.5f;
		z = Mathf.Floor (z) + 0.5f;

		return new Vector3 (x, 0, z);
	}

	public void MoveTo(float x, float z){
		StartCoroutine( SmoothMove (new Vector3 (x, transform.position.y, z)));
		Piece.pieces.Remove (this.Centre());
		Piece.pieces.Add (normalize (x, z), this);



	}

	private IEnumerator SmoothMove(Vector3 destination){
		while (transform.position != destination) {
			Vector3 move = Vector3.MoveTowards (transform.position, destination, movementSpeed * Time.deltaTime);
			transform.position = move;
			yield return null;
		}
	}

	public List<Vector3> possibleMoves(){
		List<Vector3> list = new List<Vector3> ();

		for (int x=-movementRadius; x <= movementRadius; x++) {
			for (int z = -movementRadius; z <= movementRadius; z++) {
				list.Add (new Vector3 (Centre ().x + x, 0, Centre ().z + z));
			}
		}

		return list;
	}

	public List<Vector3> possibleAttacks(){
		List<Vector3> list = new List<Vector3> ();

		for (int x=-attackRadius; x <= attackRadius; x++) {
			for (int z = -attackRadius; z <= attackRadius; z++) {
				list.Add (new Vector3 (Centre ().x + x, 0, Centre ().z + z));
			}
		}

		return list;
	}

	public void takeDamage(int amount){
		this.hitpoints -= amount;
		Debug.Log ("Take damage: " + amount.ToString ());
		Debug.Log ("New health: " + this.hitpoints.ToString ());
		if (this.hitpoints <= 0) {
			pieces.Remove (this.Centre());
			Destroy (gameObject);
		}
	}

}