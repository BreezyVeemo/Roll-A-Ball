using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour
{
    public float speed = 1;
    // Update is called once per frame
    void Update()
    {
        //Every frame, rotate object slightly
        transform.Rotate(new Vector3(10,10,20)* Time.deltaTime * speed);
    }
}
