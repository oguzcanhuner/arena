using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : BoardEntity {
	private static Dictionary<Vector3, Tile> tiles = new Dictionary<Vector3, Tile> ();

	public static Tile GetTile(Vector3 centre){
		return tiles [centre];
	}

	public static Tile GetTile(float x, float z){
		x = Mathf.Floor (x) + 0.5f;
		z = Mathf.Floor (z) + 0.5f;

		return tiles [new Vector3 (x, 0, z)];
	}

	public static void AddTile(Tile tile){
		tiles.Add(tile.Centre(), tile);
	}

	public void Select(){
		Debug.Log ("selected tile");
		Highlight highlight = Highlight.GetHighlight (this.Centre ());
		highlight.Select ();
	}
}
