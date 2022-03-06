using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nackiDeiEnemy : EnemyBase
{
    private Animator anim;
    public Transform player;
    private Rigidbody2D rb;

    public float chaseRadius;
    public float attackRadius;

    private string stateMachine;

    private Vector3 initialSpawnPoint;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialSpawnPoint = transform.position;

    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        Vector3 spawnDirection = initialSpawnPoint - transform.position;
        direction.Normalize();
        spawnDirection.Normalize();

        if (Vector3.Distance(player.position, transform.position) <= chaseRadius)
        {
            stateMachine = "chasing";
            anim.SetBool("isWalking", true);
        }
        else if (
          (transform.position.x >= initialSpawnPoint.x + 0.5) ||
          (transform.position.x <= initialSpawnPoint.x - 0.5) ||
          (transform.position.y >= initialSpawnPoint.y + 0.5) ||
          (transform.position.y <= initialSpawnPoint.y - 0.5))
        {
            stateMachine = "returning";
            //anim.SetBool("isWalking", true);
        }
        else
        {
            stateMachine = "sleeping";
            anim.SetBool("isWalking", false);
        }

        switch (stateMachine)
        {
            case "sleeping":                
                break;
            case "chasing":                
                rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
                anim.SetFloat("achse.x", direction.x);
                anim.SetFloat("achse.y", direction.y);
                break;
            case "returning":
                rb.MovePosition(transform.position + (spawnDirection * moveSpeed * Time.deltaTime));
                anim.SetFloat("achse.x", spawnDirection.x);
                anim.SetFloat("achse.y", spawnDirection.y);
                break;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}