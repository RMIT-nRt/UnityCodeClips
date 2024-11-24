using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;

    [HideInInspector]
    public Vector3 dir;

    [SerializeField] float bulletSpeed = 30;

    [SerializeField] GameObject blood;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = dir * bulletSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Environment")
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            //Spawn Blood at enemy location then destroy after a second
            var bld = Instantiate(blood, collision.transform.position, Quaternion.identity);
            Destroy(bld, 1);

            //Destroy Enemy
            Destroy(collision.gameObject);

            //Destroy this bullet
            Destroy(this.gameObject);
        }
    }
}
