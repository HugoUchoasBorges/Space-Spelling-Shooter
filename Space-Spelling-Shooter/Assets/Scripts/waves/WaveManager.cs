using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static List<int> wpm;
    public static float dynamicWPM;
    private static float startTime;
    public static List<float> accuracy;

    private static List<int> typedLetters; 
    public static List<int> TypedLetters
    {
        get { return typedLetters; }

        private set
        {
            typedLetters = value;
            UpdateAccuracy();
        }
    }
    public static List<int> typedCorrectLetters { get; private set; }

    private static int[] typedCorrectLettersAccuracy;
    private static int[] typedLettersAccuracy;

    private static int[] typedCorrectLettersWPM;
    private static int[] typedLettersWPM;

    private static int accuracyTimeCount;
    private static int wpmTimeCount;

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
        wpm = new List<int>();
        accuracy = new List<float>();

        typedLetters = new List<int>();
        typedCorrectLetters = new List<int>();
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

        accuracy.Add(0f);
        typedCorrectLettersAccuracy = new int[2] { 0, 0 };
        typedLettersAccuracy = new int[2] { 0, 0 };

        typedLetters.Add(0);
        typedCorrectLetters.Add(0);
        accuracyTimeCount = 0;

        if (Wave == 1)
            wpm.Add(0);
        else
            wpm.Add(wpm[Wave - 2]);

        wpmTimeCount = 0;
        typedCorrectLettersWPM = new int[2] { 0, 0 };
        typedLettersWPM = new int[2] { 0, 0 };

        startTime = 0f;

        GameManager.ResumeGame();
    }

    public static void AddTypedLetter(bool correct = false)
    {
        if(correct == true)
        {
            typedCorrectLetters[Wave - 1]++;
        }

        List<int> letters = TypedLetters;
        letters[Wave - 1]++;
        TypedLetters = letters;
    }

    public static void UpdateAccuracy()
    {
        if (typedLetters[Wave - 1] == 0)
            return;

        accuracyTimeCount++;

        typedCorrectLettersAccuracy[1] = typedCorrectLetters[Wave - 1] - typedCorrectLettersAccuracy[0];
        typedLettersAccuracy[1] = typedLetters[Wave - 1] - typedLettersAccuracy[0];

        float newAccuracy = 100f * typedCorrectLettersAccuracy[1] / typedLettersAccuracy[1];
        float oldAccuracy = accuracy[Wave - 1];
        accuracy[Wave - 1] = (accuracy[Wave - 1] * (accuracyTimeCount - 1) + newAccuracy) / accuracyTimeCount;

        if ((newAccuracy != oldAccuracy) || (accuracyTimeCount == 1))
        {
            GUIController.RefreshGUI();
        }

        // NaN verification
        if (accuracy[Wave - 1] != accuracy[Wave - 1])
            accuracy[Wave - 1] = 0f;

        typedCorrectLettersAccuracy[0] = typedCorrectLettersAccuracy[1];
        typedLettersAccuracy[0] = typedLettersAccuracy[1];
    }

    public static void pauseWPMTimeCount()
    {
        startTime = 0f;
    }

    public static void UpdateWPM()
    {

        if(typedLetters[Wave - 1] == 0)
        {
            return;
        }

        if (startTime == 0)
        {
            startTime = Time.time;
            typedLettersWPM[0] = typedLetters[Wave - 1];
            typedCorrectLettersWPM[0] = typedCorrectLetters[Wave - 1];
            return;
        }

        float deltaTime = Time.time - startTime;

        if(deltaTime < 1f)
        {
            return;
        }

        wpmTimeCount++;

        typedLettersWPM[1] = typedLetters[Wave - 1];
        typedCorrectLettersWPM[1] = typedCorrectLetters[Wave - 1];

        int deltaTypedLettersWPM = typedLettersWPM[1] - typedLettersWPM[0];
        int deltaTypedCorrectLettersWPM = typedCorrectLettersWPM[1] - typedCorrectLettersWPM[0];

        //dynamicWPM = 60f * deltaTypedCorrectLettersWPM / (GlobalVariables.averageWordLength * deltaTime);
        dynamicWPM = 60f * deltaTypedCorrectLettersWPM / (5f * deltaTime);

        wpm[Wave - 1] = (int)(wpm[Wave - 1] * (wpmTimeCount - 1) + dynamicWPM) / (wpmTimeCount);

        // NaN verification
        if (wpm[Wave - 1] != wpm[Wave - 1])
            wpm[Wave - 1] = 0;

        typedCorrectLettersWPM[0] = typedCorrectLettersWPM[1];
        typedLettersWPM[0] = typedLettersWPM[1];

        startTime = Time.time;
    }

    public static void UpdateGlobalStatistics()
    {
        float globalAccuracy = 0;
        int globalWPM = 0;

        for (int w = 0; w < Wave; w++)
        {
            globalAccuracy += accuracy[w];
            globalWPM += wpm[w];
        }

        globalAccuracy /= Wave;
        globalWPM /= Wave;

        GlobalVariables.averageAccuracy = globalAccuracy;
        GlobalVariables.averageWPM = globalWPM;
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

        if (RemainingEnemies == 0)
            GlobalVariables.Instance.StartCoroutine(WaveTransition(GlobalVariables.waveTransitionTime));
    }

    private static IEnumerator WaveTransition(float countdownTime)
    {
        GameManager.PauseGame();
        UpdateGlobalStatistics();

        GameManager.ShowWaveTransition();
        yield return new WaitForSeconds(1);
        
        Text countdown = GameObject.FindGameObjectWithTag("GUITimeCountDown").GetComponent<Text>();
        for (int i = (int)countdownTime; i > 0; i--)
        {
            countdown.text = i.ToString() + "...";
            yield return new WaitForSeconds(1);
        }

        GameManager.HideWaveTransition();
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

    void FixedUpdate()
    {
        if(!GameManager.GAME_ISPAUSED && typedLettersWPM != null && Wave != 0)
            UpdateWPM();
    }
}
