﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static List<GameObject> enemies;
    public static List<GameObject> Enemies
    {
        get { return enemies; }
        private set
        {
            if(enemies != null)
                WaveManager.AddEnemy();

            enemies = value;
        }
    }

    public static Player player;

    public static bool GAME_ISPAUSED = true;

    // Pause Menu Objects
    public static GameObject[] pauseObjects;

    // GameOver menu Objects
    public static GameObject[] deathObjects;

    // GUI Menu Objects
    public static GameObject[] guiObjects;

    // WaveManager
    public static WaveManager manageWaves;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        HidePaused();

        deathObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");
        HideGameOver();

        guiObjects = GameObject.FindGameObjectsWithTag("GUI");
        HideGUI();

        manageWaves = gameObject.AddComponent<WaveManager>();

        StartGame();
    }

    public void StartGame(){

        gameObject.AddComponent<EdgeGenerator>();

        WordGenerator.FillWords();

        // InstantiatesPlayer
        player = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.spaceship1Player]).GetComponent<Player>();

        // Ignore collisions between enemies
        Physics2D.IgnoreLayerCollision(GlobalVariables.LAYER_INIMIGOS, GlobalVariables.LAYER_INIMIGOS);

        Enemies = new List<GameObject>();

        WaveManager.StartWaves();

        ShowGUI();
    }

    public static void PauseGame()
    {
        GAME_ISPAUSED = true;
        WaveManager.pauseWPMTimeCount();
    }

    public static void ResumeGame()
    {
        GAME_ISPAUSED = false;
    }

    //Reloads the Level
    public static void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //controls the pausing of the scene
    public static void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
            Time.timeScale = 0;
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
            Time.timeScale = 1;
            HidePaused();
        }
    }

    //hides objects with ShowOnPause tag
    public static void HidePaused()
    {
        GameManager.ResumeGame();
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with ShowOnPause tag
    public static void ShowPaused()
    {
        GameManager.PauseGame();

        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public static void DestroyEnemy(GameObject enemy, char firstChar)
    {

        WaveManager.RemoveEnemy();

        GlobalVariables.RemoveUsedChar(firstChar);
        Enemies.Remove(enemy);
        enemy.transform.localScale = Vector3.zero;
        enemy.GetComponent<CircleCollider2D>().enabled = false;
        float audioTimeLength = enemy.GetComponent<Enemy>().PlayAudio(GlobalVariables.ENUM_AUDIO.enemy_dying);        

        Destroy(enemy, audioTimeLength);
    }

    public static GameObject FindTarget(char c)
    {
        foreach (GameObject enemy in Enemies)
        {
            if (enemy.GetComponentInChildren<Text>().text[0] == c)
            {
                return enemy;
            }
        }
        return null;
    }

    public static IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitUntil(() => GAME_ISPAUSED == false);

            if (!GlobalVariables.usedChars.Values.Contains(false))
                yield return new WaitUntil(() => GlobalVariables.usedChars.Values.Contains(false) == true);

            yield return new WaitForSeconds(GlobalVariables.spawnEnemyTime);

            if (WaveManager.AllowNewEnemy())
            {
                GameObject enemy = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.defaultEnemy]) as GameObject;

                List<GameObject> enemies = Enemies;
                enemies.Add(enemy);

                Enemies = enemies;

            }
            
        }
    }

    public static void GameOVer()
    {
        player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_dying);
        Time.timeScale = 0;
        ShowGameOver();
    }

    public static void ResetGlobalVariables()
    {
        GlobalVariables.ScoreCount = 0;
        GlobalVariables.averageAccuracy = 0f;
        GlobalVariables.averageWPM = 0f;
        GlobalVariables.DefeatedEnemiesCount = 0;
    }

    //shows objects with ShowOnDeath tag
    public static void ShowGameOver()
    {
        foreach (GameObject g in deathObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnDeath tag
    public static void HideGameOver()
    {
        foreach (GameObject g in deathObjects)
        {
            g.SetActive(false);
        }
    }

    public static void HideGUI()
    {
        foreach (GameObject g in guiObjects)
        {
            g.SetActive(false);
        }
    }

    public static void ShowGUI()
    {
        foreach (GameObject g in guiObjects)
        {
            g.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update () {

        // Uses the ESC Key to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
                Time.timeScale = 0;
                ShowPaused();
            }
            else if (Time.timeScale == 0)
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
                Time.timeScale = 1;
                HidePaused();
            }
        }
    }
}
