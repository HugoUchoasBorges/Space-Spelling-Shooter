using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Variables

    private const string EnemyPath = "Prefabs/Enemy/Enemy";

    public List<EnemyDisplay> activeEnemies;

    #endregion

    private void Start()
    {
        activeEnemies = new List<EnemyDisplay>();
        InvokeRepeating(nameof(SpawnEnemy), 2f, 2f);
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
