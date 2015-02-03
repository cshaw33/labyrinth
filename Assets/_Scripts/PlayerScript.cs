using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour{

	public char[] playerCards;
	public int playerIndex = -1;
	public int playerQuad = -1;
	public string playerName = "";
	public AvatarBehaviorScript avatar;

	// Use this for initialization
	void Start () {
	
	}

	public PlayerScript(int quad){
		//instantiate a new avatar for each player.
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

	public void setPlayerQuad(int q){
		playerQuad = q;
	}

	public void setPlayerName(string name){
		playerName = name;
	}

	public void setStartingLocation(){
		Vector3 startLocation;
		if(playerQuad == 0){
			startLocation = new Vector3(0,0, 85);
		}
		else if(playerQuad == 1){
			startLocation = new Vector3(85, 0, 0);
		}
		else if(playerQuad == 2){
			startLocation = new Vector3(0, 0, -85);
		}
		else if(playerQuad == 3){
			startLocation = new Vector3(-85, 0, 0);
		}
		else{
			//print("cannot set start location of player without a location");
			return;
		}

		avatar.transform.position = startLocation;
	}














}
