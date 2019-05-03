using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;

    public bool moveLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (moveLeft)
        {
            Vector3 temp = transform.position;
            temp.x -= moveSpeed * Time.deltaTime;
            transform.position = temp;
        }
        else
        {
            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;
            transform.position = temp;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //rb.velocity = RandomVector(0f, 5f);
        if (other.tag == "Territory" || other.tag == "LeftWall" 
            || other.tag == "RightWall" || other.tag == "UpWall" || other.tag == "DownWall")
        {
            moveLeft = !moveLeft;
        }
        if (other.tag == "Player"){
            // lose a life, reset player position. 
            // moved this logic to playerMovement.cs
            
        }

    }
}
