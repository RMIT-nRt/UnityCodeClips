using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //SCRIPT OVERVIEW: =============================================================================
    //Simple script that will receive the direction of the bullet from the player
    //and apply it to the bullets rigidbody velocity.
    //It will also deal with what the bullet collides into.
    //Optionally it will instantiate (spawn) a blood particle system that will
    //for a short time if the bullet hits an enemy.


    //Create varible (rb) for rigidbody
    Rigidbody2D rb;

    //Create a direction vector varible that will receive the direction
    //the bullet will travel when fired. It must be a public variable so
    //the player script can access it and set the direction.
    //Making it public will show it in the editor but as we don't need
    //access to it we will hide it with the [HideInInspector] before the varible.
    [HideInInspector]
    public Vector3 dir;

    //Create serialized varible for bullet speed
    [SerializeField] float bulletSpeed = 30;

    //Optional: Create GameObject varible to store the blood prefab.
    //This will be instantiated ("spawned") when the bullet hits an enemy
    [SerializeField] GameObject blood;


    private void Start()
    {
        //Assign rigidbody to rb variable
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //Apply velocity to the bullet rigidbody in the direction
        //received from the player multiplied by bulletSpeed.
        rb.velocity = dir * bulletSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the bullet enters into a collision with the walls of the map,
        //map MUST have a composite collider and an "environment" tag, then
        //destroy (delete) the bullet.
        if (collision.transform.name == "Environment")
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the the bullet enters into the trigger of a gameObject with the tag, "Enemy"...
        //REMEMBER: "isTrigger" must be ticked on the enemy's collider.
        if (collision.transform.tag == "Enemy")
        {
            //Spawn Blood into a temporary variable called "var" at the enemy location...
            var bld = Instantiate(blood, collision.transform.position, Quaternion.identity);

            //Destroy the temporary variable "bld" after a second.
            //NOTE: The second varible in the destroy function delays it by an interval (1sec).
            Destroy(bld, 1);

            //Destroy Enemy
            Destroy(collision.gameObject);

            //Destroy this bullet
            Destroy(this.gameObject);
        }
    }
}
