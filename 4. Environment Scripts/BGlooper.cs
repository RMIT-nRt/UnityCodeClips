using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGlooper : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float loopingPosition = -22;
    [SerializeField] bool left = true;

    void Update()
    {
        //Looper
        if (left == true && transform.position.x < loopingPosition)
        {
            transform.position = new Vector3(-loopingPosition, transform.position.y, transform.position.z);
        }
        else if (left == false && transform.position.x > -loopingPosition)
        {
            transform.position = new Vector3(loopingPosition, transform.position.y, transform.position.z);
        }


        if (left)
        {
            //Mover
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            //Mover
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
