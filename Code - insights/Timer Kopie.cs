using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
public class Timer : MonoBehaviour
{
    private Text TimerText;
    public float startTime;
    private float startTimer;
    public float time;
    private bool started = false;
    private bool finnished = false;
    public PlayerMovement player;

    public static float t;

    void Start() {
        TimerText = GetComponent<Text>();
        StartTime();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update() {
        time += Time.deltaTime;
        
        if (started) {
            if (finnished) {
                return;
            }

            t = startTimer - time;
            

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f0");

            TimerText.text = minutes + ":" + seconds;

             if (t <= 0) {
                player.becauseText.text = "because: Time Over";
                Finnish();
                player.Die();               
             }
        }
    }

    public void resetTimer() {
        startTimer = time + startTime;
    }

    public void StartTime() {
        resetTimer();
        started = true;
        finnished = false;
    }

    public void Finnish() {
        started = false;
        finnished = true;
    }
}