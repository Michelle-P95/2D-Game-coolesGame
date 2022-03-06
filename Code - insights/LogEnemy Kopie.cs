using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : EnemyBase
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

        if(Vector3.Distance(player.position, transform.position) <= chaseRadius) {
            wakeUp();
            stateMachine = "chasing";
        } else if (
            (transform.position.x >= initialSpawnPoint.x + 0.5) ||
            (transform.position.x <= initialSpawnPoint.x - 0.5) ||
            (transform.position.y >= initialSpawnPoint.y + 0.5) ||
            (transform.position.y <= initialSpawnPoint.y - 0.5) ) 
        {
            
            stateMachine = "returning";
        } else {
            goSleep();
            stateMachine = "sleeping";
        }

        switch(stateMachine){
            case "sleeping":
                break;
            case "chasing":
                rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
                anim.SetFloat("achseX", direction.x);
                anim.SetFloat("achseY", direction.y);
                break;
            case "returning":
                rb.MovePosition(transform.position + (spawnDirection * moveSpeed * Time.deltaTime));
                anim.SetFloat("achseX", spawnDirection.x);
                anim.SetFloat("achseY", spawnDirection.y);
                break;
        }

    }

    void wakeUp() 
    {
        moveSpeed = 0;
        anim.SetBool("sleeping", false);
        anim.SetBool("walking", true);
        moveSpeed = 3;
    }

    void goSleep()
    {
        moveSpeed = 0;
        anim.SetBool("walking", false);
        anim.SetBool("sleeping", true);
    }


    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}