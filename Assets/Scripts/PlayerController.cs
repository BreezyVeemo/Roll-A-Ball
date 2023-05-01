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
    int collectedCount;
    int pickupTotal;

    GameController gameController;
    Timer timer;

    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    bool isPaused = false;

    [Header("UI")]
    public GameObject gameOverPanel;
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
        pickupTotal = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //The count will be changed later, the total will not
        pickupCount = pickupTotal;
        //Alternative collectedcount variable for counting the score UP rather than DOWN (this is the currently used system)
        collectedCount = 0;
        //Display the current count of collected pickups, compared to the total
        scoreText.text = (("Collected: ") + collectedCount + ("/") + pickupTotal);

        //Display pickup count
        CheckPickups();
        //Get the Timer object and start the timer
        timer = FindObjectOfType<Timer>();

        //Disables the endgame panel while game runs
        gameOverPanel.SetActive(false);
        //Enables the ingame panel while game runs
        inGamePanel.SetActive(true);

        //Finds the reset point and default material
        resetPoint = GameObject.Find("ResetPoint");
        originalColour = GetComponent<Renderer>().material.color;


        //Speedrun mode checks
        gameController = FindObjectOfType<GameController>();
        timer = FindObjectOfType<Timer>();
        if (gameController.gameType == GameType.SpeedRun)
            StartCoroutine(timer.StartCountdown());
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (resetting)
            return;

        if (gameController.gameType == GameType.SpeedRun && !timer.IsTiming())
            return;



        //Get the input value from horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from vertical axis
        float moveVertical = Input.GetAxis("Vertical");
        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        //Add force to rigidbody based on new movement vector
        rb.AddForce(movement * speed * Time.deltaTime);
    }



    //Collecting items with pickup tag
    void OnTriggerEnter(Collider other)
    {
        //if the other object has the pickup tag, destroy it
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            //Decrement the pickup counter and update the UI count (with the now unused code)
            //scoreText.text = (("Pickups Remaining: ") + pickupCount);
            pickupCount -= 1;
            //Increment the collected counter and update the UI count
            collectedCount += 1;
            //Update the score text to reflect how many pickups have now been collected
            scoreText.text = (("Collected: ") + collectedCount + ("/") + pickupTotal);
            //Call function to display remaining/win message
            CheckPickups();
        }

    }


    //Win Message display
    void WinGame()
    {

        //Turns on the win panel, deactivating the ingame panel
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        if (gameController.gameType == GameType.SpeedRun)
            timer.StopTimer();
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            if (gameController.wallType == WallType.Punishing)
                StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.red;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }

}
