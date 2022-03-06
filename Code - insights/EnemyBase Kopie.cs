using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health;
    public int baseAttack;
    public float moveSpeed;
    //public Vector3 initialSpawnPoint;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // WARUM FUNKTIONIERT UPDATE NICHT???????
    }

    public void HitEnemy() {
        health--;
        Debug.Log("Enemy Ouchie " + health);
        
        if (health == 0) {
            Destroy(gameObject);
            Debug.Log("Enemy Dead");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.collider.GetComponent<PlayerMovement>().HitPlayer();
            
        }
    }

}