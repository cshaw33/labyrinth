using UnityEngine;
using System.Collections;

public class PlayerManagerScript : MonoBehaviour {

	//each player has:
	// 	1. An avatar
	// 	2. A personalized HUD
	//  3. A personal deck of cards (hand)
	//  4. A collection of gems
	//  5. 

	public PlayerScript GreenPlayer;
	public PlayerScript PurplePlayer;
	public PlayerScript RedPlayer;
	public PlayerScript BluePlayer;

	public GameObject GreenAvatar;
	public GameObject PurpleAvatar;
	public GameObject RedAvatar;
	public GameObject BlueAvatar;

	public PlayerScript[] players;
	public int currentPlayerIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//called from input from "how many players" screen
	public void createPlayerArray(int i){
		//input parameter is literally useless now.
		players = new PlayerScript[4];

		for(int j = 0; j<4; j++){
			PlayerScript player = new PlayerScript(j);
			players[j] = player;
		}
	}

	public void addPlayer(int quad, string name){
		//PlayerScript player = new PlayerScript(quad);
		PlayerScript player;
		if(quad == 0){
			player = GreenPlayer;
		}
		else if(quad == 1){
			player = PurplePlayer;
		}
		else if(quad == 2){
			player = RedPlayer;
		}
		else if(quad == 3){
			player = BluePlayer;
		}
		else{
			return;
		}
		player.setPlayerName(name);
		player.setPlayerQuad(quad);
		player.setStartingLocation();
		print("Try to add player to array:");
		players[quad] = player;
		print("Did we succeed?");
	}

	public void nextPlayer(){
		currentPlayerIndex++;
		if(currentPlayerIndex >=4) currentPlayerIndex = 0;
	}

	public int playerTurn(){
		return currentPlayerIndex;
	}

	public Vector3 playerLocation(){
		if(currentPlayerIndex == 0) return GreenAvatar.transform.position;
		else if(currentPlayerIndex == 1) return PurpleAvatar.transform.position;
		else if(currentPlayerIndex == 2) return RedAvatar.transform.position;
		else if(currentPlayerIndex == 3) return BlueAvatar.transform.position;
		else return new Vector3(0,0,0);
	}

	public void setAvatarsActive(){
		GreenAvatar.active = true;
		PurpleAvatar.active = true;
		RedAvatar.active = true;
		BlueAvatar.active = true;
	}
}
