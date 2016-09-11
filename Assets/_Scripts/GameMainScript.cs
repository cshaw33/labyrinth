using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMainScript : MonoBehaviour {

	public string player1Name;
	public string player2Name;
	public string player3Name;
	public string player4Name;

	public GameObject pressSpaceBarToStart;  //Text
	public GameObject howManyPlayers;  //Texts
	public Button one;
	public Button two;
	public Button three;
	public Button four;
	public GameObject enterYourName;  //Text
	public GameObject selectQuadrant; //Text
	public GameObject ready;  //Text

	public InputField playerNameField;

	public GameObject HUD;
	public GameObject introHud;
	public GameObject spacebarToRollText;  //Text
	public LabyrinthScript labScript;
	public PlayerManagerScript playerManager;
	public CameraBehaviorScript mainCam;

	public int gameState;

	public int playerTurn;

	public int startState;				//0
	public int numPlayersState;		//1
	public int playerNameState;		//2
	public int playerQuadState;		//3
	public int readyState;				//4
	public int startGameAnimationState;//5
	public int playerRollDiceState; 	//10
	public int playerChooseSpaceState; //11
	public int showPlayerCardsState; //12
	public int cardActionDefaultState; //120
	public int showPlayerStandingsState; //13
	public int nextPlayerTransitionState; //20

	public int numPlayers;
	public int playersCreated;

	public string tempName;

	// Use this for initialization
	void Start () {
		spacebarToRollText.SetActive(false);
		HUD.SetActive(false);
		//intro animation of some kind.
		//"Labyrinth: Press Space Bar to Start"
		//"How Many Players?" (select 1 2 3 or 4)
		//For each player:
		//		Player X: Enter Your Name: (text box goes here);
		//		PlayerName: Select a Quadrant: (rudimentary board Map shown, selected quadrants greyed.)
		//"Ready?" (Show player names with avatars). "(Press Return to Begin)"
		//Start game

		print("setting values");
		startState = 0;
		numPlayersState = 1;
		playerNameState = 2;
		playerQuadState = 3;
		readyState = 4;
		startGameAnimationState = 5;

		playerRollDiceState = 10;
		playerChooseSpaceState = 11;
		showPlayerCardsState = 12;

		/*
		 * Card Action State = 12x
		 * All card action states start with 12
		 * Default: no action for card, 120;
		 */

		cardActionDefaultState = 120;

		showPlayerStandingsState = 13;
		nextPlayerTransitionState = 20;

		playersCreated = 0;

		playerTurn = -1;

		print("done setting values?");
		setGameState(startState);


		//HUD.active = true;
	}
	
	// Update is called once per frame
	void Update () {


		introStateMachine(gameState);
	}

	private void introStateMachine(int state){
		if(state == startState){
			pressSpaceBarToStart.SetActive(true);
			if(Input.GetKeyDown(KeyCode.Space)){
				setGameState (numPlayersState);
				//howManyPlayers.SetActive(true);
				return;
			}
			return;
		}

		if(state == numPlayersState){
			//print("In NumPlayersState");

			//if "one" pressed, numPlayers = 1; etc.
			//Once number of players has been selected, go to next state
			//This action is handled by the button's events defined in the Unity editor

			return;

		}

		if(state == playerNameState){
			if(playersCreated >= numPlayers && numPlayers > 0) {
				if(numPlayers > 0){
					setGameState(readyState);
					
				}
			}
			return;
		}

		if(state == playerQuadState){
			//selectQuadrant.active = true;
			return;
		}

		if(state == readyState) {
			if(Input.GetKeyDown(KeyCode.Return)){
				setGameState(startGameAnimationState);
			}
			return;
		}
		if(state == startGameAnimationState){
			//do things here.  Interpolate camera position.
			bool ready = mainCam.smoothCameraTransition(playerManager.playerLocation(), Quaternion.Euler(new Vector3(60, 0, 0)), 50);
			print("Is ready? " + ready);
			if(ready) {
				print("Ready to start turn");
				HUD.SetActive(true);
				setGameState(playerRollDiceState);
			}
			return;
		}

		if(state == playerRollDiceState){
			//start of player's turn

			if(Input.GetKeyDown(KeyCode.Space)){
				playerManager.currentRollDice();

				setGameState(playerChooseSpaceState);
			}
			return;
		}

		if(state == playerChooseSpaceState){
			//transition to nextPlayerTransitionState if the player has no steps remaining.

			//if(NumSteps > 0){
			int steps = 1;

			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				steps = playerManager.currentTryToStepLeft();
			}
			if(Input.GetKeyDown(KeyCode.DownArrow)){
				steps = playerManager.currentTryToStepDown();
			}
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				steps = playerManager.currentTryToStepRight();
			}
			if(Input.GetKeyDown(KeyCode.UpArrow)){
				steps = playerManager.currentTryToStepUp();
			}

			if(steps <= 0){
				mainCam.cameraFollowAvatar = false;
				playerManager.nextPlayer();
				setGameState(nextPlayerTransitionState);
			}
			//}
			return;

		}

		if(state == showPlayerCardsState){
			//display the cards that the player has in hand
			return;
		}

		if(state == showPlayerStandingsState){
			//show player rankings
			return;
		}

		if (state == nextPlayerTransitionState) {
			bool cameraAtNextPlayer = false;
			while (!cameraAtNextPlayer) {
				cameraAtNextPlayer = mainCam.smoothCameraTransition(playerManager.playerLocation (),  Quaternion.Euler(new Vector3(60, 0, 0)), 50 );
			}
			mainCam.cameraFollowAvatar = true;
		}
	}

	public void setNumPlayers(int num){
		print ("we're setting the number of players here");
		numPlayers = num;
		playerManager.createPlayerArray(num);
	}

	public void setPlayerQuad(int quad){

	}

	public void setGameState(int state){
		print("setGameState called: state is : "+ state);
		gameState = state;
		hideAllUI();

		if(state == startState){ introHud.SetActive(true); pressSpaceBarToStart.SetActive(true); print("intro hud should be active");}
		else if(state == numPlayersState){
			print("HowManyPlayers UI is real?" );
			howManyPlayers.SetActive(true);
		}//set numPlayers
		else if(state == playerNameState){ enterYourName.SetActive(true);}//create player.
		else if(state == playerQuadState){ 
			tempName = playerNameField.text;  
			selectQuadrant.SetActive(true); 
			playersCreated++;
		}// increment playersCreated on transition. assign starting quad to player.  Disable quads that have been selected.
		else if(state == readyState){ ready.SetActive(true);}

		else if(state == startGameAnimationState) {
			labScript.populateMaze();
			HUD.SetActive(true);
			//updateHUD
			playerManager.setAvatarsActive();

			print("going to init current player");
			playerManager.initPlayerManager();
			print("have inited current player");
		}

		else if(state == playerRollDiceState){ 
			//hud turned on
			//player name text updated
			//steps remaining = 0;
			//player standings turned on 
			//player cards turned on 
			//spacebar to roll text turned on
			HUD.SetActive(true);
			spacebarToRollText.SetActive(true);
			playerManager.updatePlayerNameText();
			mainCam.setCameraFollowAvatar(true);
		}
		else if(state == playerChooseSpaceState){ 
			//Update steps remaining on HUD
			//possible steps in range are highlighted on board. 
			HUD.SetActive(true);
			spacebarToRollText.SetActive(false);
		}
	}

	public void hideAllUI() {
		//spacebarToRollText.active = false;
		HUD.SetActive(false);
		pressSpaceBarToStart.SetActive(false);
		howManyPlayers.SetActive(false);
		enterYourName.SetActive(false);
		selectQuadrant.SetActive(false);
		ready.SetActive(false);
	}



	public void deportPlayer(int quad){
		playerManager.addPlayer(quad, tempName);
	}
	
}
