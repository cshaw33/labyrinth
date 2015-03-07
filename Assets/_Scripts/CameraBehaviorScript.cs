using UnityEngine;
using System.Collections;

public class CameraBehaviorScript : MonoBehaviour {

	public GameObject avatar; //set by PlayerManagerScript
	private Vector3 offset;

	public bool cameraFollowAvatar = false;  //toggled by GameMainScript state machine

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

		if(cameraFollowAvatar){
			transform.position = avatar.transform.position + offset;
		}
	}

	public bool smoothCameraTransition(Vector3 newLocation){
		//linear interpolation between current camera position and desired position.
		float mag = (newLocation + offset - transform.position).magnitude;
		print("magnitude is : "+ mag);
		if(mag <= 1.0f){
			//snap to position:
			transform.position = newLocation + offset;
			print("snapping to position");
			return true;
		}
		Vector3 transpose = newLocation + offset - transform.position;
		transpose.Normalize();
		transform.position = transform.position + transpose * 1;
		if((newLocation + offset - transform.position).magnitude < 1.0f){
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
