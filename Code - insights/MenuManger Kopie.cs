using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManger : MonoBehaviour
{
    GameObject menu;
    GameObject helpUI;
    GameObject settingsUI;

    GameObject musicON;
    GameObject musicOFF;
    
    bool gameIsPaused = false;
    public static bool musicIsOn = true;
    
    public StartMenu startmenu;
    GameObject map;

    AudioSource audioGame;
    AudioSource audioPausenMenu;

    // Start is called before the first frame update
    void Start() {
        menu = GameObject.FindGameObjectWithTag("UI_StartMenu");
        helpUI = GameObject.FindGameObjectWithTag("UI_Help");
        settingsUI = GameObject.FindGameObjectWithTag("UI_Settings");
        musicON = GameObject.FindGameObjectWithTag("ON");
        musicOFF = GameObject.FindGameObjectWithTag("OFF");
        map = GameObject.FindGameObjectWithTag("Map");
    
        menu.SetActive(false);
        helpUI.SetActive(false);
        settingsUI.SetActive(false);

        audioGame = map.GetComponent<AudioSource>();
        audioPausenMenu = GetComponent<AudioSource>();
        audioGame.Play();
        audioPausenMenu.Play();
        audioPausenMenu.mute = true;

        musicIsOn = StartMenu.MusicIsOn; 
        if (!musicIsOn) {             // musik aus
           audioGame.mute = true;
           musicIsOn = false;
           musicON.SetActive(false);
           musicOFF.SetActive(true);
        }
      
    }

    void Update() {   
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (gameIsPaused) {
                    Resume(); 
                }
                else {                 
                    Pause();
                }
            }
    }

    public void Resume() {
        menu.SetActive(false);
        helpUI.SetActive(false);
        settingsUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        if(musicIsOn) {
            audioGame.mute = false;
            audioPausenMenu.mute = true;
        }
    }

    public void Pause() {
        menu.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        if(musicIsOn) {
            audioGame.mute = true;
            audioPausenMenu.mute = false;
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameIsPaused = false;

        if(musicIsOn) {
            audioGame.mute = false;
            audioPausenMenu.mute = true;
        }
    }

    public void Exit() {
        Debug.Log("Das Game übergibt dem Startmenü: " + MusicIsOnGame);
        SceneManager.LoadScene("StartMenu");
    }

    public void Help() {
        helpUI.SetActive(true);
    }

    public void Settings() {
        settingsUI.SetActive(true);
        if (musicIsOn) {
            musicON.SetActive(true);
        }
        else {
            musicOFF.SetActive(true);
        }
    }

    // Settings & Help UIS
    public void Home() {
        settingsUI.SetActive(false);
        helpUI.SetActive(false);
    }

    public void Music() {
        if(musicIsOn) {
            // -> Musik ausschalten
            musicOFF.SetActive(true);
            musicON.SetActive(false);
            musicIsOn = false; 
            audioPausenMenu.mute = true;
        }
        else {
            // -> Musik anschalten
            musicON.SetActive(true);
            musicOFF.SetActive(false);
            musicIsOn = true; 
            audioPausenMenu.mute = false;
        }
    }

    public static bool MusicIsOnGame { 
        get { return musicIsOn; }
    }
}

   