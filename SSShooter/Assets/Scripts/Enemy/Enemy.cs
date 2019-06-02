using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    #region Variables

    // Basic Info
    [Header("Basic Info________________")]
    public new string name = "Enemy";
    [TextArea] public string description;
    public Sprite sprite;

    // Attributes
    [Space]
    [Header("Attributes________________")]
    public Word word;
    public float speed = 1f;
    [Range(0.5f, 2f)] public float scale = 1f;

    #endregion

    private void OnEnable()
    {
//        LoadRandomWord();
    }

    public Enemy Init(Enemy template)
    {
        
        // Template initialization
        name = template.name;
        description = template.description;
        sprite = template.sprite;

        speed = template.speed;
        scale = template.scale;
        
        LoadRandomWord();
        
        return this;
    }

    public Enemy Init(Enemy template, string letter)
    {
        throw new NotImplementedException();
    }

    private void LoadRandomWord()
    {
        word = WordLoader.GetRandomWord();
    }
        
}
