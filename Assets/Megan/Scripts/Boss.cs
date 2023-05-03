using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    GameObject player;
    public GameObject enemyBullet;
    public float enemyBulletSpeed = 10;
    public Transform spawnPoint;
    private Vector3 direction;

    public float cooldown = 5f;
    bool isOnCooldown = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        direction = player.transform.position - transform.position;
        if (!isOnCooldown)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject bulletObject = Instantiate(enemyBullet, spawnPoint.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().velocity = direction * enemyBulletSpeed;
        isOnCooldown = true;
        Invoke("resetCooldown", cooldown);
    }
    private void resetCooldown()
    {
        isOnCooldown = false;
    }
}
