using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAndDestroy : MonoBehaviour
{
    //SCRIPT OVERVIEW: =============================================================================
    //An enemy script where the enemy will randomly rotate in search of the player.
    //If the player moves into the enemy's field of view they will chance the enemy and damage them.

    //NOTE: The enemy will need a child gameObject for the "field of view". This can just be a triangle sprite.
    //Both the enemy and the field of view will require a collider set to isTrigger.


    //Create a variable that is the time between rotations
    [SerializeField] float searchInterval = 1;

    //Create a variable which is the speed of the rotation
    [SerializeField] float searchSpeed = 4;

    //Create a variable for walk speed. Ensure this is less then the player walk speed
    [SerializeField] float walkSpeed = 4;

    //Create a variable to link the Health script
    [SerializeField] Health health;

    //Create a variable to link the CameraShake script
    [SerializeField] CameraShake cam;

    //Create a varible to store the force applied to the player if it touches an enemy
    [SerializeField] float pushBackForce = 4;

    //Create a varible to store the time the force is applied to the player if it touches an enemy
    [SerializeField] float pushBackTime = 0.5f;

    //Create a temporary quarternion variable, that will store the random rotation
    Quaternion searchRot;

    //Create a transform variable to store the player
    Transform player;


    void Start()
    {
        //Assign the player by find it with it's tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Start the custom search coroutine (below)
        StartCoroutine(Search());
    }


    void Update()
    {
        //Access the collider of the "field of view "child and check if it touching the player collider
        if (transform.GetChild(0).GetComponent<PolygonCollider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            //Calculate the direction of the player
            Quaternion toRot = Quaternion.LookRotation(Vector3.forward, player.position - transform.position);

            //Rotate the enemy towards the player
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, walkSpeed);

            //Move towards the player at walkSpeed multiplied by time
            transform.position = Vector2.MoveTowards(transform.position, player.position, walkSpeed * Time.deltaTime);
        }
        else
        {
            //Randomly rotate in search of player utilising the searchRot quaternion
            transform.rotation = Quaternion.Lerp(transform.rotation, searchRot, searchSpeed * Time.deltaTime);
        }
    }


    //Custom search coroutine ======================================================================
    IEnumerator Search()
    {
        //Randomly generate a rotation for the z-axis between 0 & 360 degrees
        searchRot = Quaternion.Euler(0, 0, Random.Range(0, 360));

        //Wait for the searchInterval in seconds
        yield return new WaitForSeconds(searchInterval);

        //Start the coroutine again
        StartCoroutine(Search());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the the enemy enters into the trigger of a gameObject with the tag, "Player"...
        //REMEMBER: "isTrigger" must be ticked on the enemy's collider.
        if (collision.gameObject.CompareTag("Player"))
        {
            //Access the Health script and remove -1 health and update health display
            health.UpdateHealth();

            //Access the CameraShake script and start shake
            if (cam != null)
            {
                cam.StartShake();
            }

            //Calculate push back direction which is just the player (collision object) position minus the enemies position
            Vector3 pushDir = collision.transform.position - transform.position;

            //Turn of the Players Basic Movement script so push back force can be applied.
            //NOTE: below requires the PlayerBasicMovement script but this can be whatever your movement script is called.
            collision.GetComponent<PlayerBasicMovement>().enabled = false;

            //Apply force to players rigidbody in the pushDir multiplied by pushBackForce
            collision.GetComponent<Rigidbody2D>().velocity = pushDir * pushBackForce;

            //Start the push timer coroutine and a the collision object (player) into the brackets (see below)
            StartCoroutine(PushTimer(collision.gameObject));
        }
    }


    //IEnumerators or "coroutines" are great for timed events. See line 65 below where we can delay an action in seconds
    //NOTE: A GameObject variable has been added in the brackets so we can have access to the player gameObject
    IEnumerator PushTimer(GameObject player)
    {
        //Wait for pushBackTime...
        yield return new WaitForSeconds(pushBackTime);

        //...then turn the players movement script back on.
        //NOTE: below requires the PlayerBasicMovement script but this can be whatever your movement script is called.
        player.GetComponent<PlayerBasicMovement>().enabled = true;
    }
}
