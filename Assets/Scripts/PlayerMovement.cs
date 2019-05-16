using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject trace;
    public float thrust;
    public string lastPress;
    public bool onTerritory = false;
    private Rigidbody rb;
    public bool canMove;
    private Vector3 lastPosition;
    public bool leftWall;
    public bool rightWall;
    public bool upWall;
    public bool downWall;
    public bool ceiling;
    public bool floor;
    public bool verticalPlane;

    public Text winText;
    public Text loseText;

    // Path will be a list of positions while the 
    public List<GameObject> pathObjects;
    public GameObject Trail;
    Vector3 originalPos;



    void Start()
    {

        rb = GetComponent<Rigidbody>();
        canMove = true;
        winText.text = "";
        loseText.text = "";
        lastPosition = this.transform.position;
        originalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }


    void FixedUpdate()
    {
        if (onTerritory)
            Move();
        else offTerritoryMove();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Territory"))
            onTerritory = true;

        if (other.gameObject.CompareTag("LeftWall"))
            leftWall = true;

        if (other.gameObject.CompareTag("RightWall"))
            rightWall = true;

        if (other.gameObject.CompareTag("UpWall"))
            upWall = true;

        if (other.gameObject.CompareTag("DownWall"))
            downWall = true;

        if (other.gameObject.CompareTag("Vertical Plane"))
            verticalPlane = true;

        if (other.gameObject.CompareTag("Enemy")) {

            loseText.text = "Gameover";
            //add a play again button
            this.transform.position = originalPos;

            clearPath();
        }
        if (other.gameObject.CompareTag("Finish Zone"))
        {

            winText.text = "Level Completed";

            //Add a play again button

            //this.transform.position = originalPos;


        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Territory"))
            onTerritory = true;
 
      
        if (other.gameObject.CompareTag("Territory"))
            onTerritory = true;

        if (other.gameObject.CompareTag("LeftWall"))
            leftWall = true;
        
        if (other.gameObject.CompareTag("RightWall"))  
            rightWall = true;
     
        if (other.gameObject.CompareTag("UpWall"))
            upWall = true;

        if (other.gameObject.CompareTag("DownWall"))
            downWall = true;
 
        if (other.gameObject.CompareTag("Ceiling"))
            ceiling = true;
       
        if (other.gameObject.CompareTag("Vertical Plane"))
            verticalPlane = true;
     
        if (other.gameObject.CompareTag("Floor"))
            floor = true;

    }


    void OnTriggerExit(Collider other)
    {
        onTerritory = false;
        leftWall = false;
        downWall = false;
        upWall = false;
        rightWall = false;
        ceiling = false;
        verticalPlane = false;

    }

    public void Move()
    {
        // do not let player go in the opposite direction,
        // and do not player move in more than one direction at once
        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;

        if (Input.GetKey(KeyCode.LeftArrow))
            left = true;
        if (Input.GetKey(KeyCode.RightArrow))
            right = true;
        if (Input.GetKey(KeyCode.UpArrow))
            up = true;
        if (Input.GetKey(KeyCode.DownArrow))
            down = true;
        if (!rightWall)
        {
            if (right && !up && !down)
            {
                transform.Translate(0, 0, thrust * Time.deltaTime);
                lastPress = "right";
            }
        }

        if (!leftWall)
        {

            if (left && !up && !down)
            {
                transform.Translate(0, 0, -thrust * Time.deltaTime);
                lastPress = "left";
            }
        }

        if (!upWall && !verticalPlane)
        {
            if (up && !left && !right)
            {
                transform.Translate(-thrust * Time.deltaTime, 0, 0);
                lastPress = "up";
            }
        }

        if (!downWall &&!verticalPlane)
        {
            if (down && !left && !right)
            {
                transform.Translate(thrust * Time.deltaTime, 0, 0);
                lastPress = "down";
            }
        }

        if (!ceiling)
        {
            if (verticalPlane && !left && !right)
            {
                transform.Translate(0, thrust * Time.deltaTime, 0);
                lastPress = "up_vertical";
            }
        }

        if (!floor)
        {
            if (verticalPlane && !left && !right)
            {
                transform.Translate(0, -thrust * Time.deltaTime, 0);
                lastPress = "down_vertical";
            }
        }


    }


    public void offTerritoryMove()
    {
        Move();

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

        if (lastPress == "up_vertical")
        {
            if (!ceiling)
            {
                transform.Translate(0, thrust * Time.deltaTime, 0);
            }

        }

        if (lastPress == "down_vertical")
        {
            if (!floor)
            {
                transform.Translate(0, -thrust * Time.deltaTime, 0);
            }

        }

        /*
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
        */

    }

    public void clearPath() {
        /* removes path from board */
        // for each object in pathObjects 
        foreach (var obj in pathObjects) {
            Destroy(obj);
        }
    }


    public void fill(List<Transform> path) {
        // fills in territory given list of player positions
        for (int i = 0; i < path.Count; i++) {
            // not sure what do to here. need to find which (if any) side of the 
            // box contains the enemy, and fill in the other section. 
            float x = path[i].position.x;
            float z = path[i].position.z;
        }
    }
}
