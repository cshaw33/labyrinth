using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public GameMainScript backend;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setNumPlayers(int numPlayers) {
		backend.numPlayers = numPlayers;
		backend.setGameState (2);
	}
}
