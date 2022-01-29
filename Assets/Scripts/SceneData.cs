﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    public static SceneData _instance;
    public AudioSource S_BGM;
    public AudioSource S_BtnClick;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;
        if(_instance == null) {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
        if(PlayerPrefs.HasKey("UserName")) {
            SceneManager.LoadScene("Main");
        }
        else {
           return;
        }
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name=="Game") {
            S_BGM.enabled = false;
        }
        else if(SceneManager.GetActiveScene().name !="Game"){
            if(!PlayerPrefs.HasKey("S_BGM") &&!PlayerPrefs.HasKey("S_BtnClick")) {
                PlayerPrefs.SetFloat("S_BGM",1.0f);
                PlayerPrefs.SetFloat("S_BtnClick",1.0f);
            }
            else {
                S_BGM.volume = PlayerPrefs.GetFloat("S_BGM");
                S_BGM.enabled = true;
            }
        }
    }
}
