using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    bool isAttracted = false;
    float attractRange = 7;
    public GameObject ammo;
    public GameObject health;
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //if player position.y is -1<enemy position.y<1 follow player
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(health, body.position, Quaternion.identity);
            float rng = Random.Range(1, 3);
            if(rng == 1)
            {
                Instantiate(ammo, body.position, Quaternion.identity);
            }
        }
    }
}
