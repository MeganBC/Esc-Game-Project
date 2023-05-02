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
    private Vector3 direction;
    public float fireRate = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }
    void Update()
    {
        direction = player.transform.position - transform.position;
        if (Vector2.Distance(transform.position, player.transform.position) < AttackRange)
        {
            Invoke("Shoot", fireRate);
        }
    }

    void Shoot ()
    {
        GameObject bulletObject = Instantiate(enemyBullet, spawnPoint.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().velocity = direction * enemyBulletSpeed;
    }
}
