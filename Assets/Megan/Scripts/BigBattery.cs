using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigBattery : MonoBehaviour
{
    private float direction;
    public float movementSpeed;
    private Rigidbody2D body;
    public float stayStillFor = 5f;
    bool isWaitingToFlip = false;
    public float health = 50;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        direction = 1f;
        movementSpeed =5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bounce"))
        {
            if (!isWaitingToFlip)
            {
                if (direction < 0)
                {
                    Invoke("FlipDirection", stayStillFor);
                    isWaitingToFlip = true;
                }
                else
                    FlipDirection();
            }
        } 
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health -= Random.Range(15, 20);

        }
    }
    private void Update()
    {
        body.velocity = new Vector2( body.velocity.x, direction * movementSpeed);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FlipDirection()
    {
        direction *= -1f;
        isWaitingToFlip = false;
    }
    
}
