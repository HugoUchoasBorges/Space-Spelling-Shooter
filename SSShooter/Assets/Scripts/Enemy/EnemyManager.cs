using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Variables

    private const string EnemyPath = "Prefabs/Enemy/Enemy";

    #endregion

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, 2f);
    }

    private void SpawnEnemy()
    {
        GameObject enemyObject = Resources.Load<GameObject>(EnemyPath);

        Instantiate(enemyObject, transform);
    }

    public void DestroyEnemy(GameObject enemy)
    {
        Destroy(enemy.GetComponent<EnemyDisplay>().panel);
        Destroy(enemy);
    }
}
