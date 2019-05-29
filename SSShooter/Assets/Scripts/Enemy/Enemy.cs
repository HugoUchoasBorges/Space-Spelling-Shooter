using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string word = "Enemy";
    public float speed = 1f;
    [Range(0.5f, 2f)] public float scale = 1f;

    #endregion
}
