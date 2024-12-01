using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //SCRIPT OVERVIEW =======================================================================================
    //This is a simple script that will shake the camera, when desired, for a set length of time (shakeLength)
    //or can be kept on for any length of time required (HoldShake coroutine). This could be used when the player
    //is injured or to simulate movement of large objects.

    //NOTE: This script will sit on the camera and works in conjunction with the CameraFollow script!


    //Create a series of variables for shakeRate (how quickly it shakes), shake distance (how much the camera moves),
    //shakeSpeed (the speed it moves between "shakes") and shakeLength (how long it shakes for).
    [SerializeField] float shakeRate = 0.1f;
    [SerializeField] float shakeDistance = 0.1f;
    [SerializeField] float shakeSpeed = 5f;
    [SerializeField] float shakeLength = 0.5f;

    //Create a bool to store whether it is shaking
    bool shaking;

    //Create a vector variable to store the randomly generated positions of the "shakes"
    Vector3 shakePos = Vector3.zero;

    //Create a bool to allow debugging and playing the shake script
    [SerializeField] bool testShake;

    //Create 2 transform variables to store the camera and the player
    Transform cam, player;

    //Create a CameraFollow variable for quick access to the CameraFollow script component.
    
    CameraFollow camFollow;


    private void Start()
    {
        //Assign the cam varible to the camera gameObject (the gameObject that "this" script is sitting on
        cam = this.transform;

        //Assign the camFollow to the CameraFollow script component.
        camFollow = this.GetComponent<CameraFollow>();

        //Assign the "Player" variable to the player by searching for it using it's tag, "Player"
        //NOTE: the Player should have the tag assigned in the editor
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Start the shakePos vector at the cameras position
        shakePos = cam.position;
    }


    void Update()
    {
        //This "if statement" is just for testing this CameraShake script.
        //REMINDER: writing the bool, testShake, alone in the brackets is the same as writin testShake == true.
        //If you are waiting to test for a false statement then an exclamation preceeds the bool so, !testShake
        //is the same as writing testShake == false.
        if (testShake)
        {
            //Run custom StartShake coroutine
            StartShake();

            //To prevent the StartShake coroutine being spammed at the frame rate, we quickly close of the "if statement"
            testShake = false;
        }

        //This "if statement" is the actual shaking. We test if the "shaking" bool is true....
        if (shaking)
        {
            //... then we firstly turn off the CameraFollow script component...
            camFollow.enabled = false;

            //...then move the camera from it's current position to the players position plus a randomly
            //generated position (shakePos) at the speed set by shakeSpeed multiplied by time rather than framerate
            cam.position = Vector3.MoveTowards(cam.position, player.position + shakePos, shakeSpeed * Time.deltaTime);
        }
        else
        {
            //If shaking has stopped then reenable the CameraFollow script component.
            camFollow.enabled = true;
        }
    }


    //Custom "public" StartShake method/function ===========================================================
    //This method/function will start the ShakeTimer for a set length of time defined by the shakeLength variable.
    //NOTE It's a public method/function so other scripts can access it.
    public void StartShake()
    {
        //Start the ShakeTimer coroutine (below)
        StartCoroutine(ShakeTimer());
    }


    //Custom "public" HoldShake method/function ============================================================
    //This method/function will keep shaking until told to stop. Again the method/function is public for external access.
    //NOTE: When calling this method a bool must be added into the brackets and it will be passed into the "shakeIt" bool
    //to control the "if statements" in the method/function.
    public void HoldShake(bool shakeIt)
    {
        //If shakeIt is true....
        if (shakeIt)
        {
            //...set the "shaking" variable to true for the positioning "if statement" in Update.
            shaking = true;

            //Start the Shaker coroutine (below)
            StartCoroutine(Shaker());
        }
        else
        {
            //Stop shaking and all coroutines
            shaking = false;
            StopAllCoroutines();
        }
    }


    //Custom ShakeTimer method/function ===================================================================
    //This method/function starts a timer, length defined by the shakeLength variable, and shakes for that length of time.
    IEnumerator ShakeTimer()
    {
        //Set the "shaking" variable to true for the positioning "if statement" in Update.
        shaking = true;

        //Start the Shaker coroutine (below)
        StartCoroutine(Shaker());

        //Wait for the length of time, shakeLength, in seconds...
        yield return new WaitForSeconds(shakeLength);

        ///.then stop shaking and all coroutines
        shaking = false;
        StopAllCoroutines();
    }


    //Custom Shaker method/function ===================================================================
    //This method/function will randomly generate the positions of the shake. The distance of the shake is
    //between the shakeDistance as a negative number and shakeDistance as a positive number.
    IEnumerator Shaker()
    {
        //Randomly generate the position on the x & y access but keep the camera's z position.
        shakePos = new Vector3(Random.Range(-shakeDistance, shakeDistance), Random.Range(-shakeDistance, shakeDistance), cam.position.z);

        //Wait for the shakeRate in seconds....
        yield return new WaitForSeconds(shakeRate);

        //...then start the Shaker coroutine again to randomly genarate the next shake position.
        //It will loop over until either the HoldShake or the ShakeTimer method "stops all coroutines"
        StartCoroutine(Shaker());
    }
}
