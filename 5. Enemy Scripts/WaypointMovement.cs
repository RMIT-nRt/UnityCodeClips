using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    //SCRIPT OVERVIEW: =============================================================================
    //A simple patrolling script for enemies but can be used on platforms too.
    //The gamObject will move between any number of positions (waypoints) and
    //pause at each as desired (pauseTime) in seconds.

    //NOTE: For this script to function correctly, create 3+ gameObjects underneath
    //an empty gameObject. The first one will be the enemy/platform which holds this script as well
    //as the enemy/platform sprite renderer, collider etc.
    //The other gameObjects will be the "waypoints" or positions the enemy/platform will move
    //and only need to be placed in the desired positions and require nothing else.


    //Create an array[] of Transform variables.
    //An array contains as many of the same variable types as you wish and can be access
    //using an interger within the square brackets e.g. waypoints[1]
    //NOTE: An array is "zero-indexed" meaning the first one is index 0, second index 1 etc.
    [SerializeField] Transform[] waypoints;

    //Create a interger variable to store the current index for the waypoints delay
    int waypointIndex = 0;

    //Create a float to store the speed and another for the pause time when the enemy/platform
    //reaches that position. REMINDER: pauseTime is in seconds
    [SerializeField] float speed = 2f;
    [SerializeField] float pauseTime = 1;

    //Create a bool variable to store whether enemy/platform is "pausing".
    bool pause = false;

    //Create a varible to store the current target position the enemy/platform is moving towards
    Transform currentTarget;


    void Start()
    {
        //Set currecTarget to first waypoint. REMEMBER the array is "zero-indexed".
        currentTarget = waypoints[0];
    }


    void Update()
    {
        //Run custom waypoint moving method/function
        WaypointMove();
    }


    void WaypointMove()
    {
        //Check to see if the enemy/platform ISN'T in a pause state...
        if (!pause)
        {
            //and if not paused, move the enemy/platform toward the current target
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }
        

        //Measure how close you are to target position and if extremely close (<0.1f) then...
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            //Increase the waypointIndex by one.
            //NOTE: waypointIndex++ is the same as write waypointIndex+=1 or waypointIndex = waypointIndex + 1;
            waypointIndex++;

            //Using modulo (%) we can loop the waypointIndex between 0 and the number of waypoints (.length) in the array.
            waypointIndex %= waypoints.Length;

            //Update the currentTarget to the new position in the waypoints array
            currentTarget = waypoints[waypointIndex];

            //If enemy/platform movement is not in a pause state
            if (!pause)
            {
                //Start pause coroutine (below)
                StartCoroutine(Pause());
            }
        }
    }


    //IEnumerators or "coroutines" are great for timed events. See line 91 below where we can delay an action in seconds
    IEnumerator Pause()
    {
        //Set enemy/platform to pause state...
        pause = true;

        //...wait pauseTie...
        yield return new WaitForSeconds(pauseTime);

        //...then unpause.
        pause = false;
    }
}


