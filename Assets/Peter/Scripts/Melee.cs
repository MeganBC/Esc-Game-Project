using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float time = 0.5f;
    public Transform attachPoint;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    Rigidbody2D body;
    public bool isActive;

    public AudioSource attack;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        //direction between the gun and the mouse
        Vector3 direction = mousePosition - transform.position;

        //rotate to face that direction
        transform.right = direction;

        transform.position = attachPoint.position;

        if(isActive)
        {
            spriteRenderer.enabled = true;
            col.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
            col.enabled = false;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            Invoke("Attack", time);
            attack.Play();
            isActive = true;
        }
    }

    void Attack()
    {
        isActive = false;
    }
}
