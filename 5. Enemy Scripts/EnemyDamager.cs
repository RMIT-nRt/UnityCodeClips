using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    //SCRIPT OVERVIEW: =============================================================================
    //A simple script that causes damge to the player when it comes in contact with the enemy.
    //It relies on the creation of the health script and must be linked in the editor.
    //It will also apply force (pushBackForce) to the player and push the player away for a
    //set time (pushBackTime)

    //OPTIONAL: Camera shake has been add as an optional feature so relies on the CameraShake
    //script being on the camera and linked in the editor also.

    //Create a variable to link the Health script
    [SerializeField] Health health;

    //Create a variable to link the CameraShake script
    [SerializeField] CameraShake cam;

    //Create a varible to store the force applied to the player if it touches an enemy
    [SerializeField] float pushBackForce = 4;

    //Create a varible to store the time the force is applied to the player if it touches an enemy
    [SerializeField] float pushBackTime = 0.5f;


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
