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
    public bool leftWall;
    public bool rightWall;
    public bool upWall;
    public bool downWall;
    public bool ceiling;

	// Path will be a list of positions while the 
	public List<GameObject> pathObjects; 
	public GameObject Trail;
    Vector3 originalPos;



	void Start ()
    {

		rb = GetComponent<Rigidbody>();
		lastPosition = this.transform.position;
        originalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

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
        if (other.gameObject.CompareTag("LeftWall"))
        {
            leftWall = true;
        }
        if (other.gameObject.CompareTag("RightWall"))
        {
            rightWall = true;
        }
        if (other.gameObject.CompareTag("UpWall"))
        {
            upWall = true;
        }
        if (other.gameObject.CompareTag("DownWall"))
        {
            downWall = true;
        }
        if (other.gameObject.CompareTag("Enemy")){
            this.transform.position = originalPos;
            clearPath();
        }
    }

	void OnTriggerStay(Collider other)
    {
		if(other.gameObject.CompareTag ("Territory"))
        {
			onTerritory = true;
            clearPath();
		}
        if (other.gameObject.CompareTag("Territory"))
        {
            onTerritory = true;
        }
        if (other.gameObject.CompareTag("LeftWall"))
        {
            leftWall = true;
        }
        if (other.gameObject.CompareTag("RightWall"))
        {
            rightWall = true;
        }
        if (other.gameObject.CompareTag("UpWall"))
        {
            upWall = true;
        }
        if (other.gameObject.CompareTag("DownWall"))
        {
            downWall = true;
        }
        if (other.gameObject.CompareTag("Ceiling"))
        {
            ceiling = true;
        }
    }

	void OnTriggerExit(Collider other)
    {
		onTerritory = false;
        leftWall = false;
        downWall = false;
        upWall = false;
        rightWall = false;

	}


 	public void offTerritoryMove()
    {
        // do not let player go in the opposite direction,
        // and do not player move in more than one direction at once
        if (!rightWall)
        {  
            if (Input.GetKey(KeyCode.RightArrow)
                && !Input.GetKey(KeyCode.UpArrow)
                && !Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, 0, thrust * Time.deltaTime);
                lastPress = "right";
            }    
        }

        if (!leftWall)
        {

            if (Input.GetKey(KeyCode.LeftArrow)
                && !Input.GetKey(KeyCode.UpArrow)
                && !Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, 0, -thrust * Time.deltaTime);
                lastPress = "left";
            }
        }


        if (!upWall)
        {
            if (Input.GetKey(KeyCode.UpArrow)
                && !Input.GetKey(KeyCode.LeftArrow)
                && !Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(-thrust * Time.deltaTime, 0, 0);
                lastPress = "up";
            }
        }

        if (!downWall)
        {
            if (Input.GetKey(KeyCode.DownArrow)
                && !Input.GetKey(KeyCode.LeftArrow)
                && !Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(thrust * Time.deltaTime, 0, 0);
                lastPress = "down";
            }            
        }

        if (!ceiling)
        {
            if (Input.GetKey(KeyCode.Space)
                && !Input.GetKey(KeyCode.LeftArrow)
                && !Input.GetKey(KeyCode.RightArrow)
                && !Input.GetKey(KeyCode.UpArrow)
                && !Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, thrust * Time.deltaTime, 0);
                lastPress = "vertical";
            }
        }
            //if a key press didn't happen in this frame,
            // go the same direction last pressed
            if (lastPress == "right")
        {
            if (!rightWall)
            {
                transform.Translate(0, 0, thrust * Time.deltaTime);
            }
        }
        if (lastPress == "left")
        {
            if (!leftWall)
            {
                transform.Translate(0, 0, -thrust * Time.deltaTime);
            }
        }
        if (lastPress == "up")
        {
            if (!upWall)
            {
                transform.Translate(-thrust * Time.deltaTime, 0, 0);
            }
        }
        if (lastPress == "down")
        {
            if (!downWall)
            {
                transform.Translate(thrust * Time.deltaTime, 0, 0);
            }
            
        }
        if (lastPress == "vertical")
        {
            if (!ceiling)
            {
                transform.Translate(0, thrust * Time.deltaTime, 0);
            }

        }


        // creats objects behind moving player. These objects will have colliders
        // and be used to register impacts. 
        float dist = Vector3.Distance(this.transform.position, lastPosition);
        // as the player moves, add(invisible) game objects along path
        // and add player coordinates to path. 
        if (dist > .2)
        {
            GameObject lineObj = Instantiate(trace, lastPosition, Quaternion.identity);
            pathObjects.Add(lineObj);
            lastPosition = this.transform.position;
            //yield return new WaitForFixedUpdate();
        }







    }

    public void clearPath(){
        /* removes path from board */
        // for each object in pathObjects 
        foreach (var obj in pathObjects){
            Destroy(obj);
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

        lastPress = "";
	    if (Input.GetKey(KeyCode.RightArrow)
	    	&& !Input.GetKey(KeyCode.UpArrow)  
	    	&& !Input.GetKey(KeyCode.DownArrow)){
            if (!rightWall)
            {
                transform.Translate(0, 0, thrust * Time.deltaTime);
            }
		}
	
	    if (Input.GetKey(KeyCode.LeftArrow)
	    	&& !Input.GetKey(KeyCode.UpArrow)  
	    	&& !Input.GetKey(KeyCode.DownArrow)){
            if (!leftWall)
            {
                transform.Translate(0, 0, -thrust * Time.deltaTime);
            }
	    }

	    if (Input.GetKey(KeyCode.UpArrow)
	    	&& !Input.GetKey(KeyCode.LeftArrow)  
	    	&& !Input.GetKey(KeyCode.RightArrow)){
            if (!upWall)
            {
                transform.Translate(-thrust * Time.deltaTime, 0, 0);
            }	    	
	    }

	    if (Input.GetKey(KeyCode.DownArrow)
	    	&& !Input.GetKey(KeyCode.LeftArrow)  
	    	&& !Input.GetKey(KeyCode.RightArrow)){
            if (!downWall)
            {
                transform.Translate(thrust * Time.deltaTime, 0, 0);
            }
	    	

	    }

 	}


}
