using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesBreak : MonoBehaviour
{
    public GameObject objectToDestroy;
    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(objectToDestroy);
            GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }
}
