using UnityEngine;
using System.Collections;

public class BoardEntity : MonoBehaviour {
	private float TILE_OFFSET = 0.5f;

	// use the parent container to figure out the center of the object.
	// We use this so that we can play animations relative to the object's position, hence
	// why we have parent objects which hold the object position.
	public Vector3 Centre(){
		float x = Mathf.Floor(transform.parent.position.x) + TILE_OFFSET;
		float z = Mathf.Floor(transform.parent.position.z) + TILE_OFFSET;
		return new Vector3 (x, 0, z);
	}

	protected Vector3 normalize(float x, float z){
		x = Mathf.Floor (x) + 0.5f;
		z = Mathf.Floor (z) + 0.5f;

		return new Vector3 (x, 0, z);
	}


}