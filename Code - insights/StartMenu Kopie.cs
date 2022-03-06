using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    GameObject menu;
    GameObject helpUI;
    GameObject settingsUI;

    GameObject musicON;
    GameObject musicOFF;

    public static bool musicIsOn = true;

    public MenuManger menumanager;
    AudioSource sound;

    void Start() {
        menu = GameObject.FindGameObjectWithTag("UI_StartMenu");
        helpUI = GameObject.FindGameObjectWithTag("UI_Help");
        settingsUI = GameObject.FindGameObjectWithTag("UI_Settings");
        musicON = GameObject.FindGameObjectWithTag("ON");
        musicOFF = GameObject.FindGameObjectWithTag("OFF");

        menu.SetActive(true);
        helpUI.SetActive(false);
        settingsUI.SetActive(false);

        sound = GetComponent<AudioSource>();
        sound.Play();

        musicIsOn = MenuManger.MusicIsOnGame; 
        SetMusicStatus(musicIsOn);
    }

    void Update() {
 
    }

    public void StartGame() {
        Debug.Log("Das Startmenu übergibt dem Game: " + MusicIsOn);
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void Help() {
        helpUI.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }

    public void Settings() {
        settingsUI.SetActive(true);
    }

    // Settings & Help UIS
    public void Home() {
        settingsUI.SetActive(false);
        helpUI.SetActive(false);
    }

    public void Music()
    {
        if(musicIsOn) {
            // Musik an -> also ausschalten
            SetMusicStatus(false);
        }
        else {
            // Musik aus -> also anschalten
            SetMusicStatus(true);
        }
    }

    public void SetMusicStatus(bool statusMusicOn){
        
        if (statusMusicOn) {            // Musik ist an
            musicIsOn = true;
            sound.mute = false;
            musicON.SetActive(true);
            musicOFF.SetActive(false);
        }
        else {                          // Musik ist aus
            musicIsOn = false;
            sound.mute = true;
            musicON.SetActive(false);
            musicOFF.SetActive(true);
        }
        
    }

    public static bool MusicIsOn { 
        get { return musicIsOn; }
    }

}

   