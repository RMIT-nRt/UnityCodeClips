using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMovement : MonoBehaviour
{
    //SCRIPT OVERVIEW: =============================================================================
    //Basic movement script that takes in the input and applies it to the velocity of the rigidbody.

    //OPTIONAL: "BodyRotating" has been added if you need to rotate the player sprite in the direction they
    //are moving. If this is required then the player sprite image gameObject MUST be a child gameObject of
    //this player controller and assigned to the "bodySprite" variable


    //Create varible (rb) for rigidbody
    Rigidbody2D rb;

    //Create varibles for horizontal & vertical inputs
    float hInput, vInput;

    //Create variable for walkSpeed that can be adjusted in the Unity Editor (SerializeField)
    [SerializeField] float walkSpeed = 3;

    //OPTIONAL: Create Transform variable for the body sprite gameObject
    [SerializeField] Transform bodySprite;
    [SerializeField] float rotationSpeed = 3;

    void Start()
    {
        //Assign rigidbody to rb variable
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        //Run custom movement method/function
        Movement();

        //Run custom body rotation method/function
        BodyRotating();
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


    //Custom body rotate method/function =================================================================================
    void BodyRotating()
    {
        //Create a vector variable with both axis inputs
        Vector2 moveDir = new Vector2(hInput, vInput);

        //As long as a direction key is pressed...
        if (moveDir != Vector2.zero)
        {
            //Set the rotation with the specified forward direction relative to the direction of movement.
            Quaternion toRot = Quaternion.LookRotation(Vector3.forward, moveDir);

            //Rotate the bodySprite to desired rotation
            bodySprite.rotation = Quaternion.RotateTowards(bodySprite.rotation, toRot, rotationSpeed);
        }
    }
}
