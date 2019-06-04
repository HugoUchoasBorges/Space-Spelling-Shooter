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

    private const string EnemyPath = "Prefabs/Enemy/Enemy";

    public List<EnemyDisplay> activeEnemies;
    [FormerlySerializedAs("EnemiesFrequency")]
    [SerializeField]
    [Range(1f, 10f)] 
    private float enemiesFrequency = 2f;

    #endregion

    private void Awake()
    {
        _wordLoader = GetComponent<WordLoader>();
        
        Assert.IsNotNull(_wordLoader);
    }

    private void Start()
    {
        activeEnemies = new List<EnemyDisplay>();
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

    public void DestroyEnemy(GameObject enemy)
    {
        EnemyDisplay enemyDisplay = enemy.GetComponent<EnemyDisplay>();
        activeEnemies.Remove(enemyDisplay);
        Destroy(enemyDisplay.panel);
        Destroy(enemy);
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
