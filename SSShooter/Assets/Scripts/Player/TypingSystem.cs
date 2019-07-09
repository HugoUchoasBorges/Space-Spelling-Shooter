using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class TypingSystem : MonoBehaviour
{
    #region Variables

    // Enemies
    private EnemyManager _enemyManager;
    private EnemyDisplay _selectedEnemy;

    private Color _enemyOldColor;

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

    public EnemyDisplay TypeChar(string input)
    {
        // Return key
//        if (input == "\r")
//        {
//            DeselectEnemy();
//            return null;
//        }
        if (!_selectedEnemy || _selectedEnemy && _selectedEnemy.Word == "")
            FindEnemy(input);
        
        return Shoot(input);
    }

    private void FindEnemy(string input)
    {
        EnemyDisplay[] activeEnemies = _enemyManager.activeEnemies.ToArray();
        EnemyDisplay[] foundEnemies = Array.FindAll(activeEnemies,
            enemy => enemy.Word.StartsWith(input));

        if (foundEnemies.Length == 0)
            return;
        
        SelectEnemy(foundEnemies[0]);
    }

    private void SelectEnemy(EnemyDisplay enemy)
    {
        _selectedEnemy = enemy;
        _selectedEnemy.gameObject.layer = LayerMask.NameToLayer(GlobalVariables.SELECTED_ENEMY_LAYER);

        TextMeshProUGUI enemyText = _selectedEnemy.text;

        _enemyOldColor = enemyText.color;
        enemyText.color = Color.red;
    }
    
    public void DeselectEnemy()
    {
        if (!_selectedEnemy)
            return;
        
        _selectedEnemy.FillEnemyWord();
        _selectedEnemy.text.color = _enemyOldColor;
        _selectedEnemy.gameObject.layer = LayerMask.NameToLayer(GlobalVariables.ENEMY_LAYER);
        _selectedEnemy = null;
    }

    private EnemyDisplay Shoot(string input)
    {
        if (!_selectedEnemy)
            return null;

        return _selectedEnemy.ConsumeLetter(input) ? _selectedEnemy : null;
    }
}
