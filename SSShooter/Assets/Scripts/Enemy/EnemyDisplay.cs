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
    private void Start()
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
        Transform enemyTransform = transform;
        
        enemyTransform.localScale = enemy.scale * Vector3.one;

        // TODO: Scale Panel
    }
    
}
