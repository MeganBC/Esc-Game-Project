using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    GameObject player;
    public GameObject enemyBullet;
    SpriteRenderer spriteRenderer;
    public Transform attachPoint;
    public Transform spawnPoint;
    public float bulletSpeed = 20;
    public bool isAttracted = false;
    float attractRange = 15;

    public float cooldown = .4f;
    bool isInCooldown = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < attractRange)
        {
            isAttracted = true;
        }
        else
        {
            isAttracted = false;
        }
        if(isAttracted)
        {
            //direction between player and enemy gun
            Vector3 direction = player.transform.position - transform.position;
            //rotate to face that direction
            transform.up = direction;

            transform.position = attachPoint.position;

            if(!isInCooldown)
            {
                GameObject bulletObject = Instantiate(enemyBullet, spawnPoint.position, Quaternion.identity);
                bulletObject.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;

                isInCooldown = true;
                Invoke("ResetCooldown", cooldown);
            }
        }

        //rotate sprite
        if (player.transform.position.x <= transform.position.x)
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
        if(collision.gameObject.CompareTag("Melee"))
        {
            Destroy(gameObject);
        }
    }
}
