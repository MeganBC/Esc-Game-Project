using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFaceMouse : MonoBehaviour
{
    public Transform attachPoint;
    public GameObject bullet;
    public Transform spawnPoint;
    public float bulletSpeed = 20;
    SpriteRenderer spriteRenderer;

    public AudioSource shoot;

    public float cooldown = .5f;
    bool isInCooldown = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (Input.GetButton("Fire1") && !isInCooldown)
        {
            GameObject bulletObject = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
            bulletObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            shoot.Play();
            
            isInCooldown = true;
            Invoke("ResetCooldown", cooldown);

        }

        //rotate sprite
        if (mousePosition.x < attachPoint.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    void ResetCooldown()
    {
        isInCooldown = false;
    }

}

