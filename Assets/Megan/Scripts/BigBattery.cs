using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBattery : MonoBehaviour
{
    private float direction;
    public float movementSpeed;
    private Rigidbody2D body;
    public float stayStillFor = 5f;
    bool isWaitingToFlip = false;
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
    }
    private void Update()
    {
        body.velocity = new Vector2( body.velocity.x, direction * movementSpeed);
    }

    void FlipDirection()
    {
        direction *= -1f;
        isWaitingToFlip = false;
    }
}
