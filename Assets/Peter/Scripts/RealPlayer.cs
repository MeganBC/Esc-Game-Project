using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : MonoBehaviour
{
    private Rigidbody2D body;
    Vector2 horizontalForce, verticalForce;

    public float movementSpeed;
    public float maxMovementSpeed;
    public float maxAirSpeed;
    public float jumpForce;
    public int maxJumps = 1;

    int currentJumps = 0;
    bool isOnGround;
    public float maxSlope = 0.5f;

    private float horizontal;
    private float vertical;
    Vector2 checkpointPosition;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        horizontalForce.x = horizontal * movementSpeed * Time.deltaTime * 100;
        body.AddForce(horizontalForce);
        bool jumpInputReleased = Input.GetButtonUp("Jump");

        // limit movement speed
        if (isOnGround)
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxMovementSpeed);
        else
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxAirSpeed);
        //stop moving if direction is not held, only on ground
        if (horizontal == 0 && isOnGround)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && currentJumps <= maxJumps && isOnGround == true)
        {
            verticalForce.y = jumpForce;
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            currentJumps++;
            isOnGround = false;
        }

        if (jumpInputReleased && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
    }

    bool CanJump()
    {
        return isOnGround || currentJumps < maxJumps;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnGround(collision);
        /*
        if (collision.gameObject.CompareTag("Death"))
        {
            body.velocity = Vector2.zero;
            transform.position = checkpointPosition;
        }
        */
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkpointPosition = collision.gameObject.transform.position;
        }
    }
}