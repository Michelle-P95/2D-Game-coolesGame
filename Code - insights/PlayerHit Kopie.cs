using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private AudioSource enemHit1;
    private AudioSource sound;
    private GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        sound = player.GetComponent<AudioSource>();
        enemHit1 = GameObject.Find("EnemyHitSound1").GetComponent<AudioSource>();
    }

    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("breakable")) {     // Player Hit auf pot           
            other.GetComponent<pot>().Smash();
            sound.Play();
        }

        if (other.CompareTag("Enemy")) {         // Player Hit auf Enemy                
            other.GetComponent<EnemyBase>().HitEnemy();         
            enemHit1.Play();
        }
    }
}
