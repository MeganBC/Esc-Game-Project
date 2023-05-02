using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("CleanUp", 2);

        //direction between the bullet and the player
        Vector3 direction = player.transform.position - transform.position;
        //rotate to face that direction
        transform.right = direction;
    }

    void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Melee"))
        {
            Destroy(gameObject);
        }
    }
}
