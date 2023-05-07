using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BossSpawner : MonoBehaviour
{
    public GameObject BossPrefab;
    public Vector3 SpawnPoint;

    private bool bossIsSpawned = false;
    void SummonBoss()
    {

        Instantiate(BossPrefab, SpawnPoint, Quaternion.identity);
        bossIsSpawned = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && bossIsSpawned == false)
            {
            SummonBoss();
            }
    }

}