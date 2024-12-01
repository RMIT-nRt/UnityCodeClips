using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    //Create an array of waypoints and serialize field so we
    //can drag the objects into the array in the editor
    [SerializeField] Transform[] wayPoints;

    //Create an 'index' to select which waypoint, in the array,
    //we are moving towards
    int currentWaypoint = 0;

    //Speed at which to move
    [SerializeField] float speed = 2f;



    void Update()
    {
        // Move to current waypoint using Vector3.MoveTowards(current position, target position, movement delta)
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypoint].position, speed * Time.deltaTime);

        //Calculate how close to current waypoint
        float distance = Vector2.Distance(transform.position, wayPoints[currentWaypoint].position);

        //If really close to current waypoint then change waypoint 'index' to next one
        if (distance < 0.1f)
        {
            currentWaypoint++;

            //Ensure to loop back to the first waypoint after the last one
            currentWaypoint %= wayPoints.Length;
        }
    }
}
