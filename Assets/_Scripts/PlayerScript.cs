using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour{

	public char[] playerCards;
	public int playerIndex = -1;
	public int playerQuad = -1;
	public string playerName = "NAME";


	public int numSteps = 0; 

	// Use this for initialization
	void Start () {
	
	}

	public PlayerScript(int quad){
		//instantiate a new avatar for each player.
		setPlayerQuad (quad);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setPlayerIndex(int index){
		playerIndex = index;
		if(playerName == ""){
			playerName = ("Player " + index);
		}
	}

	public void setPlayerCards(char[] cards){
		playerCards = cards;
	}

	public char[] getPlayerCards(){
		return playerCards;
	}

	public void setPlayerQuad(int q){
		playerQuad = q;
	}

	public int getPlayerQuad(){
		return playerQuad;
	}

	public void setPlayerName(string name){
		playerName = name;
	}

	public string getPlayerName(){
		return playerName;
	}



	public void setStartingLocation(){
		//deprecated
	}














}
