using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    public float speed2;
    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody component of the gameObject
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the input value from horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from vertical axis
        float moveVertical = Input.GetAxis("Vertical");
        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        //Add force to rigidbody based on new movement vector
        rb.AddForce(movement * speed);
    }
}
