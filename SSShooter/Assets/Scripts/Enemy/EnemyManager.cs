using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    
    // Components
    private WordLoader _wordLoader;

    private const string EnemyPath = "Prefabs/Enemy/Enemy";

    [Header("EnemyController Info________________")]
    public bool spawnEnemies;
    [SerializeField]
    private float enemiesFrequency = 2f;
    
    [Header("GUI Elements__________________")]
    public GuiController[] guiControllers;

    [Header("Active Enemies Info________________")]
    [Range(1f, 10f)] 
    public List<EnemyDisplay> activeEnemies;
    
    [Header("Defeated Enemies Info________________")]
    [SerializeField]
    private List<Enemy> defeatedEnemies;

    private int _totalEnemiesDefeated;
    private int _totalPointsObtained;
    private int _totalCharsTyped;

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
        _totalEnemiesDefeated = 0;
        _totalPointsObtained = 0;
        _totalCharsTyped = 0;
        
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
//            enemyDisplay.InitializeEnemy();
            return enemyDisplay;
        }

        return null;
    }

    public void RemoveEnemy(GameObject enemyGameObject)
    {
        EnemyDisplay enemyDisplay = enemyGameObject.GetComponent<EnemyDisplay>();
        Enemy enemy = enemyDisplay.enemy;
        
        activeEnemies.Remove(enemyDisplay);
        defeatedEnemies.Add(enemy);

        if (!enemy)
            return;
        
        _totalEnemiesDefeated += 1;
        _totalPointsObtained += enemy.CalculatePontuation();
        _totalCharsTyped += enemy.GetWordLength();

        UpdateGuiInfoEnemyManager();
    }

    public void DestroyEnemy(GameObject enemyGameObject)
    {
        enemyGameObject.GetComponent<EnemyDisplay>().ToDestroy();
    }

    private void UpdateGuiInfoEnemyManager()
    {
        GuiController.InvokeMulti(this, guiControllers);
    }

    public string[] GetAvailableLetters()
    {
        if (!_wordLoader)
            return null;
        
        string[] allLetters = _wordLoader.wordCollection.allLetters;
        
        List<string> usingLetters = new List<string>();
        foreach (EnemyDisplay enemy in activeEnemies)
        {
            if (!enemy.enemy)
                continue;
            
            usingLetters.Add(enemy.enemy.word.text.Substring(0, 1));
        }

        return allLetters.Where(x=>!usingLetters.ToArray().Contains(x)).ToArray();
    }
}
