using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text winner;

	public int currentPlayer;
	public int numPlayers;
	private bool hasPlayed = false;

	public Color[] playerColors;

	public static GameController singleton;
	private int[,,] cellStates;
	private Vector3[] winningMove;

	// Use this for initialization
	void Start () {
		singleton = this;
		Cursor.visible = true;
		currentPlayer = 0;
		numPlayers = playerColors.Length;
		cellStates = new int[3,3,3];
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++){
				for (int k = 0; k < 3; k++) {
					cellStates [i, j, k] = -1;
				}
			}
		}
		winningMove = new Vector3[3];
	}

	void LateUpdate () {
		if (hasPlayed) {
			if (isVictory ()) {
				Debug.Log ("win");
				winner.text = "Player " + currentPlayer + " wins!";
			} else {
				currentPlayer = (currentPlayer + 1) % numPlayers;
				hasPlayed = false;
			}
		}
	}		

	public bool MoveMade(CellBehaviour cell, Vector3 pos){
		Debug.Log ("Moving");
		Debug.Log ("v: " + cell.ownerID + ", r: " + cell.isOwned ());
		if (!cell.isOwned ()) {
			hasPlayed = true;
			Debug.Log("Moved");
			cellStates [(int)pos.x, (int)pos.y, (int)pos.z] = currentPlayer;
			Debug.Log (cellStates [(int)pos.x, (int)pos.y, (int)pos.z]);
			return true;
		} else {

			return false;
		}
	}

	bool isVictory(){
		//check verticals
		for (int x = 0; x < 3; x ++) {
			for (int z = 0; z < 3; z++){
				if (x != 1 || z != 1) {
					if (cellStates [x, 0, z] == currentPlayer) {
						if (cellStates [x, 1, z] == currentPlayer && cellStates [x, 2, z] == currentPlayer) {
							Debug.Log ("V win");
							winningMove [0] = new Vector3 (x, 0, z);
							winningMove [1] = new Vector3 (x, 1, z);
							winningMove [2] = new Vector3 (x, 2, z);
							return true;
						}
					}
				}
			}
		}
		//check corners horizontal
		for (int x = 0, z = 0; x < 3 && z< 3; x += 2, z += 2) {
			for(int y = 0; y < 3; y++){
				if (cellStates [x, y, z] == currentPlayer) {
					if (cellStates [(x+1)%3, y, z] == currentPlayer && cellStates [(x+2)%3, y, z] == currentPlayer) {
						winningMove [0] = new Vector3 (x, y, z);
						winningMove [1] = new Vector3 ((x+1)%3, y, z);
						winningMove [2] = new Vector3 ((x+2)%3, y, z);
						return true;
					} else if (cellStates [x, y, (z+1)%3] == currentPlayer && cellStates [x, y, (z+2)%3] == currentPlayer){
						winningMove [0] = new Vector3 (x, y, z);
						winningMove [1] = new Vector3 (x, y, (z+1)%3);
						winningMove [2] = new Vector3 (x, y, (z+2)%3);
						return true;
					}
				}
			}
		}
		//check corners diagonal


		//check centers horizontal
		for(int y = 0; y < 3; y+=2){
			if (cellStates [1, y, 1] == currentPlayer) {
				if (cellStates [0, y, 1] == currentPlayer && cellStates [2, y, 1] == currentPlayer) {
					winningMove [0] = new Vector3 (0, y, 1);
					winningMove [1] = new Vector3 (1, y, 1);
					winningMove [2] = new Vector3 (2, y, 1);
					return true;
				} else if (cellStates [1, y, 0]  == currentPlayer && cellStates [1, y, 2] == currentPlayer){
					winningMove [0] = new Vector3 (1, y, 0);
					winningMove [1] = new Vector3 (1, y, 1);
					winningMove [2] = new Vector3 (1, y, 2);
					return true;
				}
			}
		}
		return false;
	}
}
