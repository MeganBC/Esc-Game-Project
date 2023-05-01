using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    public float AttackRange = 3;
    GameObject player;
    public GameObject enemyBullet;
    public float enemyBulletSpeed = 15;
    public Transform spawnPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < AttackRange)
        {
            Shoot();
            
        }
    }

    void Shoot ()
    {
        GameObject bulletObject = Instantiate(enemyBullet, spawnPoint.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().velocity = transform.up * enemyBulletSpeed;
    }
}
