using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text winTime;
    public GameObject inGamePanel;
    public TMP_Text timerText;
    public TMP_Text scoreText;





    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody component of the gameObject
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in the scene and updates the score UI to reflect the count
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        scoreText.text = (("Pickups Remaining: ") + pickupCount);

        //Display pickup count
        CheckPickups();
        //Get the Timer object
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();

        //Disables the win panel while the game runs
        winPanel.SetActive(false);
        //Enables the ingame panel while game runs
        inGamePanel.SetActive(true);
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
        timerText.text = (("Time: ") + timer.GetTime().ToString("F2"));
    }



    //Collecting items with pickup tag
    void OnTriggerEnter(Collider other)
    {
        //if the other object has the pickup tag, destroy it
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            //Decrement the pickup counter and update the UI count
            pickupCount -= 1;
            scoreText.text = (("Pickups Remaining: ") + pickupCount);
            //Call function to display remaining/win message
            CheckPickups();
        }

    }


    //Win Message display
    void WinGame()
    {
        //Turns on the win panel, deactivating the ingame panel
        inGamePanel.SetActive(false);
        winPanel.SetActive(true);
        //Set the time onto the win message text
        winTime.text = (("Time: ") + timer.GetTime().ToString("F2"));
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
