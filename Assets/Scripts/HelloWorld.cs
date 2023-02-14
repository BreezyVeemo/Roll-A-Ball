using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public string winMessage = "You win";
    public int counter = 0;
    public int winAmount = 5;
    public int winCount = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            counter++;
            if (counter == winAmount)
            {
                showMessage();
                counter = 0;
                winCount++;
            }
        }

    }

    void showMessage()
    {
        Debug.Log(winMessage + " " + winCount + " times");
    }
}
