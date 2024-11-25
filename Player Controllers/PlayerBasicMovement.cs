using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMovement : MonoBehaviour
{
    //SCRIPT OVERVIEW: =============================================================================
    //Basic movement script that takes in the input and applies it to the velocity of the rigidbody.


    //Create varible (rb) for rigidbody
    Rigidbody2D rb;

    //Create varibles for horizontal & vertical inputs
    float hInput, vInput;

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
        Movement();
    }


    //Custom movement method/function =============================================================
    void Movement()
    {
        //Assign axises to varible names.
        //"GetAxis" will work for both the arrow keys & WASD as well as joysticks.
        //"GetAxis" will give a number between -1 & 1,
        //where -1 will be left (or down for vertical axis)
        //and 1 will be right (or up for vertical axis).
        //When either direction is realeased the axis will return to 0.
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        //Combine each axis varible into a single vector called "direction"
        Vector2 direction = new Vector2(hInput, vInput);

        //Apply direction to rigidbody multiplied by walkSpeed
        rb.velocity = direction * walkSpeed;
    }
}
