using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst.CompilerServices;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    Vector2 horizontalForce, verticalForce;
    private TrailRenderer trailRenderer;
    Animator animator;
   public SpriteRenderer spriteRenderer;

    public AudioSource jump;
    public AudioSource hit;

    public float movementSpeed;
    public float maxMovementSpeed;
    public float jumpForce;
    public float maxAirSpeed;
    public int maxJumps = 1;

    int currentJumps = 0;
    bool isOnGround;
    public float maxSlope = 0.5f;

    private float horizontal;
    private float vertical;
    Vector2 checkpointPosition;

    // Dash variables
    public float dashingVelocity = 14f;
    private float dashingTime = 0.5f;
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash =true;

    public float health = 100;
    public const float maxHealth = 100;



    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        checkpointPosition = transform.position;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        horizontalForce.x = horizontal * movementSpeed * Time.deltaTime * 100;
        body.AddForce(horizontalForce);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        bool jumpInputReleased = Input.GetButtonUp("Jump");
       

        if (Input.GetButtonDown("Jump") && currentJumps <= maxJumps && isOnGround == true)
        {
            verticalForce.y = jumpForce;
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            currentJumps++;
            isOnGround = false;
            jump.Play();
        }
        if (body.velocity.x == 0 && isOnGround)
        {
            animator.Play("IdlePlayer");
        }
        if (body.velocity.x != 0 && isOnGround)
        {
            animator.Play("PlayerRun");
        }
        if (body.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (body.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (jumpInputReleased && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
        // Dash Stuff
        // I got this from a tutorial on youtube
        bool dashInput = Input.GetButtonDown("Dash");
        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            StartCoroutine(StopDashing());
            jump.Play();
        }
        if (isDashing)
        {
            body.velocity = dashingDirection * dashingVelocity;
        }
        if (isOnGround == true)
        {
            canDash = true;
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxMovementSpeed);
        }
        else
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxAirSpeed);

        if (horizontal == 0 && isOnGround)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        if (health <= 0)
        {
            body.velocity = Vector2.zero;
            transform.position = checkpointPosition;
            health = 100;
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
            transform.position = checkpointPosition;
            hit.Play();
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= Random.Range(15, 25);
            Destroy(collision.gameObject);
            hit.Play();
        }
    }
  private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }
}









