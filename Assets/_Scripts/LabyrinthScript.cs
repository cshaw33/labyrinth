using UnityEngine;
using System.Collections;

public class LabyrinthScript : MonoBehaviour {

	public GameObject StarSpace;

	public GameObject Emerald;
	public GameObject Ruby;
	public GameObject Sapphire;
	public GameObject Amythyst;
	public GameObject Gate;


	private char[,] maze;

	//Symbols for maze:

	// p = path
	// g = red wall
	// v = blue wall
	// b = purple wall
	// r = green wall
	// s = start space
	// x = star tile (draw a card)
	// 3/4/5/6 = gate
	// t = stone
	// e = ruby
	// a = sapphire
	// h = amythyst
	// u = emerald

	//constructor
	public LabyrinthScript(){

	}

	// Use this for initialization
	void Start () {
		maze = new char[37, 37] {{'t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t'},
			{'t','p','p','p','g','3','p','p','p','p','x','p','g','p','p','4','g','p','s','p','p','v','p','p','6','x','p','x','p','p','p','p','p','p','p','p','t'},
			{'t','p','g','p','g','p','g','g','g','g','g','p','g','p','g','p','g','p','t','v','p','v','p','v','v','v','v','v','v','p','v','v','v','v','v','p','t'},
			{'t','p','g','p','g','p','p','p','p','p','g','2','p','p','g','p','g','p','t','v','x','p','p','p','4','p','p','p','p','p','v','3','p','p','p','p','t'},
			{'t','p','g','p','g','g','g','g','g','p','g','g','g','g','g','p','g','p','t','v','v','v','v','v','v','v','v','v','v','v','v','p','v','v','v','v','t'},
			{'t','p','g','3','p','p','3','x','g','p','g','x','p','p','p','p','g','6','t','x','p','p','p','v','p','p','p','v','x','p','p','p','v','p','p','3','t'},
			{'t','p','g','g','g','p','g','p','g','5','g','p','g','g','g','g','g','p','t','p','v','v','p','v','p','v','p','v','p','v','v','3','v','p','v','p','t'},
			{'t','p','p','p','g','p','g','p','g','p','g','p','g','p','3','x','g','p','t','p','v','p','p','v','p','v','p','p','p','p','p','x','v','p','v','p','t'},
			{'t','p','g','p','g','x','p','p','g','p','g','p','p','p','g','p','g','p','t','p','v','p','v','v','p','v','v','v','v','v','v','v','v','p','v','p','t'},
			{'t','x','g','p','g','g','g','p','g','e','g','g','g','p','g','x','g','x','t','p','v','p','4','p','p','v','p','a','p','p','5','p','p','p','v','p','t'},
			{'t','p','g','p','g','p','p','p','g','p','p','p','g','p','g','g','g','p','t','p','v','v','v','v','v','v','p','v','v','v','v','v','v','v','v','x','t'},
			{'t','x','g','p','g','p','g','g','g','g','g','p','g','p','p','p','g','p','t','p','p','p','p','p','5','p','p','v','p','p','p','x','v','2','p','p','t'},
			{'t','6','g','4','g','p','p','p','p','p','g','5','g','g','g','p','g','p','t','v','v','v','v','v','v','v','v','v','p','v','v','p','v','p','v','v','t'},
			{'t','p','g','p','g','g','g','g','g','p','g','p','g','p','p','p','g','4','t','x','p','2','p','p','v','p','p','p','p','p','v','p','v','p','p','p','t'},
			{'t','p','p','p','g','p','p','p','g','4','g','p','g','p','g','g','g','p','t','p','v','v','v','p','v','p','v','v','v','3','v','p','v','v','v','p','t'},
			{'t','g','g','p','g','p','g','p','p','p','g','p','g','2','g','p','p','p','t','p','p','p','v','p','p','p','v','x','p','x','v','p','p','p','p','4','t'},
			{'t','p','p','x','g','p','g','g','g','g','g','p','g','p','g','p','t','t','t','t','t','p','v','v','v','v','v','v','v','v','v','v','v','v','v','v','t'},
			{'t','p','g','g','g','x','p','p','p','p','p','p','g','x','p','p','t','t','t','t','t','p','p','4','p','p','p','x','p','p','p','6','p','p','p','p','t'},
			{'t','s','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','s','t'},
			{'t','p','p','p','p','6','p','p','p','x','p','p','p','4','p','p','t','t','t','t','t','p','p','x','r','p','p','p','p','p','p','x','r','r','r','p','t'},
			{'t','b','b','b','b','b','b','b','b','b','b','b','b','b','b','p','t','t','t','t','t','p','r','p','r','p','r','r','r','r','r','p','r','x','p','p','t'},
			{'t','4','p','p','p','p','b','x','p','x','b','p','p','p','b','p','p','p','t','p','p','p','r','2','r','p','r','p','p','p','r','p','r','p','r','r','t'},
			{'t','p','b','b','b','p','b','3','b','b','b','p','b','p','b','b','b','p','t','p','r','r','r','p','r','p','r','4','r','p','p','p','r','p','p','p','t'},
			{'t','p','p','p','b','p','b','p','p','p','p','p','b','p','p','2','p','x','t','4','r','p','p','p','r','p','r','p','r','r','r','r','r','4','r','6','t'},
			{'t','b','b','p','b','p','b','b','p','b','b','b','b','b','b','b','b','b','t','p','r','p','r','r','r','5','r','p','p','p','p','p','r','p','r','x','t'},
			{'t','p','p','2','b','x','p','p','p','b','p','p','5','p','p','p','p','p','t','p','r','p','p','p','r','p','r','r','r','r','r','p','r','p','r','p','t'},
			{'t','x','b','b','b','b','b','b','b','b','p','b','b','b','b','b','b','p','t','p','r','r','r','p','r','p','p','p','r','p','p','p','r','p','r','x','t'},
			{'t','p','b','p','p','p','5','p','p','h','p','b','p','p','4','p','b','p','t','x','r','x','r','p','r','r','r','u','r','p','r','r','r','p','r','p','t'},
			{'t','p','b','p','b','b','b','b','b','b','b','b','p','b','b','p','b','p','t','p','r','p','r','p','p','p','r','p','r','p','p','x','r','p','r','p','t'},
			{'t','p','b','p','b','x','p','p','p','p','p','b','p','b','p','p','b','p','t','p','r','x','3','p','r','p','r','p','r','p','r','p','r','p','p','p','t'},
			{'t','p','b','p','b','3','b','b','p','b','p','b','p','b','p','b','b','p','t','p','r','r','r','r','r','p','r','5','r','p','r','p','r','r','r','p','t'},
			{'t','3','p','p','b','p','p','p','x','b','p','p','p','b','p','p','p','x','t','6','r','p','p','p','p','x','r','p','r','x','3','p','p','3','r','p','t'},
			{'t','b','b','b','b','p','b','b','b','b','b','b','b','b','b','b','b','b','t','p','r','p','r','r','r','r','r','p','r','r','r','r','r','p','r','p','t'},
			{'t','p','p','p','p','3','b','p','p','p','p','p','4','p','p','p','x','b','t','p','r','p','r','p','p','2','r','p','p','p','p','p','r','p','r','p','t'},
			{'t','p','b','b','b','b','b','p','b','b','b','b','b','b','p','b','p','b','t','p','r','p','r','p','r','p','r','r','r','r','r','p','r','p','r','p','t'},
			{'t','p','p','p','p','p','p','p','p','x','p','x','6','p','p','b','p','p','s','p','r','4','p','p','r','p','x','p','p','p','p','3','r','p','p','p','t'},
			{'t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t','t'}
		};

		//populateMaze();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void populateMaze(){
		for(int i=0; i<37; i++) {
			for(int j=0; j<37; j++){
				char c = maze[i, j];
				if(c == 'x'){
					GameObject star = (GameObject) Instantiate(StarSpace, new Vector3(getXCoordinate(i), 0, getYCoordinate(j)), StarSpace.transform.rotation);
				}
				else if(c == 'u'){
					GameObject emerald = (GameObject) Instantiate(Emerald, new Vector3(getXCoordinate(i), 2.5f, getYCoordinate(j)), Emerald.transform.rotation);
				}
				else if(c == 'e'){
					GameObject ruby = (GameObject) Instantiate(Ruby, new Vector3(getXCoordinate(i), 2.5f, getYCoordinate(j)), Ruby.transform.rotation);
				}
				else if(c == 'h'){
					GameObject amythyst = (GameObject) Instantiate(Amythyst, new Vector3(getXCoordinate(i), 2.5f, getYCoordinate(j)), Amythyst.transform.rotation);
				}
				else if(c == 'a'){
					GameObject sapphire = (GameObject) Instantiate(Sapphire, new Vector3(getXCoordinate(i), 2.5f, getYCoordinate(j)), Sapphire.transform.rotation);
				}
				else if(c == '2'){
					GameObject gate = (GameObject) Instantiate(Gate, new Vector3(getXCoordinate(i), 0.0f, getYCoordinate(j)), Gate.transform.rotation);
					//set text to 2
				}
				else if(c == '3'){
					GameObject gate = (GameObject) Instantiate(Gate, new Vector3(getXCoordinate(i), 0.0f, getYCoordinate(j)), Gate.transform.rotation);
					//set text to 3
				}
				else if(c == '4'){
					GameObject gate = (GameObject) Instantiate(Gate, new Vector3(getXCoordinate(i), 0.0f, getYCoordinate(j)), Gate.transform.rotation);
					//set text to 4
				}
				else if(c == '5'){
					GameObject gate = (GameObject) Instantiate(Gate, new Vector3(getXCoordinate(i), 0.0f, getYCoordinate(j)), Gate.transform.rotation);
					//set text to 5
				}
				else if(c == '6'){
					GameObject gate = (GameObject) Instantiate(Gate, new Vector3(getXCoordinate(i), 0.0f, getYCoordinate(j)), Gate.transform.rotation);
					//set text to 6
				}
			}
		}
	}

