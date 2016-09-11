using UnityEngine;
using System.Collections;

public class CameraBehaviorScript : MonoBehaviour {

	public GameObject avatar; //set by PlayerManagerScript
	private Vector3 offset;
	private Vector3 avatarOffset;

	public bool cameraFollowAvatar = false;  //toggled by GameMainScript state machine

	// Use this for initialization
	void Start () {
		offset = transform.position;
		avatarOffset = new Vector3 (0, 30, -15);
	}

	// Update is called once per frame
	void LateUpdate () {

		//if(avatar.active == true){
		//	transform.position = avatar.transform.position + offset;
			
		//}
		//else transform.position = offset;

		if(cameraFollowAvatar){
			transform.position = avatar.transform.position + avatarOffset;

		}
	}

	public bool smoothCameraTransition(Vector3 newLocation, Quaternion newRotation, int numIterationsLeft){
		//linear interpolation between current camera position and desired position.

		//Take the number of iterations left
		//divide the distance by the number of iternations
		//divide the number of degrees to rotate in each direction by the number of iterations
		//move and rotate by that much 
		//decrement numIterationsLeft




		float mag = (newLocation + avatarOffset - transform.position).magnitude;
		print("magnitude is : "+ mag);
		if(mag <= 1.0f){
			//snap to position:
			transform.position = newLocation + avatarOffset;
			print("snapping to position");
			return true;
		}


		Vector3 transpose = newLocation + avatarOffset - transform.position;
		transpose.Normalize();
		transform.position = transform.position + transpose * 1;
		if((newLocation + avatarOffset - transform.position).magnitude < 1.0f){
			print("We're pretty damn close!");
			return true;
		}
		else return false;
	}


	public void printVector(Vector3 vector){
		print("x: "+vector.x+" y: "+vector.y+" z: "+vector.z);
	}

	public void setAvatar(GameObject av){
		this.avatar = av;
	}

	public void setCameraFollowAvatar(bool b){
		cameraFollowAvatar = b;
	}
}
