using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GateScript : MonoBehaviour {

	public Text gateText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setText(string s){
		if(s == "") gateText.text = "#";
		else gateText.text = s;
	}
}
