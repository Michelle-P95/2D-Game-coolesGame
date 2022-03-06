using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public GameObject HealthUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHealth(Vector3 currentPos) {
        Instantiate(HealthUpgrade, currentPos, Quaternion.identity);
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.collider.GetComponent<PlayerMovement>().HealPlayer();
            Destroy(gameObject);
        }
    }
}
