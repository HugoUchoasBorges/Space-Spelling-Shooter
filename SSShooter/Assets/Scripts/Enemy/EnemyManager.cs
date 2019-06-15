using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    
    // Components
    private WordLoader _wordLoader;
    private GuiController _guiController;

    private const string EnemyPath = "Prefabs/Enemy/Enemy";

    // Active Enemies Info
    [Header("Active Enemies Info________________")]
    [SerializeField]
    [Range(1f, 10f)] 
    private float enemiesFrequency = 2f;
    public List<EnemyDisplay> activeEnemies;
    
    [Space]
    [Header("Defeated Enemies Info________________")]
    // Defeated Enemies Info
    [SerializeField]
    private List<Enemy> defeatedEnemies;

    [SerializeField] private int totalEnemiesDefeated;
    [SerializeField] private int totalPointsObtained;
    [SerializeField] private int totalCharsTyped;

    #endregion

    private void Awake()
    {
        _wordLoader = GetComponent<WordLoader>();

        Assert.IsNotNull(_wordLoader);
    }

    private void Start()
    {
        activeEnemies = new List<EnemyDisplay>();
        defeatedEnemies = new List<Enemy>();
        totalEnemiesDefeated = 0;
        totalPointsObtained = 0;
        totalCharsTyped = 0;
        
        _guiController = GlobalVariables.GuiController;
        Assert.IsNotNull(_guiController);
        
        UpdateGuiInfoEnemyManager();
            
        InvokeRepeating(nameof(SpawnEnemy), enemiesFrequency , enemiesFrequency );
    }

    private void SpawnEnemy()
    {
        if (!_wordLoader || GetAvailableLetters().Length == 0)
            return;
     
        GameObject enemyObject = Resources.Load<GameObject>(EnemyPath);
        GameObject newEnemy = Instantiate(enemyObject, transform);
        
        EnemyDisplay enemyDisplay = newEnemy.GetComponent<EnemyDisplay>();
        if(enemyDisplay)
            activeEnemies.Add(enemyDisplay);
    }

    public void DestroyEnemy(GameObject enemyGameObject)
    {
        EnemyDisplay enemyDisplay = enemyGameObject.GetComponent<EnemyDisplay>();
        Enemy enemy = enemyDisplay.enemy;
        
        activeEnemies.Remove(enemyDisplay);
        defeatedEnemies.Add(enemy);
        Destroy(enemyDisplay.panel);
        Destroy(enemyGameObject);

        if (!_guiController)
            return;

        totalEnemiesDefeated += 1;
        totalPointsObtained += enemy.CalculatePontuation();
        totalCharsTyped += enemy.GetWordLength();

        UpdateGuiInfoEnemyManager();
    }

    private void UpdateGuiInfoEnemyManager()
    {
        if (!_guiController)
            return;
        
        _guiController.UpdateGuiInfo(
            points:totalPointsObtained.ToString(),
            enemiesDefeated:totalEnemiesDefeated.ToString(),
            charsTyped:totalCharsTyped.ToString()
        );
    }

    public string[] GetAvailableLetters()
    {   
        string[] allLetters = _wordLoader.wordCollection.allLetters;
        
        List<string> usingLetters = new List<string>();
        foreach (EnemyDisplay enemy in activeEnemies)
        {
            usingLetters.Add(enemy.enemy.word.text.Substring(0, 1));
        }

        return allLetters.Where(x=>!usingLetters.ToArray().Contains(x)).ToArray();
    }
}
