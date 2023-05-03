using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float direction;
    private float movementSpeed;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        direction = -1f;
        movementSpeed = 4f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bounce"))
        {
           direction *= -1f;
        }

        if (direction < 0f)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction > 0f)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }
    private void FixedUpdate()
    {
        body.velocity = new Vector2(direction * movementSpeed, body.velocity.y);   
    }
}
