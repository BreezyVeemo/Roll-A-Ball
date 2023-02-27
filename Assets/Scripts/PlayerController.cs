using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    public float speed2;
    int pickupCount;
    Timer timer;

    [Header("UI")]
    public GameObject winPanel;




    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody component of the gameObject
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in the scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;

        //Display pickup count
        CheckPickups();
        //Get the Timer object
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();

        //Disables the win panel while the game runs
        winPanel.SetActive(false);
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

    //Collecting items with pickup tag
    void OnTriggerEnter(Collider other)
    {
        //if the other object has the pickup tag, destroy it
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            //Decrement the pickup counter
            pickupCount -= 1;
            //Call function to display remaining/win message
            CheckPickups();
        }

    }

    //Win Message display
    void WinGame()
    {
        //makes the win message appear once all pickups are collected
        winPanel.SetActive(true);
        Debug.Log("Time: " + timer.GetTime().ToString("F2"));
    }
    //Display count of remaining pickups
    void CheckPickups()
    {
        //Display remaining pickup count
        Debug.Log("Pickups remaining: " + pickupCount);
        //If all pickups are collected, display a win message
        if (pickupCount == 0)
        {
            timer.StopTimer();
            WinGame();
        }
    }

}
