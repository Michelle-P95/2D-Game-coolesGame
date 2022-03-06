using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pot : MonoBehaviour
{
    private Animator anim;
    public UpgradeSpawner upgrade;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {

    }

    public void Smash() {        // Methode wenn der pot getroffen wird
        anim.SetBool("smash", true);    
        PlayerMovement.score = PlayerMovement.score + (((int)Timer.t/60)+1);
        StartCoroutine(breakCo());

        int random = Random.Range(1, 100);

        if(random <= 20) {
            Vector3 currentPos = transform.position;
            upgrade.SpawnHealth(currentPos);
        }       
    }

    IEnumerator breakCo() {      // Enumerator für WaitForSeconds     
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);        
    }
}
