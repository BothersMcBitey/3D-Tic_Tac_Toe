using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour {

	public Vector3 GridPosition;
	public Color highlightColor;
	public Color ownerColor;
	public int ownerID;

	private Color defaultColor;

	// Use this for initialization
	void Start () {
		defaultColor = GetComponent<MeshRenderer> ().material.color;
		ownerID = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		HighlightCell();

		if (Input.GetMouseButtonDown(0)) {
			Debug.Log ("MouseClickOn: " + GridPosition);

			if (GameController.singleton.MoveMade(this, GridPosition)) {				
				ownerID = GameController.singleton.currentPlayer;
				ownerColor = GameController.singleton.playerColors [ownerID];
				ChangeCellColor (ownerColor);
			}
		}
	}

	void OnMouseExit(){
		UnHighlightCell ();
	}

	void HighlightCell(){
		if (ownerID == -1) {
			ChangeCellColor (highlightColor);
		} else {
			ChangeCellColor (MixColors (ownerColor, highlightColor));
		}
	}

	void UnHighlightCell(){
		if (ownerID == -1) {
			ChangeCellColor (defaultColor);
		} else {
			ChangeCellColor (ownerColor);
		}
	}

	void ChangeCellColor (Color color){
		GetComponent<MeshRenderer> ().material.color =  color;
	}

	Color MixColors(Color c1, Color c2){
		float r = (c1.r + c2.r) / 2;
		float g = (c1.g + c2.g) / 2;
		float b = (c1.b + c2.b) / 2;
		float a = (c1.a + c2.a) / 2;
		return new Color (r, g, b, a);
	}

	public bool isOwned(){
		return ownerID != -1;
	}
}
