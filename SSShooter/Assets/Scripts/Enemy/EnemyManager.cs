using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    
    // Components
    private WordLoader _wordLoader;
    private GuiController _guiController;

    private const string EnemyPath = "Prefabs/Enemy/Enemy";

    [Header("EnemyController Info________________")]
    public bool spawnEnemies;
    [SerializeField]
    private float enemiesFrequency = 2f;

    [Header("Active Enemies Info________________")]
    [Range(1f, 10f)] 
    public List<EnemyDisplay> activeEnemies;
    
    [Header("Defeated Enemies Info________________")]
    [SerializeField]
    private List<Enemy> defeatedEnemies;

    [SerializeField] private int totalEnemiesDefeated;
    [SerializeField] private int totalPointsObtained;
    [SerializeField] private int totalCharsTyped;

    #endregion

    private void Awake()
    {
        _wordLoader = GetComponent<WordLoader>();

        GlobalVariables.EnemyManager = this;

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
            
        if(spawnEnemies) 
            InvokeRepeating(nameof(SpawnEnemy), enemiesFrequency , enemiesFrequency );
    }

    public EnemyDisplay SpawnEnemy()
    {
        if (!_wordLoader || GetAvailableLetters().Length == 0)
            return null;
     
        GameObject enemyObject = Resources.Load<GameObject>(EnemyPath);
        GameObject newEnemy = Instantiate(enemyObject, transform);
        
        EnemyDisplay enemyDisplay = newEnemy.GetComponent<EnemyDisplay>();
        if (enemyDisplay)
        {
            activeEnemies.Add(enemyDisplay);
            enemyDisplay.InitializeEnemy();
            return enemyDisplay;
        }

        return null;
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
