using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	private float TILE_OFFSET = 0.5f;

	public Vector3 Centre(){
		float x = Mathf.Floor(transform.position.x) + TILE_OFFSET;
		float z = Mathf.Floor(transform.position.z) + TILE_OFFSET;
		return new Vector3 (x, 0, z);
	}

	protected Vector3 normalize(float x, float z){
		x = Mathf.Floor (x) + 0.5f;
		z = Mathf.Floor (z) + 0.5f;

		return new Vector3 (x, transform.position.y, z);
	}


}