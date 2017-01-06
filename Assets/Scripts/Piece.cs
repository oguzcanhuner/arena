using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : BoardEntity {
	private static Dictionary<Vector3, Piece> pieces = new Dictionary<Vector3, Piece> ();
	public float movementSpeed;
	public int movementRadius;
	public int attackRadius;

	public int hitpoints;
	public int attackValue;
	public TextMesh StatusText;
	public int team;

	Transform parent;

	Animator anim;

	void Start(){
		parent = this.transform.parent;
		anim = GetComponent<Animator> ();
	}

	public static Piece GetPiece(Vector3 centre){
		centre = new Vector3 (centre.x, 0, centre.z);
		return pieces [centre];
	}

	public static Piece GetPiece(float x, float z){
		
		return pieces [normalize (x, z)];
	}

	public static void AddPiece(Piece piece){
		pieces.Add(piece.Centre(), piece);
	}

	public static bool Exists(float x, float z){
		return pieces.ContainsKey (normalize(x,z));
	}

	new private static Vector3 normalize(float x, float z){
		x = Mathf.Floor (x) + 0.5f;
		z = Mathf.Floor (z) + 0.5f;

		return new Vector3 (x, 0, z);
	}

	public void MoveTo(float x, float z){

		Vector3 destination = new Vector3 (x, 0, z);
		
		Piece.pieces.Remove (this.Centre());
		Piece.pieces.Add(normalize(x, z), this);
		anim.SetBool("moving", true);
		this.transform.LookAt (destination);
		StartCoroutine( SmoothMove (new Vector3 (x, 0, z)));

	}

	private IEnumerator SmoothMove(Vector3 destination){
		

		while (parent.position != destination) {
			
			Vector3 move = Vector3.MoveTowards (parent.position, destination, movementSpeed * Time.deltaTime);
			parent.position = move;

			if (parent.position == destination){
				anim.SetBool ("moving", false);
			}

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

	public void attack(Vector3 target){
		transform.LookAt (target);
		anim.SetTrigger ("Attack");

		if (Piece.Exists (target.x, target.z)) {
			Piece targetPiece = Piece.GetPiece (target.x, target.z);
			targetPiece.takeDamage (this.attackValue);
			targetPiece.StatusText.text = this.attackValue.ToString();
			Animator textAnimator = targetPiece.StatusText.GetComponent<Animator> ();
			textAnimator.SetTrigger ("float text");

		}
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

	private void printPieces(){
		foreach (KeyValuePair<Vector3, Piece> piece in Piece.pieces) {
			Debug.Log (piece.Value.Centre());
		}
	}
}