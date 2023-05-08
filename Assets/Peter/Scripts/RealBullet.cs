using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBullet : MonoBehaviour
{
    public AudioSource wallHit;

    void Start()
    {
        Invoke("CleanUp", 2);

        //mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        //direction between the bullet and the mouse
        Vector3 direction = mousePosition - transform.position;
        //rotate to face that direction
        transform.right = direction;
    }

    void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(!collision.gameObject.CompareTag("Enemy"))
        {
            wallHit.Play();
            Destroy(gameObject);
        }
    }
}
