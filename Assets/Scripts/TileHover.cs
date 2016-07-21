using UnityEngine;
using System.Collections;

public class TileHover : MonoBehaviour {

	void Update(){
		//Debug.Log ("tile hover update");
		Highlight.ClearHighlights();

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if(hit.transform.CompareTag("Tile")){
				
				Vector3 pos = hit.transform.position;
				Tile tile = Tile.GetTile (pos.x, pos.z);
				tile.gameObject.GetComponent<Renderer>().material.color = new Color (10f, 10f, 10f);

			}
		}
	}

}
