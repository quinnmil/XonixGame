using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float thrust;
	private string lastPress;



	void Start (){

	}
	 
	void Update(){ 
	    if (Input.GetKey(KeyCode.RightArrow)
	    	&& !Input.GetKey(KeyCode.UpArrow)  
	    	&& !Input.GetKey(KeyCode.DownArrow)){
	    	transform.Translate(0,0,thrust*Time.deltaTime);	 
	    	lastPress = "right"; 	
	    }
	    if (Input.GetKey(KeyCode.LeftArrow)
	    	&& !Input.GetKey(KeyCode.UpArrow)  
	    	&& !Input.GetKey(KeyCode.DownArrow)){
	    	transform.Translate(0,0,-thrust*Time.deltaTime);
	    	lastPress = "left";
	    }

	    if (Input.GetKey(KeyCode.UpArrow)
	    	&& !Input.GetKey(KeyCode.LeftArrow)  
	    	&& !Input.GetKey(KeyCode.RightArrow)){
	    	transform.Translate(-thrust*Time.deltaTime,0,0);
	    	lastPress = "up";	    	
	    }

	    if (Input.GetKey(KeyCode.DownArrow)
	    	&& !Input.GetKey(KeyCode.LeftArrow)  
	    	&& !Input.GetKey(KeyCode.RightArrow)){
	    	transform.Translate(thrust*Time.deltaTime,0,0);
	    	lastPress = "down";
	    }
	    if (lastPress == "right"){
	    	transform.Translate(0,0,thrust*Time.deltaTime);
	    }
	    if (lastPress == "left"){
	    	transform.Translate(0,0,-thrust*Time.deltaTime);
	    }
	    if (lastPress == "up"){
	    	transform.Translate(-thrust*Time.deltaTime,0,0);
	    }
	    if (lastPress == "down"){
	    	transform.Translate(thrust*Time.deltaTime,0,0);
	    }
	   
 	}

}