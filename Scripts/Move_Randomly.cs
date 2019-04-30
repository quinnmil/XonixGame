using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Randomly : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;

    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);

        return new Vector3(x, y, z);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = RandomVector(0f, 5f);
    }

    void FixedUpdate()
    {
        //rb.AddForce(transform.forward * thrust);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
