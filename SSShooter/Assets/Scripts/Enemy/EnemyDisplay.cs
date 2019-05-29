﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour
{
    #region Variables

    // Scriptable Enemy Reference
    public Enemy enemy;

    [Space]
    [Header("Canvas Components")]
    public Image panel;
    public Text text;

    #endregion

    private void Awake()
    {
        Assert.IsNotNull(enemy, "The Enemy " + gameObject.name + " must have an EnemyObject reference");
        Assert.IsNotNull(panel, "The Enemy " + gameObject.name + " must have an Canvas Panel reference");
        Assert.IsNotNull(text, "The Enemy " + gameObject.name + " must have an Canvas Text reference");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (enemy && text && panel)
        {
            FillEnemyWord();
            SetEnemyScale();
        }
    }

    private void FillEnemyWord()
    {
        text.text = enemy.word;
    }

    private void SetEnemyScale()
    {
        transform.localScale = enemy.scale * Vector3.one;
        panel.rectTransform.localScale = transform.localScale;
    }
}
