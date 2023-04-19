using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoatetToFaceMouse : MonoBehaviour
{
    public Transform attachPoint;
    public GameObject bullet;
    public Transform spawnPoint;
    public float bulletSpeed = 10;

    void Start()
    {
        
    }

    void Update()
    {
        //mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        //direction between the gun and the mouse
        Vector3 direction = mousePosition - transform.position;

        //rotate to face that direction
        transform.up = direction;

        transform.position = attachPoint.position;

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bulletObject = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
            bulletObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        }
    }
}
