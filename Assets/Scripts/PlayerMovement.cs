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
    public GameObject crate1;
    public GameObject crate2;
    public GameObject crate3;
    public GameObject crate4;
    private GameObject crate_clone1;
    private GameObject crate_clone2;
    private GameObject crate_clone3;
    private GameObject crate_clone4;
    public Vector3 crate_pos1;
    public Vector3 crate_pos2;
    public Vector3 crate_pos3;
    public Vector3 crate_pos4;
    private bool gone_1 = false;
    private bool gone_2 = false;
    private bool gone_3 = false;
    private bool gone_4 = false;
    
    public AudioClip playerSound;
    public AudioClip levelComplete;
    public AudioClip collectingSound;
    AudioSource audioSource;
    

    Vector3 originalPos;
    public TextMeshProUGUI deathCounter;
    public TextMeshProUGUI winAlert;
    public TextMeshProUGUI crateCounter;




    private void Awake()
    {
        if (crates)
        {
            print("awake");
            crate_clone1 = Instantiate(crate1, crate_pos1, Quaternion.identity);
            crate_clone2 = Instantiate(crate2, crate_pos2, Quaternion.identity);
            crate_clone3 = Instantiate(crate3, crate_pos3, Quaternion.identity);
            crate_clone4 = Instantiate(crate4, crate_pos4, Quaternion.identity);
        }

    }

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        canMove = true;
        lastPosition = this.transform.position;
        originalPos = this.transform.position;
        print(originalPos);
         
         audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (crates)
            crateCounter.text = "Crates:" + (numCrates/2).ToString()+"/"+crate_num.ToString();
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
            if (crates)
            {
                if (gone_1)
                {
                    gone_1 = false;
                    crate_clone1 = Instantiate(crate1, crate_pos1, Quaternion.identity);
                }

                if (gone_2)
                {
                    gone_2 = false;
                    crate_clone2 = Instantiate(crate2, crate_pos2, Quaternion.identity);
                }

                if (gone_3)
                {
                    gone_3 = false;
                    crate_clone3 = Instantiate(crate3, crate_pos3, Quaternion.identity);
                }
                if (gone_4)
                {
                    gone_4 = false;
                    crate_clone4 = Instantiate(crate4, crate_pos4, Quaternion.identity);
                }
            }

            //Plays sound
            audioSource.PlayOneShot(playerSound, 1);
        }
        
        if (other.gameObject.CompareTag("Crate1"))
        {
            Destroy(crate_clone1);
            numCrates += 1;
            gone_1 = true;
            
            //Plays sound
            audioSource.PlayOneShot(collectingSound, 1);
        }
        
        if (other.gameObject.CompareTag("Crate2"))
        {
            Destroy(crate_clone2);
            numCrates += 1;
            gone_2 = true;
            
            //Plays sound
            audioSource.PlayOneShot(collectingSound, 1);
        }
        
        if (other.gameObject.CompareTag("Crate3"))
        {
            Destroy(crate_clone3);
            numCrates += 1;
            gone_3 = true;
            
            //Plays sound
            audioSource.PlayOneShot(collectingSound, 1);
        }
        
        if (other.gameObject.CompareTag("Crate4"))
        {
            Destroy(crate_clone4);
            numCrates += 1;
            gone_4 = true;
            
            //Plays sound
            audioSource.PlayOneShot(collectingSound, 1);
        }
        
        if (other.gameObject.CompareTag("Finish Zone"))
        {
            onTerritory = true;
            if (crates)
            {
                if (numCrates == crate_num*2)
                {
                    win = true;
                    winAlert.text = "Level Complete";
                    StartCoroutine("nextLevel");
                    Debug.Log("on territory enter");
                    //Plays sound
                    audioSource.PlayOneShot(levelComplete, 1);
                }
            }
            else
            {
                win = true;
                winAlert.text = "Level Complete";
                StartCoroutine("nextLevel");
                Debug.Log("on territory enter");
                //Plays sound
                audioSource.PlayOneShot(levelComplete, 1);
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
