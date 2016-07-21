using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : BoardEntity {
	private static Dictionary<Vector3, Piece> pieces = new Dictionary<Vector3, Piece> ();
	public float movementSpeed;

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
		Vector3 startPosition = this.transform.position;

		while (transform.position != destination) {
			Vector3 move = Vector3.MoveTowards (transform.position, destination, movementSpeed * Time.deltaTime);
			transform.position = move;
			Debug.Log (move);
			yield return null;
		}
	}

	public List<Vector3> possibleMoves(){
		List<Vector3> list = new List<Vector3> ();

		int radius = 2;
		for (int x=-radius; x <= radius; x++) {
			for (int z = -radius; z <= radius; z++) {
				list.Add (new Vector3 (Centre ().x + x, 0, Centre ().z + z));
			}
		}

		return list;
	}

}