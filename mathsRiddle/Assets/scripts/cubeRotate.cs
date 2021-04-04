using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum Move{
	free,
	restrained
}*/

public class Direction
{
	public Vector3 up = 	new Vector3( 90f,  0f, 0f);
	public Vector3 down = 	new Vector3(-90f,  0f, 0f);
	public Vector3 left = 	new Vector3(  0f,-90f, 0f);
	public Vector3 right = 	new Vector3(  0f, 90f, 0f);
}

public class cubeRotate : MonoBehaviour
{

	//public
	public Space scene;
	public Move type;
	public Vector3 lastPosition;
	public float minMovment;
	public float CubeSpeed;
	public int animationFrames;

	//private
	private bool pressed;
	private Vector3 DeltaDistance;
	private int frameCount;
	private Vector3 rotation;
	private bool turning;
	
	//init lastPosition
    void Start()
    {
        lastPosition = Input.mousePosition;
    }

    void Update()
    {
    	//key events
    	if(Input.GetMouseButtonDown(0)) {
    		pressed = true;
    		frameCount = 0;
    	}
		if(Input.GetMouseButtonUp(0)){
			pressed = false;
			frameCount = 0;
		}

		//rotation
		if(pressed && type == Move.free){
			DeltaDistance = Input.mousePosition - lastPosition;
			rotation  = new Vector3(DeltaDistance.y, -DeltaDistance.x, 0);
			transform.Rotate(rotation * CubeSpeed * Time.deltaTime, scene);
		}

		if(pressed && type == Move.restrained){
			if(frameCount == 1){
				DeltaDistance = Input.mousePosition - lastPosition;
				if(DeltaDistance.x > DeltaDistance.y && DeltaDistance.x > minMovment){
					if(DeltaDistance.x > 0) rotation =  new Vector3(  0f, 45f, 0f);
					if(DeltaDistance.x < 0) rotation =  new Vector3(  0f,-45f, 0f);
				}
				if(DeltaDistance.y > DeltaDistance.x && DeltaDistance.y > minMovment){
					if(DeltaDistance.y > 0) rotation =  new Vector3( 45f, 0f, 0f);
					if(DeltaDistance.y < 0) rotation =  new Vector3(-45f, 0f, 0f);
				}
			}
			if(frameCount >= 1 && frameCount < animationFrames){
				turning = true;
			}
		}


		if(!pressed && frameCount < animationFrames && type == Move.free){
			transform.Rotate(rotation * CubeSpeed * Time.deltaTime, scene);
		}

		if(turning){
			transform.Rotate(rotation / animationFrames, scene);
		}

		

		//update lastPosition
		lastPosition = Input.mousePosition;
		frameCount += 1;
    }
}
