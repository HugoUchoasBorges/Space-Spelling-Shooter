using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    #region Variables

    private const string EnemyPath = "Prefabs/Enemy/Enemy";

    public List<EnemyDisplay> activeEnemies;
    [FormerlySerializedAs("EnemiesFrequency")]
    [SerializeField]
    [Range(1f, 10f)] 
    private float enemiesFrequency = 2f;

    #endregion

    private void Start()
    {
        activeEnemies = new List<EnemyDisplay>();
        InvokeRepeating(nameof(SpawnEnemy), enemiesFrequency , enemiesFrequency );
    }

    private void SpawnEnemy()
    {
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
}
