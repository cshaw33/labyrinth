using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManagerScript : MonoBehaviour {

	//each player has:
	// 	1. An avatar
	// 	2. A personalized HUD
	//  3. A personal deck of cards (hand)
	//  4. A collection of gems
	//  5. 

	public Text PlayerNameText;
	public Text StepsText;
	//reference to card UI element

	public PlayerScript GreenPlayer;
	public PlayerScript PurplePlayer;
	public PlayerScript RedPlayer;
	public PlayerScript BluePlayer;

	public PlayerScript currentPlayer;

	public GameObject GreenAvatar;
	public GameObject PurpleAvatar;
	public GameObject RedAvatar;
	public GameObject BlueAvatar;

	public GameObject currentAvatar;

	public PlayerScript[] players;
	public int currentPlayerIndex = 0;
	public int NumPlayers = 1;

	public int NumStepsLeft = 0;
	public CameraBehaviorScript mainCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initPlayerManager(){
		initCurrentPlayer();
		initStartingLocation();
	}

	public void initCurrentPlayer(){
		//this seems like it should look for first player in list, not just always green player
		currentPlayer = GreenPlayer;
		currentAvatar = GreenAvatar;
		mainCam.setAvatar(GreenAvatar);
	}

	public void initStartingLocation(){
		Vector3 startLocation;
		startLocation = new Vector3(0,0, 85); GreenAvatar.transform.position = startLocation;
		startLocation = new Vector3(85, 0, 0); PurpleAvatar.transform.position = startLocation;
		startLocation = new Vector3(0, 0, -85); RedAvatar.transform.position = startLocation;
		startLocation = new Vector3(-85, 0, 0); BlueAvatar.transform.position = startLocation;
	}

	//called from input from "how many players" screen
	public void createPlayerArray(int i){
		//input parameter is literally useless now.
		players = new PlayerScript[i];

		for(int j = 0; j<i; j++){
			//PlayerScript player = new PlayerScript(j);
			PlayerScript player = new PlayerScript(j);
			players[j] = player;
		}
		NumPlayers = i;
	}

	public void addPlayer(int quad, string name){
		//PlayerScript player = new PlayerScript(quad);
		PlayerScript player;
		GameObject avatar;
		if(quad == 0){
			player = GreenPlayer;
			avatar = GreenAvatar;
		}
		else if(quad == 1){
			player = PurplePlayer;
			avatar = PurpleAvatar;
		}
		else if(quad == 2){
			player = RedPlayer;
			avatar = RedAvatar;
		}
		else if(quad == 3){
			player = BluePlayer;
			avatar = BlueAvatar;
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
		if(currentPlayerIndex >= NumPlayers) currentPlayerIndex = 0;

		//maincam turn off avatar tracking
		//switch current player to next player
		//wait while we move camera to next player
		//turn avatar tracking back on
		mainCam.cameraFollowAvatar = false;
		setPlayerActive(currentPlayerIndex);
		bool cameraAtNextPlayer = false;
		while (!cameraAtNextPlayer) {
			cameraAtNextPlayer = mainCam.smoothCameraTransition(this.playerLocation (),  Quaternion.Euler(new Vector3(60, 0, 0)), 50 );
		}
		mainCam.cameraFollowAvatar = true;
		updateHUD();

		//update UI with new player name, number of steps, change Cards displayed when Cards are shown.  
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
		if(NumPlayers >=2) PurpleAvatar.active = true;
		if(NumPlayers >=3) RedAvatar.active = true;
		if(NumPlayers >=4) BlueAvatar.active = true;
	}

	public void setPlayerActive(int playerIndex){
		if(playerIndex == 0){
			currentPlayer = GreenPlayer;
			currentAvatar = GreenAvatar;

		}
		else if(playerIndex == 1){
			currentPlayer = PurplePlayer;
			currentAvatar = PurpleAvatar;
		}
		else if(playerIndex == 2){
			currentPlayer = RedPlayer;
			currentAvatar = RedAvatar;
		}
		else if(playerIndex == 3){
			currentPlayer = BluePlayer;
			currentAvatar = BlueAvatar;
		}
		mainCam.setAvatar(currentAvatar);
	}

	public int currentTryToStepInDirection(Vector3 direction){
		AvatarBehaviorScript av = currentAvatar.GetComponent<AvatarBehaviorScript>();
		bool success = av.tryToStep(direction, NumStepsLeft);
		if(success){
			NumStepsLeft--;
			updateStepsText();
			Vector3 newLocation = currentAvatar.transform.position + direction;
			currentAvatar.transform.position = newLocation;
		}

		return NumStepsLeft;
	}

	public int currentTryToStepLeft(){
		return currentTryToStepInDirection(new Vector3(-5, 0, 0));
	}

	public int currentTryToStepRight(){
		return currentTryToStepInDirection(new Vector3(5,0,0));
	}

	public int currentTryToStepUp(){
		return currentTryToStepInDirection(new Vector3(0,0,5));
	}

	public int currentTryToStepDown(){
		return currentTryToStepInDirection(new Vector3(0,0,-5));
	}

	public void currentRollDice(){
		NumStepsLeft = Random.Range(1, 21);
		updateStepsText();

	}

	public void updateHUD() {
		print("about to update player name text");
		updatePlayerNameText();
		print("about to update steps text");
		updateStepsText();
	}

	public void updatePlayerNameText(){
		print("there's a null something in here.");
		print("currentPlayer is: "+currentPlayer);
		print("currentPlayer.name is: "+currentPlayer.name);
		print("PlayerNameText.text is: "+PlayerNameText.text);
		PlayerNameText.text = currentPlayer.playerName;
	}

	public void updateStepsText() {
		StepsText.text  = "" + NumStepsLeft;
	}
}
