using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyDamage;
    float movementSpeed;
    public float MinMovementSpeed = 1;
    public float MaxMovementSpeed = 3;
    public float attackRange = 5 ;
    private Rigidbody2D body;
    GameObject player;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        movementSpeed = Random.Range(MinMovementSpeed, MaxMovementSpeed);

    }


    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange )
        {
            transform.up = player.transform.position - transform.position;
            Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed);
        }
    }
}
