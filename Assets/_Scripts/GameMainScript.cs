using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMainScript : MonoBehaviour {

	public string player1Name;
	public string player2Name;
	public string player3Name;
	public string player4Name;

	public Text pressSpaceBarToStart;
	public Text howManyPlayers;
	public Button one;
	public Button two;
	public Button three;
	public Button four;
	public Text enterYourName;
	public Text selectQuadrant;
	public Text ready;

	public InputField playerNameField;

	public GameObject HUD;
	public Text spacebarToRollText;
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
		spacebarToRollText.active = false;
		HUD.active = false;
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
			pressSpaceBarToStart.active = true;
			if(Input.GetKeyDown(KeyCode.Space)){
				setGameState (numPlayersState);
				howManyPlayers.active = true;
			}
		}

		if(state == numPlayersState){
			print("In NumPlayersState");
		}

		if(state == playerNameState){
			if(playersCreated >= numPlayers) {
				if(numPlayers > 0){
					setGameState(readyState);
					
				}
			}
		}

		if(state == playerQuadState){
			//selectQuadrant.active = true;
		}

		if(state == readyState) {
			if(Input.GetKeyDown(KeyCode.Return)){
				setGameState(startGameAnimationState);
			}
		}
		if(state == startGameAnimationState){
			//do things here.  Interpolate camera position.
			bool ready = mainCam.smoothCameraTransition(playerManager.playerLocation());
			if(ready) {
				print("Ready to start turn");
				HUD.active = true;
				setGameState(playerRollDiceState);
			}
		}

		if(state == playerRollDiceState){
			//start of player's turn
		}

		if(state == playerChooseSpaceState){
			//transition to nextPlayerTransitionState if the player has no steps remaining.
		}

		if(state == showPlayerCardsState){
			//display the cards that the player has in hand
		}

		if(state == showPlayerStandingsState){
			//show player rankings
		}
	}

	public void setNumPlayers(int num){
		numPlayers = num;
		playerManager.createPlayerArray(num);
	}

	public void setPlayerQuad(int quad){

	}

	public void setGameState(int state){
		print("setGameState called: state is : "+ state);
		gameState = state;
		hideAllUI();

		if(state == startState){ pressSpaceBarToStart.active = true; print("intro hud should be active");}
		else if(state == numPlayersState){ howManyPlayers.active = true;}//set numPlayers
		else if(state == playerNameState){ enterYourName.active = true;}//create player.
		else if(state == playerQuadState){ tempName = playerNameField.text;  selectQuadrant.active = true; playersCreated++;}// increment playersCreated on transition. assign starting quad to player.  Disable quads that have been selected.
		else if(state == readyState){ ready.active = true;}

		else if(state == startGameAnimationState) {
			labScript.populateMaze();
			HUD.active = true;
			//updateHUD
			playerManager.setAvatarsActive();
		}

		else if(state == playerRollDiceState){ 
			//hud turned on
			//player name text updated
			//steps remaining = 0;
			//player standings turned on 
			//player cards turned on 
			//spacebar to roll text turned on

			spacebarToRollText.active = true;
		}
		else if(state == playerChooseSpaceState){ 
			//Update steps remaining on HUD
			//possible steps in range are highlighted on board.  
			spacebarToRollText.active = false;
		}
	}

	public void hideAllUI() {
		//spacebarToRollText.active = false;
		HUD.active = false;
		pressSpaceBarToStart.active = false;
		howManyPlayers.active = false;
		enterYourName.active = false;
		selectQuadrant.active = false;
		ready.active = false;
	}



	public void deportPlayer(int quad){
		playerManager.addPlayer(quad, tempName);
	}
}
