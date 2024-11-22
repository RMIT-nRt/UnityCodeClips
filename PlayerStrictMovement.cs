using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrictMovement : MonoBehaviour
{
    //Create varible (rb) for rigidbody
    Rigidbody2D rb;

    //Create varibles for horizontal & vertical inputs
    float hInput, vInput;

    //Create varible to store last direction pressed
    Vector2 lastDir;

    //Create variable for walkSpeed that can be adjusted in the Unity Editor (SerializeField)
    [SerializeField] float walkSpeed = 3;


    void Start()
    {
        //Assign rigidbody to rb variable
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //Run custom movement method/function
        StrictMovement();
    }


    //Custom movement method/function =============================================================
    void StrictMovement()
    {
        //Assign axises to varible names.
        //"GetAxis" will work for both the arrow keys & WASD as well as joysticks.
        //"GetAxis" will give a number between -1 & 1,
        //where -1 will be left (or down for vertical axis)
        //and 1 will be right (or up for vertical axis).
        //When either direction is realeased the axis will return to 0.
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        //Assign each axis varible to independent vectors
        Vector2 hDir = new Vector2(hInput, 0);
        Vector2 vDir = new Vector2(0, vInput);


        //If left or right (A/D) pressed store in lastDir variable
        //"Or" is defined by the double "pipes" (||) located with the "back-slash" key
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            lastDir = hDir;
        }

        //If up or down (W/S) pressed store in lastDir variable
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            lastDir = vDir;
        }


        //If any direction released set lastDir to zero
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            lastDir = Vector2.zero;
        }


        //Apply lastDir to rigidbody multiplied by walkSpeed
        rb.velocity = (lastDir * walkSpeed);
    }
}
