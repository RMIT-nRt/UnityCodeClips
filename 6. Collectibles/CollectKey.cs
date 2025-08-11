using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectKey : MonoBehaviour
{
    // Super simple one key for one dooor
    
    [SerializeField] Collider2D Door; //Ensure your door object has a 2D Collider

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Block.enabled = false;
            Destroy(Block.gameObject);

            Destroy(gameObject);
        }
    }

}
