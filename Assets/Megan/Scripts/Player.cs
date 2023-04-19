using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    Vector2 horizontalForce, verticalForce;

    public float movementSpeed;
    public float jumpForce;
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

        if (Input.GetKeyDown(KeyCode.Space)) { 
        verticalForce.y = jumpForce;
        body.AddForce(verticalForce, ForceMode2D.Impulse);
        }

    }
}

        
    



