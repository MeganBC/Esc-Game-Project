using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    Vector2 horizontalForce, verticalForce;

    public float movementSpeed;
    public float jumpForce;
    public int maxJumps = 2;

    int currentJumps = 0;
    bool isOnGround;
    public float maxSlope = 0.5f;

    private float horizontal;
    private float vertical;



    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        horizontalForce.x = horizontal * movementSpeed * Time.deltaTime * 100;
        body.AddForce(horizontalForce);

        if (Input.GetKeyDown(KeyCode.Space) && currentJumps <= maxJumps)
        {
            verticalForce.y = jumpForce;
            body.AddForce(verticalForce, ForceMode2D.Impulse);
            currentJumps++;
            isOnGround = false;
        }
        
    }
    bool CanJump()
    {
        return isOnGround || currentJumps < maxJumps;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnGround(collision);

        if (collision.gameObject.CompareTag("Death"))
        {
            body.velocity = Vector2.zero;
        }
    }
    
    void CheckIfOnGround(Collision2D collision)
    {
        if (!isOnGround)
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];

                float dot = Vector2.Dot(contact.normal, Vector2.up);

                isOnGround = dot >= maxSlope;
                if (isOnGround)
                {
                    currentJumps = 0;
                }
            }
        }
    }
}









