using UnityEngine;
using System.Collections;

public class CameraBehaviorScript : MonoBehaviour {

	public GameObject avatar;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		//if(avatar.active == true){
		//	transform.position = avatar.transform.position + offset;
			
		//}
		//else transform.position = offset;
	}

	public bool smoothCameraTransition(Vector3 newLocation){
		//linear interpolation between current camera position and desired position.
		if((transform.position - newLocation + offset).magnitude <= 4.0f){
			//snap to position:
			transform.position = newLocation + offset;
		}
		Vector3 transpose = newLocation + offset - transform.position;
		transpose.Normalize();
		transform.position = transform.position + transpose * 1;
		if((transform.position - newLocation + offset).magnitude < 1.0f){
			print("We're pretty damn close!");
			return true;
		}
		else return false;
	}


	public void printVector(Vector3 vector){
		print("x: "+vector.x+" y: "+vector.y+" z: "+vector.z);
	}
}
