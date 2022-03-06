using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject ArrowPrefab;
    public float shootDelay;
    public float arrowMovement = 20f;
    public bool canShoot;
    public void Start()
    {
        canShoot = true;
    }
    public void Update() {

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) 
        {
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetButtonDown("attackBow") && canShoot == true) 
        {
            canShoot = false;
            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay() {
        yield return new WaitForSeconds(shootDelay);
        Shooting();
    }

    void Shooting() {
        GameObject arrow = Instantiate(ArrowPrefab, firePoint.position, firePoint.rotation);    // spawning Arrow at firePoint
        Rigidbody2D arrowbody = arrow.GetComponent<Rigidbody2D>();                              // Get the body of this arrow
        arrowbody.AddForce(firePoint.up * arrowMovement, ForceMode2D.Impulse);                  // Give Arrow Force
    }

    public void canShootagain() {
        canShoot = true;
    }
}
