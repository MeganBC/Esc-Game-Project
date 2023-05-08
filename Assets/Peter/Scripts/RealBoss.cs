using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum BossState {Idle, Attacking, Damaged, Dead}

public class RealBoss : MonoBehaviour
{
    public BossState state;
    GameObject player;
    public GameObject enemyBullet;
    Animator animator;
    Collider2D col;
    Rigidbody2D body;
    public float health = 5;
    float maxHealth = 5;
    public bool canTakeDamage;
    bool isInCooldown;
    public float cooldown = 0.5f;
    public float bulletSpeed;
    public Transform spawnPoint;

    public AudioSource hit;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        body = GetComponent<Rigidbody2D>();

        SetState(BossState.Idle);
    }

    void Update()
    {
        if(player.transform.position.y >= 60 && state != BossState.Dead)//a point during the drop down the player cant jump to, when player respawns boss resets
        {
            health = maxHealth;
            SetState(BossState.Idle);
        }

        //direction between player and boss
        Vector3 direction = player.transform.position - spawnPoint.transform.position;
        direction.Normalize();

        if (!isInCooldown && state == BossState.Attacking)
        {
            GameObject bulletObject = Instantiate(enemyBullet, spawnPoint.position, Quaternion.identity);
            bulletObject.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            isInCooldown = true;
            Invoke("ResetCooldown", cooldown);
        }
    }

    void ResetCooldown()
    {
        isInCooldown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(canTakeDamage && collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            SetState(BossState.Damaged);

            if(health <= 0)
                SetState(BossState.Dead);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canTakeDamage && collision.gameObject.CompareTag("Melee"))
        {
            health--;
            hit.Play();
        }
    }

    public void SetNewState()
    {
        if(state == BossState.Damaged)
        {
            SetState(BossState.Attacking);
        }
        if(state == BossState.Idle)
        {
            int rng = Random.Range(1, 15);//1 in 15 chance to start attacking again to give player time to attack. large number required due to short animation
            if(rng == 1)
                SetState(BossState.Attacking);
            else
                SetState(BossState.Idle);
        }
        if(state == BossState.Attacking)
        {
            int rng = Random.Range(1, 10);//1 in 10 chance to stop attacking for longer attack times
            if(rng == 1)
                SetState(BossState.Idle);
            else
                SetState(BossState.Attacking);
        }
    }

    public void SetState(BossState newState)
    {
        state = newState;

        if(state == BossState.Idle)
        {
            canTakeDamage = true;
            animator.Play("BossIdle");
        }
        else if(state == BossState.Attacking)
        {
            canTakeDamage = false;
            animator.Play("BossShoot");
        }
        else if(state == BossState.Damaged)
        {
            canTakeDamage = false;
            animator.Play("BossHurt");
        }
        else if(state == BossState.Dead)
        {
            canTakeDamage = false;
            animator.Play("BossDeath");
            body.velocity = Vector3.zero;
            body.gravityScale = 0;
            col.isTrigger = true;
        }
    }
}
