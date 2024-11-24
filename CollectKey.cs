using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectKey : MonoBehaviour
{
    // super simple one key for one dooor
    
    [SerializeField] Collider2D Block;

    // Start is called before the first frame update
    void Start()
    {
        // reset on game restart
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
