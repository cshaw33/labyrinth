﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvatarBehaviorScript : MonoBehaviour {


	public int NumSteps = 0;
	//public Script mazeScript;
	//public GUIText steps;

	public LabyrinthScript labScript;

	public Text UISteps;

	private char[] playerCards;

	// Use this for initialization
	void Start () {
		print("Start");
	}
	
	// Update is called once per frame
	void Update () {

		//move avatar if there are steps left.
		if(NumSteps > 0){
			if(Input.GetKeyDown(KeyCode.LeftArrow)){

				transform.position = tryToStep(new Vector3(-5, 0, 0));
			}
			if(Input.GetKeyDown(KeyCode.DownArrow)){

				transform.position = tryToStep(new Vector3(0,0,-5));
			}
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				transform.position = tryToStep(new Vector3(5,0,0));
			}
			if(Input.GetKeyDown(KeyCode.UpArrow)){
				transform.position = tryToStep(new Vector3(0,0,5));
			}
		}

		if(NumSteps <= 0){
			if(Input.GetKeyDown(KeyCode.Space)){
				NumSteps = Random.Range(1, 21);
				updateStepsText();
			}
		}

	}

	void updateStepsText() {
		//steps.text = "Steps: "+NumSteps;
		UISteps.text = "Steps: "+NumSteps;
	}

	Vector3 tryToStep(Vector3 direction) {
		Vector3 newPosition = transform.position + direction;
		bool canStep = labScript.checkIfCanStep(newPosition.x, newPosition.z);
		if(canStep){ 
			NumSteps--;
			updateStepsText();
			return newPosition;
		}
		else return transform.position;
	}
}