using UnityEngine;
using System.Collections;

//Class to control the movement of the camera
//Controls :
// Mouse click -> moves camera to clicked point
//A/D arrows  -> rotates camera to the right/left
//right arrow  -> moves camera to the right
//left arrow   -> moves camera to the left
//up arrow	   -> moves camera front
//down arrow   -> moves camera back
//the 'd' key  -> moves camera down
//the 's' key  -> moves camera up
public class CameraControl : MonoBehaviour {
	
	public float moveSpeed = 0.2f;
	Vector3 newPosition;

	void Start () {
		newPosition = transform.position;
	}

	void Update()
	{
		//Moves camera to the clicked point
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				newPosition = hit.point;
				newPosition.y += 1f;
			}
		}

		transform.position = Vector3.Lerp (transform.position,newPosition,moveSpeed*Time.deltaTime);

		if(Input.GetKey(KeyCode.A))//rotates camera to the right
		{
			transform.Rotate(new Vector3(0,moveSpeed *10* Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.D))//rotates camera to the left
		{
			transform.Rotate(new Vector3(0,-moveSpeed *10* Time.deltaTime,0));
		}

		if(Input.GetKey(KeyCode.RightArrow))//moves camera to the right
		{
			newPosition = transform.position + new Vector3(moveSpeed * Time.deltaTime,0,0);
		}
		if(Input.GetKey(KeyCode.LeftArrow))//moves camera to the left
		{
			newPosition = transform.position + new Vector3(-moveSpeed * Time.deltaTime,0,0);
		}
		if(Input.GetKey(KeyCode.DownArrow))//moves camera back
		{
			newPosition = transform.position + new Vector3(0, 0,-moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.UpArrow))// moves the camera to the front
		{
			newPosition = transform.position + new Vector3(0, 0, moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D))//moves the camera down 
		{
			newPosition = transform.position + new Vector3(0, -moveSpeed * Time.deltaTime,0);
		}
		if(Input.GetKey(KeyCode.S))//moves camera up
		{
			newPosition = transform.position + new Vector3(0, moveSpeed * Time.deltaTime,0);
		}
	}
	
}

