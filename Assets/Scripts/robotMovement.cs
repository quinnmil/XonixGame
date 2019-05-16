using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotMovement : MonoBehaviour
{
    private Rigidbody rb;
    public bool leftWall = false;
    public bool rightWall = false;
    public bool upWall;
    public bool downWall;
    public bool ceiling;
    public bool floor;
    public float thrust = 5;
    public bool verticalPlane;
 

    void Start()
    {
        rb = GetComponent<Rigidbody>();


}

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Rotate(Vector3.right*Time.deltaTime*20);
        float drone_dir = -thrust * Time.deltaTime;
        if (leftWall || rightWall)
            transform.Translate(0, 0, drone_dir);
            StartCoroutine(Move(drone_dir));





    }
    IEnumerator Move(float drone_dir)
    {
        yield return new WaitForSeconds(0.5f);
        transform.Translate(0, 0, drone_dir);

    }


    void OnTriggerEnter(Collider other)
    {

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
    }

   /* void OnTriggerStay(Collider other)
    {

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

    }
    */


    void OnTriggerExit(Collider other)
    {
        leftWall = false;
        downWall = false;
        upWall = false;
        rightWall = false;
        ceiling = false;
        floor = false;
        verticalPlane = false;

    }

}


