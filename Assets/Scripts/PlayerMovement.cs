using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



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
    public bool enemy;
    public bool verticalPlane;
    public float orig_x = -20;
    public float orig_y = 0;
    public float orig_z = -26;
    private static int deaths = 0;
    public bool win = false;
    public int crate_num;
    public int numCrates = 0;
    public bool crates = false;
    public GameObject crate;
    private GameObject crate_clone;
    public Vector3 crate_pos;
    
    //sounds
    public GameObject playerSound;
    public GameObject levelCompleteSound;

    Vector3 originalPos;
    public TextMeshProUGUI deathCounter;
    public TextMeshProUGUI winAlert;
    public TextMeshProUGUI crateCounter;




    void Start()
    {

        rb = GetComponent<Rigidbody>();
        canMove = true;
        lastPosition = this.transform.position;
        originalPos = this.transform.position;
        print(originalPos);
        crate_clone = Instantiate(crate,crate_pos,Quaternion.identity);
    }

    private void Update()
    {
        if (crates)
            crateCounter.text = "Crates:" + numCrates.ToString()+"/"+crate_num.ToString();
        deathCounter.text = "Deaths:" + deaths.ToString();
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
        if (other.gameObject.CompareTag("floor"))
            floor = true;

        if (other.gameObject.CompareTag("Enemy"))
        {
            numCrates = 0;
            enemy = true;
            crate_clone = Instantiate(crate,crate_pos, Quaternion.identity);
            
            //Plays sound
            Instantiate(playerSound, other.transform.position, other.transform.rotation);
        }
        
        if (other.gameObject.CompareTag("Crate"))
        {
            Destroy(crate_clone);
            numCrates = 1;
            

        }
        if (other.gameObject.CompareTag("Finish Zone"))
        {
            onTerritory = true;
            if (crates)
            {
                if (numCrates == crate_num)
                    win = true;
                    winAlert.text = "Level Complete";
                    StartCoroutine("nextLevel");
                    Debug.Log("on territory enter");
            }
            else
            {
                win = true;
                winAlert.text = "Level Complete";
                StartCoroutine("nextLevel");
                Debug.Log("on territory enter");

                //Plays level complete sound
                Instantiate(levelCompleteSound, other.transform.position, other.transform.rotation);

            }

        }
        
    }
    
    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    void OnTriggerStay(Collider other)
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

        if (other.gameObject.CompareTag("Ceiling"))
            ceiling = true;

        if (other.gameObject.CompareTag("Vertical Plane"))
            verticalPlane = true;

        if (other.gameObject.CompareTag("floor"))
            floor = true;
        if (other.gameObject.CompareTag("Enemy"))
        {

            enemy = true;
            lastPress = "";
            deaths += 1;
            this.transform.position = originalPos;
        }

    }


    void OnTriggerExit(Collider other)
    {
        onTerritory = false;
        leftWall = false;
        downWall = false;
        upWall = false;
        rightWall = false;
        ceiling = false;
        floor = false;
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
                transform.Translate(thrust * Time.deltaTime, 0, 0);
                lastPress = "right";
            }
        }

        if (!leftWall)
        {

            if (left && !up && !down)
            {
                transform.Translate(-thrust * Time.deltaTime, 0, 0);
                lastPress = "left";
            }
        }

        if (!upWall && !verticalPlane)
        {
            if (up && !left && !right)
            {
                transform.Translate(0, 0, thrust * Time.deltaTime);
                lastPress = "up";
            }
        }

        if (!downWall && !verticalPlane)
        {
            if (down && !left && !right)
            {
                transform.Translate(0, 0, -thrust * Time.deltaTime);
                lastPress = "down";
            }
        }

        if (!ceiling)
        {
            if (verticalPlane && up && !left && !right)
            {
                transform.Translate(0, thrust * Time.deltaTime, 0);
                lastPress = "up_vertical";
            }
        }

        if (!floor)
        {
            if (verticalPlane && down && !left && !right)
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
                transform.Translate(thrust * Time.deltaTime, 0, 0);
            }
        }

        if (lastPress == "left")
        {
            if (!leftWall)
            {
                transform.Translate(-thrust * Time.deltaTime, 0, 0);
            }
        }

        if (lastPress == "up")
        {
            if (!upWall && !verticalPlane)
            {
                transform.Translate(0, 0, thrust * Time.deltaTime); ;
            }
        }

        if (lastPress == "down")
        {
            if (!downWall)
            {
                transform.Translate(0, 0, -thrust * Time.deltaTime);
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


    }

}
