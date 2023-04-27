using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float time = 0.5f;
    bool exists;

    void Start()
    {
        Invoke("CleanUp", 0);
    }

    void CleanUp()
    {
        Destroy(gameObject, time);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
