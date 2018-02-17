using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour {

    public Toggle fullScreenToggle;
    public Dropdown resolutionDropdown;
    public Slider musicVolumeSlider;
    public Toggle antiAliasingToggle;
    public Button applyButton;

    public Resolution[] resolutions;
    public GameSetting gameSettings;

    public void OnEnable()
    {
        gameSettings = new GameSetting();

        fullScreenToggle = GameObject.FindGameObjectWithTag("FullScreenToggle").GetComponent<Toggle>();
        resolutionDropdown = GameObject.FindGameObjectWithTag("ResolutionDropdown").GetComponent<Dropdown>();
        musicVolumeSlider = GameObject.FindGameObjectWithTag("VolumeSlider").GetComponent<Slider>();
        antiAliasingToggle = GameObject.FindGameObjectWithTag("AntiAliasingToggle").GetComponent<Toggle>();
        applyButton = GameObject.FindGameObjectWithTag("ApplyButton").GetComponent<Button>();

        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        antiAliasingToggle.onValueChanged.AddListener(delegate { OnAntiAliasingToggle(); });
        applyButton.onClick.AddListener(delegate { onApplyButtonClick(); });

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }

    public void OnFullScreenToggle()
    {
        gameSettings.fullScreen = Screen.fullScreen = fullScreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnMusicVolumeChange()
    {
        GlobalVariables.VOLUME = gameSettings.musicVolume = musicVolumeSlider.value; 
    }

    public void OnAntiAliasingToggle()
    {

        if (antiAliasingToggle.isOn)
            QualitySettings.antiAliasing = (int)Mathf.Pow(2f, 4f);
        else
            QualitySettings.antiAliasing = (int)Mathf.Pow(2f, 1f);

        gameSettings.antiAliasing = antiAliasingToggle.isOn;
    }

    public void onApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        try
        {
            string file = File.ReadAllText(Application.persistentDataPath + "/gamesettings.json");
            gameSettings = JsonUtility.FromJson<GameSetting>(file);
        }
        catch (FileNotFoundException e)
        {
            Debug.Log(e);
            Debug.Log("Gerando configurações padrão");

            gameSettings.musicVolume = 1f;
            gameSettings.fullScreen = true;
            gameSettings.antiAliasing = true;
            gameSettings.resolutionIndex = resolutions.Length - 1;
        }
        
        musicVolumeSlider.value = gameSettings.musicVolume;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullScreenToggle.isOn = gameSettings.fullScreen;
        antiAliasingToggle.isOn = gameSettings.antiAliasing;
        Screen.fullScreen = gameSettings.fullScreen;

        resolutionDropdown.RefreshShownValue();
    }

}
