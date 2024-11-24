using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGunRotate : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunBarrel;
    [SerializeField] Transform barrelEnd;

    void Start()
    {
        
    }


    void Update()
    {
        GunRotate();
        GunShoot();
    }


    void GunRotate()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = dir;
    }


    void GunShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            var bullet = Instantiate(bulletPrefab, barrelEnd.position, Quaternion.identity);
            bullet.GetComponent<BulletController>().dir = barrelEnd.position - gunBarrel.position;
            bullet.transform.parent = null;
        }
    }
}
