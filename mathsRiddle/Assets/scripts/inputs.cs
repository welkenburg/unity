using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Move{
	free,
	restrained
}

public class inputs : MonoBehaviour
{
	public Move moveType;
	public Space scene;
	public float cubeSpeed;
	public float maxAnimationTime = 1.0f;

	private float startAnimationType;

	private bool moving = false;
	private bool pressed = false;

	private Vector3 prevPos;
	private Vector3 movment;

    void Update()
    {
    	//mouse was pressed , initialization of variables
    	if(Input.GetMouseButtonDown(0)) {
    		pressed = true;
    		moving = true;
    		prevPos = Input.mousePosition;
    	}

    	//mouse was released , beginning of animation
    	if(Input.GetMouseButtonUp(0)){
			pressed = false;
			startAnimationType = Time.time;
		}
		
		//free move option
		if(moveType == Move.free){
			//the cube follows the mouse
			if(pressed){
				movment = Input.mousePosition - prevPos;
				movment = new Vector3(movment.y, -movment.x, 0);

				transform.Rotate(movment * cubeSpeed * Time.deltaTime, scene);
				prevPos = Input.mousePosition;
			}

			//the cube was released , moving slower and slower (animation)
			if(moving && !pressed){
				float elapsedTime = Time.time - startAnimationType;
				float buff = 1 - elapsedTime / maxAnimationTime;
				transform.Rotate(movment * cubeSpeed * Time.deltaTime * buff, scene);

				if(elapsedTime > maxAnimationTime){
					moving = false;
				}
			}

		}

		//restrained move option
		if(moveType == Move.restrained){

			//the cube follows the mouse
			if(pressed){
				movment = Input.mousePosition - prevPos;
				movment = new Vector3(movment.y, -movment.x, 0);

				transform.Rotate(movment * cubeSpeed * Time.deltaTime, scene);
				prevPos = Input.mousePosition;
			}

			//the cube was released , turn to the nearest face
			if(moving && !pressed){
				float elapsedTime = Time.time - startAnimationType;

				/*do stuff
				movment = new Vector3(Mathf.Round(movment.x / 90)*90,Mathf.Round(movment.y / 90)*90,0);
				transform.Rotate(movment * cubeSpeed * Time.deltaTime, scene);*/


				if(elapsedTime > maxAnimationTime){
					moving = false;
				}
			}
		}
    }
}