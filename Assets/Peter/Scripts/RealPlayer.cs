using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RealPlayer : MonoBehaviour
{
    private Rigidbody2D body;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public TMPro.TMP_Text healthText;

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
    public bool dropDown;
    public float health = 100;
    public const float maxHealth = 100;
    Vector2 checkpointPosition;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //healthText = GetComponent<TMP_Text>();
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

        vertical = Input.GetAxisRaw("Vertical");
        if(vertical < 0)
        {
            dropDown = true;
        }
        else
        {
            dropDown = false;
        }

        if(body.velocity.x == 0 && isOnGround)
        {
            animator.Play("PlayerIdle");
        }
        if(body.velocity.x != 0 && isOnGround)
        {
            animator.Play("PlayerWalk");
        }
        if(body.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if(body.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if(!isOnGround)
        {
            animator.Play("PlayerJump");
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health <= 0)
        {
            health = maxHealth;
            transform.position = checkpointPosition;
        }

        healthText.text = health.ToString();
    }

    bool CanJump()
    {
        return isOnGround || currentJumps < maxJumps;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnGround(collision);

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            health -= Random.Range(15, 25);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Stairs"))
        {
            if(dropDown)
            {
                collision.collider.isTrigger = true;
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkpointPosition = collision.gameObject.transform.position;
        }

        if(collision.gameObject.CompareTag("Health"))
        {
            health += Random.Range(20, 40);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Stairs"))
        {
            collision.isTrigger = false;
        }
    }
}