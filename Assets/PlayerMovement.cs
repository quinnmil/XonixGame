using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float thrust;
	private string lastPress;
	public bool onTerritory = false;



	void Start (){

	}
	 
	void Update(){


		if (onTerritory)
			onTerritoryMove();
		else
			offTerritoryMove();
	   
 	}


 	public void offTerritoryMove(){

 		// do not let player go in the opposite direction,
 		// and do not player move in more than one direction at once
 		if (lastPress != "left"){
		    if (Input.GetKey(KeyCode.RightArrow)
		    	&& !Input.GetKey(KeyCode.UpArrow)  
		    	&& !Input.GetKey(KeyCode.DownArrow)){
		    	transform.Translate(0,0,thrust*Time.deltaTime);	 
		    	lastPress = "right"; 	
		    }
		}
		if (lastPress != "right"){
		    if (Input.GetKey(KeyCode.LeftArrow)
		    	&& !Input.GetKey(KeyCode.UpArrow)  
		    	&& !Input.GetKey(KeyCode.DownArrow)){
		    	transform.Translate(0,0,-thrust*Time.deltaTime);
		    	lastPress = "left";
		    }
		}

	    if (lastPress != "down"){
		    if (Input.GetKey(KeyCode.UpArrow)
		    	&& !Input.GetKey(KeyCode.LeftArrow)  
		    	&& !Input.GetKey(KeyCode.RightArrow)){
		    	transform.Translate(-thrust*Time.deltaTime,0,0);
		    	lastPress = "up";	    	
		    }
		}

		if (lastPress != "up"){
		    if (Input.GetKey(KeyCode.DownArrow)
		    	&& !Input.GetKey(KeyCode.LeftArrow)  
		    	&& !Input.GetKey(KeyCode.RightArrow)){
		    	transform.Translate(thrust*Time.deltaTime,0,0);
		    	lastPress = "down";
		    }
		}
	    //if a key press didn't happen in this frame,
	    // go the same direction last pressed
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


 	public void onTerritoryMove(){
 		
	    if (Input.GetKey(KeyCode.RightArrow)
	    	&& !Input.GetKey(KeyCode.UpArrow)  
	    	&& !Input.GetKey(KeyCode.DownArrow)){
	    	transform.Translate(0,0,thrust*Time.deltaTime);	  	
		}
	
	    if (Input.GetKey(KeyCode.LeftArrow)
	    	&& !Input.GetKey(KeyCode.UpArrow)  
	    	&& !Input.GetKey(KeyCode.DownArrow)){
	    	transform.Translate(0,0,-thrust*Time.deltaTime);
	    }

	    if (Input.GetKey(KeyCode.UpArrow)
	    	&& !Input.GetKey(KeyCode.LeftArrow)  
	    	&& !Input.GetKey(KeyCode.RightArrow)){
	    	transform.Translate(-thrust*Time.deltaTime,0,0);	    	
	    }

	    if (Input.GetKey(KeyCode.DownArrow)
	    	&& !Input.GetKey(KeyCode.LeftArrow)  
	    	&& !Input.GetKey(KeyCode.RightArrow)){
	    	transform.Translate(thrust*Time.deltaTime,0,0);

	    }

 	}

}
