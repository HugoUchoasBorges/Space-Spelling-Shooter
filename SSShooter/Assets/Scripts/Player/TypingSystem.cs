using System;
using UnityEngine;
using UnityEngine.Assertions;

public class TypingSystem : MonoBehaviour
{
    #region Variables

    // Enemies
    private EnemyManager _enemyManager;
    private EnemyDisplay _selectedEnemy;

    #endregion

    private void Awake()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("EnemyManager");
        if (!enemy)
            throw new Exception("No GameObject with 'EnemyManager' TAG found in this scene." +
                                "\nTyping System will be disabled");
        
        _enemyManager = enemy.GetComponent<EnemyManager>();
        
        Assert.IsNotNull(_enemyManager, "No EnemyManager GameObject found in this Scene");
    }

    public void TypeChar(string input)
    {
        // Return key
        if (input == "\r")
        {
            DeselectEnemy();
        }
        if (!_selectedEnemy)
            FindEnemy(input);
        
        Shoot(input);
    }

    public void DeselectEnemy()
    {
        if (!_selectedEnemy)
            return;
        
        _selectedEnemy.FillEnemyWord();
        _selectedEnemy = null;
    }

    private void FindEnemy(string input)
    {
        EnemyDisplay[] activeEnemies = _enemyManager.activeEnemies.ToArray();
        EnemyDisplay[] foundEnemies = Array.FindAll(activeEnemies,
            enemy => enemy.Word.StartsWith(input));

        if (foundEnemies.Length == 0)
            return;

        _selectedEnemy = foundEnemies[0];
    }

    private void Shoot(string input)
    {
        if (!_selectedEnemy)
            return;

        _selectedEnemy.ConsumeLetter(input);
    }
}
