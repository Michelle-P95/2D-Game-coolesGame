using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

using System.IO;
using System.Linq;
using System.Net.Mime;
//using UnityEditor.VersionControl;

public enum PlayerState{
    walk,
    attack,
    interact,
    attackBow
}

public class PlayerMovement : MonoBehaviour
{
    private GameObject heart0;
    private GameObject heart1;
    private GameObject heart2;
    private GameObject heart3;
    private GameObject heart4;


    private float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;
    public PlayerState currentState;

    public int health;
    private int Toepfe;

    public static int score; // bad coding style but ok for 1 variable
    public Text scoreText;

    public string highscore;
    public Text highscoreText;
    //private string path;
    
    public Text ToepfeCount;

    Vector2 movement;

    GameObject youDiedUI;
    GameObject uiWin;
    public Text becauseText;
    public Text scoreT;
    private AudioSource dmgsound;
    private GameObject cam;

    private GameObject swordSoundObj;
    private AudioSource swordAttack;

    private Boolean canMove;
    private Shoot shooting;
    private GameObject player;
    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        dmgsound = cam.GetComponent<AudioSource>();

        //path = "Assets/Scenes/HighScore.txt";

        heart0 = GameObject.Find("Player");
        heart1 = GameObject.Find("heart (1)");
        heart2 = GameObject.Find("heart (2)");
        heart3 = GameObject.Find("heart (3)");
        heart4 = GameObject.Find("heart (4)");

        heart0.SetActive(true);
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        heart4.SetActive(true);

        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();

        youDiedUI = GameObject.FindGameObjectWithTag("dead");
        youDiedUI.SetActive(false);

        uiWin = GameObject.FindGameObjectWithTag("win");
        uiWin.SetActive(false);

        Toepfe = GameObject.FindGameObjectsWithTag("breakable").Length;

        score = 0;
        scoreText.text = "Score: " + score;
        DisplayHighscore();

        ToepfeCount.text = "Toepfe uebrig: " + Toepfe;

        swordSoundObj = GameObject.Find("swordAttack Sound");
        swordAttack = swordSoundObj.GetComponent<AudioSource>();

        canMove = true;
        player = gameObject;
        shooting = player.GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update() {
        HighscoreUpdate();

        switch(health) {
            case 0: 
            heart0.SetActive(false);
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            break;
            case 1: 
            heart0.SetActive(true);
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            break;
            case 2: 
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            break;
            case 3: 
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            heart4.SetActive(false);
            break;
            case 4: 
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(false);
            break;
            case 5: 
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            break;
        }

        scoreText.text = "Score: " + score;

        Toepfe = GameObject.FindGameObjectsWithTag("breakable").Length;
        ToepfeCount.text = "Toepfe uebrig: " + Toepfe;
        
        movement.x = Input.GetAxisRaw("Horizontal"); //input
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.attackBow)
        {
            currentState = PlayerState.attack;
            canMove = false;
            animator.SetBool("moving", false);
            moveSpeed = 0f;
            AttackCo();
        }
        else if (Input.GetButtonDown("attackBow") && currentState != PlayerState.attack && currentState != PlayerState.attackBow)
        {
            currentState = PlayerState.attackBow;
            canMove = false;
            animator.SetBool("moving", false);
            moveSpeed = 0f;
            AttackBowCo();
        }
        else if (currentState == PlayerState.walk)
        {
            moveSpeed = 3.75f;
            UpdateAnimationAndMove();
        }
        UpdateAnimationAndMove();

        if (Toepfe == 0)
        {
            Time.timeScale = 0f;
            uiWin.SetActive(true);
        }

        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateAnimationAndMove()
    {
        if (movement != Vector2.zero && canMove == true)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    void EndAttack()
    {
        animator.SetBool("attacking", false);
        currentState = PlayerState.walk;
        canMove = true;
    }

    private void AttackCo()
    {
        swordAttack.Play();
        animator.SetBool("attacking", true);
    }

    void EndAttackBow()
    {
        animator.SetBool("attackBow", false);
        currentState = PlayerState.walk;
        canMove = true;
        shooting.canShootagain();
    }

    private void AttackBowCo()
    {
        animator.SetBool("attackBow", true);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void HitPlayer() {
        health--;
        dmgsound.Play();
        Debug.Log("HealthPlayer subtracted, total: " + health);
        if (health == 0) {
            becauseText.text = "because: Health Over";
            HighscoreUpdate();
            Debug.Log("Player Dead");
            Time.timeScale = 0f;
            Die();
        }
    }

    public void HealPlayer() {
        if (health < 5) {
            health++;
        }
        Debug.Log("HealthPlayer added, total: " + health);
    }

    public int getHealth() {
        return health;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    private void DisplayHighscore() {     
        if(PlayerPrefs.HasKey("highscore")){
            highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore");
        }else{
            highscoreText.text = "Highscore: " + 0;
            PlayerPrefs.SetInt("highscore", 0);
            PlayerPrefs.Save();
        }
    }

    public void HighscoreUpdate() {
        if(score > PlayerPrefs.GetInt("highscore")){
            PlayerPrefs.SetInt("highscore", score);
        }
        PlayerPrefs.Save();
    }
    // Eine Methode für das beenden des Games
    public void Die() {
        scoreT.text = "score: " + score;
        youDiedUI.SetActive(true);
        Time.timeScale = 0;
    }

}
