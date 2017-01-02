using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Highlight : Unit {
	
	private static Dictionary<Vector3, Highlight> highlights = new Dictionary<Vector3, Highlight> ();
	private bool selected;

	public static Highlight GetHighlight(Vector3 centre){
		return highlights [centre];
	}

	public static Highlight GetHighlight(float x, float z){
		x = Mathf.Floor (x) + 0.5f;
		z = Mathf.Floor (z) + 0.5f;

		return highlights [new Vector3 (x, 0, z)];
	}

	public static bool Exists(float x, float z){
		return highlights.ContainsKey (normalize(x,z));
	}

	public static void AddHighlight(Highlight highlight){
		highlights.Add(highlight.Centre(), highlight);
	}

	public static void ClearHighlights(){
		foreach (var item in highlights) {
			Highlight highlight = item.Value;
			if (!highlight.selected) {
				highlight.gameObject.SetActive (false);
			}
		}
	}

	public static void UnselectAll(){
		foreach (var item in highlights) {
			Highlight highlight = item.Value;
			highlight.gameObject.SetActive (false);
		}
	}

	void Start(){
		gameObject.SetActive (false);
		selected = false;
	}

	public void Hover(){
		gameObject.SetActive (true);
	}

	public void Select(){
		selected = true;
	}

	public void UnSelect(){
		selected = false;
	}

	new private static Vector3 normalize(float x, float z){
		x = Mathf.Floor (x) + 0.5f;
		z = Mathf.Floor (z) + 0.5f;

		return new Vector3 (x, 0, z);
	}
}
