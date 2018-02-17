﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : Personagem {

    private GameSetting gameSettings;

    void OnEnable()
    {
        gameSettings = new GameSetting();

        LoadSettings();

    }

    public void NewGame()
    {
        PlayAudioSelect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadSettings()
    {
        Resolution[] resolutions = Screen.resolutions;
        try
        {
            string file = File.ReadAllText(Application.persistentDataPath + "/gamesettings.json");
            gameSettings = JsonUtility.FromJson<GameSetting>(file);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Log("Gerando configurações padrão");

            gameSettings.musicVolume = 1f;
            gameSettings.fullScreen = true;
            gameSettings.antiAliasing = true;
            gameSettings.resolutionIndex = resolutions.Length - 1;
        }

        GlobalVariables.VOLUME = gameSettings.musicVolume;
        Screen.SetResolution(resolutions[gameSettings.resolutionIndex].width, resolutions[gameSettings.resolutionIndex].height, Screen.fullScreen);
        Screen.fullScreen = gameSettings.fullScreen;

        if (gameSettings.antiAliasing)
            QualitySettings.antiAliasing = (int)Mathf.Pow(2f, 4f);
        else
            QualitySettings.antiAliasing = (int)Mathf.Pow(2f, 1f);

    }

    protected override void Start()
    {
        base.Start();
        inicializaAudios(GlobalVariables.audio_game);
    }

    public void PlayGame()
    {
        PlayAudioSelect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayAudioSelect()
    {
        PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
    }

    public void PlayAudioBack()
    {
        PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
    }

    public void QuitGame()
    {
        PlayAudioSelect();
        Debug.Log("Quit");
        Application.Quit();
    }

    public void QuitToMainMenu()
    {
        PlayAudioSelect();
        Debug.Log("Quit to Main Menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //controls the pausing of the scene
    public void Resume()
    {
        PlayAudioSelect();
        GerenciadorJogo.pauseControl();
    }
}
