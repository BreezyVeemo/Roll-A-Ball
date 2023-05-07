using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObjects : MonoBehaviour
{
    public float speed = 1;
    // Update is called once per frame
    void Update()
    {
        //Every frame, rotate object slightly
        transform.Rotate(new Vector3(0, 10,0) * Time.deltaTime * speed);
    }
}
