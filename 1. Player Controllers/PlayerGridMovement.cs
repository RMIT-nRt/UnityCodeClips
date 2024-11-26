using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    //SCRIPT OVERVIEW: ================================================================================================
    //This allows the player to move a specified gridSize, or "step" size, at a regular interval (walkDelay).
    //The GridMovement function deals with the input (both arrow keys and WASD) then sends what direction is
    //pressed to a Coroutine that delays the movement, or "steps", by the interval.

    //OPTIONAL: "BodyRotating" has been added if you need to rotate the player sprite in the direction they
    //are moving. If this is required then the player sprite image gameObject MUST be a child gameObject of this
    //player controller and assigned to the "bodySprite" variable. 


    //Create varible (rb) for rigidbody
    Rigidbody2D rb;

    //Create varible to store last direction pressed
    Vector2 newPos;

    //Create a bool (true/false variable) to store if player is already moving
    bool moving;

    //Create a variable that you can edit in the Unity editor (SerializeField)
    //that will set the gridSize or the size of the "step" the player will take each keypress
    [SerializeField] float gridSize = 1;

    //Create a variable that will be the speed of player moving from "step" to "step"
    [SerializeField] float walkSpeed = 40;

    //Create a varible which will be the time between each of those "steps"
    [SerializeField] float walkDelay = 0.3f;

    //OPTIONAL: Create Transform variable for the body sprite gameObject and rotation speed.
    [SerializeField] Transform bodySprite;
    [SerializeField] float rotationSpeed = 3;


    void Start()
    {
        //Assign rigidbody to rb variable
        rb = GetComponent<Rigidbody2D>();

        //Assign the newPos vector to where the player is at the start
        newPos = transform.position;
    }


    void Update()
    {
        //Run custom movement method/function
        GridMovement();

        //Run custom rotation method/function
        BodyRotating();
    }


    //Custom movement method/function ====================================================================================
    void GridMovement()
    {
        //If the player isn't already moving...
        //!moving is the same as "moving == false", where as,
        //moving (without the exclamation) is the same as "moving == true"
        if (!moving)
        {

            //..check to see if any of the direction keys are being pressed...
            //Making sure to check for both the arrow keys OR the WASD keys.
            //Note: "OR" is defined by the double "pipes" (||) located with the "back-slash" key

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //Then start the coroutine (an in-built method that deals with "timed" events)
                //and state what direction was pressed ("left").
                StartCoroutine(MovementDelay("left"));

                //Optional: if needing player sprite to rotate in direction of movement
                //BodyRotating("left");
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                StartCoroutine(MovementDelay("right"));
                //BodyRotating("right");
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                StartCoroutine(MovementDelay("up"));
                //BodyRotating("up");
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                StartCoroutine(MovementDelay("down"));
                //BodyRotating("down");
            }
        }

        //Move the rigidbody (rb.MovePosition) using the Vector3.MoveTowards functionfrom the
        //Players current position (transform.position) to the new position (newPos) at the
        //speed, "walkSpeed" multiplied by time rather then framerate
        rb.MovePosition(Vector3.MoveTowards(transform.position, newPos, walkSpeed * Time.deltaTime));
    }


    //Coroutine function (Or "IEnumerator) ===============================================================================
    //These are great for dealing with "timed" events (note the "WaitForSeconds" function).
    //Also note a string variable (dir) was added in the brackets of the coroutine so we code state
    //the direction pressed when we started the coroutines above.
    IEnumerator MovementDelay(string dir)
    {
        //First state that Player is moving so to ignore any other keys pressed
        moving = true;

        //A "switch" is like a series of "if" statements.
        //Feed whatever changing variable we are going to check (dir) into the brackets next to "switch".
        //Then state what each of the possiblities are for that changing varible ( "case" then the possibility).
        //Then what action to take incase of that possibility, ensuring to add a "break" after that action.
        switch (dir)
        {
            //If coroutine is start with a "left" string added to the coroutine (see above at line 61)...
            case "left":

                //then update the newPos vector with wherever the Player is current located
                //plus the gridSize (or "step" size) in the left direction.
                //Repeat this for all other directions
                newPos = transform.position + new Vector3(-gridSize, 0);
                break;
            case "right":
                newPos = transform.position + new Vector3(gridSize, 0);
                break;
            case "up":
                newPos = transform.position + new Vector3(0, gridSize);
                break;
            case "down":
                newPos = transform.position + new Vector3(0, -gridSize);
                break;
        }

        //then add a delay in seconds (walkDelay)..
        yield return new WaitForSeconds(walkDelay);

        // before setting "moving" to false allowing another key/direction press
        moving = false;
    }


    //Custom body rotate method/function =================================================================================
    void BodyRotating()
    {
        //Get direction keypresses (arrow keys and WASD) and store in variables
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

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


    //Collision Trigger Event ===========================================================+===============================
    //If portals are in your game will need the following code to reset
    //the newPos variable to the second portal position
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Portal")
        {
            newPos = transform.position;
        }
    }
}
