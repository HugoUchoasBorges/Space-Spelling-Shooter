using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour
{
    #region Variables

    // Scriptable Enemy Reference
    public Enemy enemy;
    
    // Components
    public EnemyManager EnemyManager;

    [Space]
    [Header("Canvas Components")]
    public GameObject panel;
    private Text _text;
    private Canvas _canvas;

    public string Word { get; private set; } = "";

    #endregion

    private void Awake()
    {
        EnemyManager = GetComponentInParent<EnemyManager>();
        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        if (_canvas)
        {
            panel = Instantiate(panel, _canvas.transform);
            _text = panel.GetComponentInChildren<Text>();
        }

        Assert.IsNotNull(_canvas);
        Assert.IsNotNull(enemy, "The Enemy " + gameObject.name + " must have an EnemyObject reference");
        Assert.IsNotNull(panel, "The Enemy " + gameObject.name + " must have an Canvas Panel reference");
        Assert.IsNotNull(_text, "The Enemy " + gameObject.name + " must have an Canvas Text reference");
        Assert.IsNotNull(EnemyManager, "The Enemy " + gameObject.name + " must have an EnemyManager reference");
    }
    
    private void Start()
    {
        if (CheckForRun())
        {
            FillEnemyWord();
            SetEnemyScale();
        }
    }

    public bool CheckForRun()
    {
        return _canvas && enemy && panel && _text;
    }

    #region Initializing Methods

    private void FillEnemyWord()
    {
        Word = enemy.word.ToUpper();
        _text.text = Word;
    }

    private void SetEnemyScale()
    {
        Transform enemyTransform = transform;
        
        enemyTransform.localScale = enemy.scale * Vector3.one;

        // TODO: Scale Panel
    }

    #endregion

    #region Action Methods

    public bool ConsumeLetter(string letter)
    {
        if (!Word.StartsWith(letter))
            return false;

        Word = Word.Substring(1);

        if (Word == "")
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            EnemyManager.DestroyEnemy(gameObject);
        }
        
        _text.text = Word;
        
        return true;
    }

    #endregion
    
}
