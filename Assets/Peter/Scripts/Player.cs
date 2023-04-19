using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    float horizontal, vertical;
    public bool isOnGround;

    private Rigidbody2D body;
    Vector2 horizontalForce, verticalForce;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        verticalForce.y = jumpForce;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        horizontalForce.x = horizontal * movementSpeed * Time.deltaTime * 100;
        body.AddForce(horizontalForce);

        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            body.AddForce(verticalForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;
    }
}
