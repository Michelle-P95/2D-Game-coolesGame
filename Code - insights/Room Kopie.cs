using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
public GameObject virtualCamera;
public bool needText;
public string placeName;
public GameObject text;
public Text placeText;

    public virtual void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            virtualCamera.SetActive(true);

            if(needText){
                StartCoroutine(placeNameCo());
            }

        }
    }
    private IEnumerator placeNameCo(){
        yield return new WaitForSeconds(0.5f);
        text.SetActive(true);
        placeText.text = placeName;
        placeText.CrossFadeAlpha(0, 3f, false);
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }

    public virtual void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player") && !other.isTrigger){
            virtualCamera.SetActive(false);
        }
    }
}