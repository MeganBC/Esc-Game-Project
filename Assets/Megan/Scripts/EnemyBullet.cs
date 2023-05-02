using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Start()
    {
        Invoke("CleanUp", 2f);
    }
    private void Update()
    {
        Invoke("CleanUp", 2f);
    }

    void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
