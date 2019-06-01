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

    [Space]
    [Header("Canvas Components")]
    public GameObject panel;
    private Text _text;
    private Canvas _canvas;

    #endregion

    private void Awake()
    {
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
    }
    // Start is called before the first frame update
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

    private void FillEnemyWord()
    {
        _text.text = enemy.word.ToUpper();
    }

    private void SetEnemyScale()
    {
        Transform enemyTransform = transform;
        
        enemyTransform.localScale = enemy.scale * Vector3.one;

        // TODO: Scale Panel
    }
    
}
