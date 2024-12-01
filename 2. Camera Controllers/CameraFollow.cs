using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //SCRIPT OVERVIEW =======================================================================================
    //This is a simple script that will move the camera to the player at a set speed.
    //This script will sit on the camera.


    //Create a transform variable to store the player
    Transform Player;

    //Create a serialized variable for the camera's move speed so it can be edited in the editor
    [SerializeField] float moveSpeed = 2;


    void Start()
    {
        //Assign the "Player" variable to the player by searching for it using it's tag, "Player"
        //NOTE: the Player should have the tag assigned in the editor
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        //Create a vector varible to store the players position
        Vector3 playerPos = new Vector3(Player.position.x, Player.position.y, transform.position.z);

        //move the camera from it's current position to the player's position at the moveSpeed rate
        transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
    }
}
