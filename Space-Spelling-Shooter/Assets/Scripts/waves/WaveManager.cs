using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    private static int wave;
    public static int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            GUIController.RefreshGUI();
        }
    }
    public static bool activedWave;
    public static int enemiesCount;

    public static List<int> enemiesWaveCount;
    public static List<int> maxOnScreenEnemiesCount;
    public static List<int> defeatedEnemies;
    private static int remainingEnemies;
    public static int RemainingEnemies
    {
        get { return remainingEnemies; }
        set
        {
            remainingEnemies = value;
            GUIController.RefreshGUI();
        }
    }

    public static List<int> score;
    public static List<float> wpm;
    public static List<float> accuracy;

	// Use this for initialization
	void Awake () {
        Wave = 0;
        StopWaves();

        initializesVariables();

        StartCoroutine(manageWaves());
        StartCoroutine(GameManager.SpawnEnemies());
    }

    private void initializesVariables()
    {
        enemiesWaveCount = new List<int>();
        maxOnScreenEnemiesCount = new List<int>();
        defeatedEnemies = new List<int>();
        score = new List<int>();
        wpm = new List<float>();
        accuracy = new List<float>();
    }

    private IEnumerator manageWaves()
    {
        while (true)
        {
            yield return new WaitUntil(() => activedWave == true);

            NewWave();
            StopWaves();
        }
    }

    private void NewWave()
    {
        Wave += 1;

        int waveEnemiesCount = (Wave + 1) * 3;
        enemiesWaveCount.Add(waveEnemiesCount);

        int onScreenEnemiesCount = Wave + 2;
        maxOnScreenEnemiesCount.Add(onScreenEnemiesCount);
        defeatedEnemies.Add(0);
        RemainingEnemies = waveEnemiesCount;

        onScreenEnemiesCount = 0;
        enemiesCount = 0;
        

        score.Add(0);
        wpm.Add(0f);
        accuracy.Add(0f);

        GameManager.ResumeGame();
    }

    public static void AddEnemy()
    {
        enemiesCount++;
    }

    public static void RemoveEnemy()
    {
        defeatedEnemies[Wave - 1]++;
        GlobalVariables.DefeatedEnemiesCount++;

        RemainingEnemies--;

        AddOnScreenEnemy();

        IncreaseScore();
        
        if(RemainingEnemies == 0)
            WaveTransition();
    }

    private static void WaveTransition()
    {
        GameManager.PauseGame();

        GameManager.ResumeGame();
        StartWaves();
    }

    private static void AddOnScreenEnemy()
    {
        // Há cada totalInimigosWave/3 inimigos derrotados
        if (defeatedEnemies[Wave - 1] % (enemiesWaveCount[Wave - 1] / 3) == 0)
        {
            maxOnScreenEnemiesCount[Wave - 1]++;
        }
    }

    private static void IncreaseScore()
    {
        // Aumenta pontuação do jogador
        if(GlobalVariables.playerIsActive == true)
        {
            score[Wave - 1] += TypingSystem.word.Length * 10;
            GlobalVariables.ScoreCount += score[Wave - 1];
        }

    }

    public static void StopWaves()
    {
        activedWave = false;
    }

    public static void StartWaves()
    {
        activedWave = true;
    }

    public static bool AllowNewEnemy()
    {
        if ((GameManager.Enemies.Count < maxOnScreenEnemiesCount[Wave - 1]) && 
            (RemainingEnemies > 0) && enemiesCount < enemiesWaveCount[Wave - 1])
            return true;
        return false;
    }

    // Update is called once per frame
    void Update () {

	}
}
