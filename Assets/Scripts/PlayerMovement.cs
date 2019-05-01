using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public GameObject trace;
	public float thrust;
	public string lastPress;
	public bool onTerritory = false;
	private Rigidbody rb;
	private Vector3 lastPosition;

	// Path will be a list of positions while the 
	public List<Transform> path; 
	public GameObject Trail;



	void Start ()
    {

		rb = GetComponent<Rigidbody>();
		lastPosition = this.transform.position;

	}
	 


    void Update()
    {
        if (onTerritory)
        {
            onTerritoryMove();
        }
        else
        {
            offTerritoryMove();
        }
    }

    void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.CompareTag ("Territory"))
        {
			onTerritory = true;
		}
	}

	void OnTriggerStay(Collider other)
    {
		if(other.gameObject.CompareTag ("Territory"))
        {
			onTerritory = true;
		}
	}

	void OnTriggerExit(Collider other)
    {
		onTerritory = false;

	}


 	public void offTerritoryMove()
    {
 		// do not let player go in the opposite direction,
 		// and do not player move in more than one direction at once
 		if (lastPress != "left")
        {
		    if (Input.GetKey(KeyCode.RightArrow)
		    	&& !Input.GetKey(KeyCode.UpArrow)  
		    	&& !Input.GetKey(KeyCode.DownArrow)){
		    	transform.Translate(0,0,thrust * Time.deltaTime);	 
		    	lastPress = "right"; 	
		    }
		}

		if (lastPress != "right")
        {
		    if (Input.GetKey(KeyCode.LeftArrow)
		    	&& !Input.GetKey(KeyCode.UpArrow)  
		    	&& !Input.GetKey(KeyCode.DownArrow)){
		    	transform.Translate(0,0,-thrust * Time.deltaTime);
		    	lastPress = "left";
		    }
		}

	    if (lastPress != "down")
        {
		    if (Input.GetKey(KeyCode.UpArrow)
		    	&& !Input.GetKey(KeyCode.LeftArrow)  
		    	&& !Input.GetKey(KeyCode.RightArrow)){
		    	transform.Translate(-thrust * Time.deltaTime, 0,0);
		    	lastPress = "up";	    	
		    }
		}

		if (lastPress != "up")
        {
		    if (Input.GetKey(KeyCode.DownArrow)
		    	&& !Input.GetKey(KeyCode.LeftArrow)  
		    	&& !Input.GetKey(KeyCode.RightArrow)){
		    	transform.Translate(thrust * Time.deltaTime, 0,0);
		    	lastPress = "down";
		    }
		}
        //if a key press didn't happen in this frame,
        // go the same direction last pressed
        if (lastPress == "right")
        {
            transform.Translate(0, 0, thrust*Time.deltaTime);
        }
        if (lastPress == "left")
        {
            transform.Translate(0, 0, -thrust * Time.deltaTime);
        }
        if (lastPress == "up")
        {
            transform.Translate(-thrust * Time.deltaTime, 0, 0);
        }
        if (lastPress == "down")
        {
            transform.Translate(thrust * Time.deltaTime, 0, 0);
        }


        // creats objects behind moving player. These objects will have colliders
        // and be used to register impacts. 
        float dist = Vector3.Distance(this.transform.position, lastPosition);
        // as the player moves, add(invisible) game objects along path
        // and add player coordinates to path. 
        if (dist > .2)
        {
            GameObject lineObj = Instantiate(trace, lastPosition, Quaternion.identity);
            path.Add(this.transform);
            lastPosition = this.transform.position;
            //yield return new WaitForFixedUpdate();
        }







    }




	public void fill(List<Transform> path){
		// fills in territory given list of player positions
		for (int i = 0 ; i< path.Count; i++){
			// not sure what do to here. need to find which (if any) side of the 
			// box contains the enemy, and fill in the other section. 
			float x = path[i].position.x; 
			float z = path[i].position.z;
		}
    }


    public void onTerritoryMove()
    {     
		// if playing is coming back from non-territory
		if (path.Count > 0) {
			fill(path);
			path.Clear();
		}

        lastPress = "";
	    if (Input.GetKey(KeyCode.RightArrow)
	    	&& !Input.GetKey(KeyCode.UpArrow)  
	    	&& !Input.GetKey(KeyCode.DownArrow)){
	    	transform.Translate(0,0,thrust * Time.deltaTime);	  	
		}
	
	    if (Input.GetKey(KeyCode.LeftArrow)
	    	&& !Input.GetKey(KeyCode.UpArrow)  
	    	&& !Input.GetKey(KeyCode.DownArrow)){
	    	transform.Translate(0,0,-thrust * Time.deltaTime);
	    }

	    if (Input.GetKey(KeyCode.UpArrow)
	    	&& !Input.GetKey(KeyCode.LeftArrow)  
	    	&& !Input.GetKey(KeyCode.RightArrow)){
	    	transform.Translate(-thrust * Time.deltaTime, 0,0);	    	
	    }

	    if (Input.GetKey(KeyCode.DownArrow)
	    	&& !Input.GetKey(KeyCode.LeftArrow)  
	    	&& !Input.GetKey(KeyCode.RightArrow)){
	    	transform.Translate(thrust * Time.deltaTime, 0,0);

	    }

 	}


}
