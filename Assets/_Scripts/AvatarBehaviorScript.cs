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

		/*
		if(NumSteps <= 0){
			if(Input.GetKeyDown(KeyCode.Space)){
				NumSteps = Random.Range(1, 21);
				updateStepsText();
			}
		}
	*/
	}

	void updateStepsText() {
		//steps.text = "Steps: "+NumSteps;
		UISteps.text = "Steps: "+NumSteps;
	}

	public bool tryToStep(Vector3 direction, int stepsLeft) {
		Vector3 newPosition = transform.position + direction;
		bool canStep = labScript.checkIfCanStep(newPosition.x, newPosition.z, stepsLeft);
		if(canStep){ 
			return true;
		}
		else return false;
	}
}
