using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private AudioSource enemHit1;
    private AudioSource sound;
    private GameObject player;
    private Collider2D co2;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        sound = player.GetComponent<AudioSource>();
        co2 = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(co2, player.GetComponent<Collider2D>(), true);

        enemHit1 = GameObject.Find("EnemyHitSound1").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("breakable")) {             // Arrow Hit auf pot    
            collision.GetComponent<pot>().Smash();
            sound.Play();
        }
        if (collision.CompareTag("Enemy")) {                 // Arrow Hit auf Enemy        
            collision.GetComponent<EnemyBase>().HitEnemy();
            enemHit1.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
