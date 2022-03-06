using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotEnemy : EnemyBase
{
    public Transform player;
    private Rigidbody2D rb;

    public float chaseRadius;
    public float attackRadius; 


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
       
    }

    void FixedUpdate()
    {
        Vector3 direction = player.position - transform.position;
        
        direction.Normalize();

        if(Vector3.Distance(player.position, transform.position) <= chaseRadius) {
            rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
        } 
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}