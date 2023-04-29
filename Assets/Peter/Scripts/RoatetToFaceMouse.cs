using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoatetToFaceMouse : MonoBehaviour
{
    public Transform attachPoint;
    public GameObject bullet;
    public Transform spawnPoint;
    public float bulletSpeed = 20;
    SpriteRenderer spriteRenderer;
    public float ammo = 15;

    public float cooldown = .1f;
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

        if(Input.GetButton("Fire1") && !isInCooldown && ammo > 0)
        {
            GameObject bulletObject = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
            bulletObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;

            ammo--;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //random ammo amount
        if(collision.gameObject.CompareTag("Ammo"))
        {
            ammo += Random.Range(3, 5);
            Destroy(collision.gameObject);
        }
    }
}