	private float getXCoordinate(int xIndex) {
		//index of 18 = 0;
		//index of 0 = -90;
		//index of 36 = 90
		float xCoord = xIndex * 5.0f;
		xCoord = xCoord - 90.0f;
		return xCoord;
	}

	public float getYCoordinate(int yIndex) {
		float yCoord = yIndex * 5.0f;
		yCoord = yCoord - 90.0f;
		return yCoord;
	}

	private char getValue(float xPos, float yPos){
		int xInt = (int)xPos;
		int yInt = (int)yPos;

		xInt = xInt+90;
		yInt = yInt+90;

		xInt = xInt/5;
		yInt = yInt/5;
		print(maze[xInt, yInt]);
		return maze[xInt, yInt];
	}

	public bool checkIfCanStep(float x, float y) {
		char space = getValue(x, y);

		if(space == 'p'){
			return true;
		}
		if(space == 'x'){

		}

		if(space == 'p' || space == 'x' || space == 'e' || space == 'a' || space == 'h' || space == 'u'){
			return true;
		}
		if(space == '2'){
			return testRoll(2);
		}
		if(space == '3'){
			return testRoll(3);
		}
		if(space == '4'){
			return testRoll(4);
		}
		if(space == '5'){
			return testRoll(5);
		}
		if(space == '6'){
			return testRoll(6);
		}

		return false;
	}

	private bool testRoll(int num){
		return true;
	}
}
