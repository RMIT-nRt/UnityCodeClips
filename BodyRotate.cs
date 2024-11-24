using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotate : MonoBehaviour
{
    float hInput, vInput;


    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if (hInput < 0)
        {
            BodyRotating("left");
        }
        else if (hInput > 0)
        {
            BodyRotating("right");
        }

        if (vInput < 0)
        {
            BodyRotating("down");
        }
        else if (vInput > 0)
        {
            BodyRotating("up");
        }
    }


    //Custom body rotate method/function =================================================================================
    void BodyRotating(string dir)
    {
        switch (dir)
        {
            case "left":
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case "right":
                transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
            case "up":
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case "down":
                transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }
}
