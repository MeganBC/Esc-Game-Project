using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoatetToFaceMouse : MonoBehaviour
{
    public Transform attachPoint;
    public GameObject bullet;
    public Transform spawnPoint;
    public float bulletSpeed = 40;
    SpriteRenderer spriteRenderer;

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

        if (Input.GetButton("Fire1") && !isInCooldown)
        {
            GameObject bulletObject = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
            bulletObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;

            isInCooldown = true;
            Invoke("ResetCooldown", cooldown);
        }

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
        if(collision.gameObject.CompareTag("Ammo"))
        {

        }
    }
}
