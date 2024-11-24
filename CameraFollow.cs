using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform Player;

    [SerializeField] float moveSpeed = 2;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    void Update()
    {
        Vector3 playerPos = new Vector3(Player.position.x, Player.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
    }
}
