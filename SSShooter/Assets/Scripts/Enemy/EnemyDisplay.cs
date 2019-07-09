using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyDisplay : MonoBehaviour
{
    #region Variables

    // Scriptable Enemy Reference
    public Enemy enemy;
    public Enemy enemyTemplate;
    
    // Components
    [HideInInspector]
    public EnemyManager enemyManager;
    [HideInInspector]
    public WordLoader wordLoader;
    private EnemyMovement _enemyMovement;
    private AudioManager _audioManager;

    [Header("Canvas Components")]
    public GameObject canvasPanel;
    public HealthBar healthBar;
    public TextMeshProUGUI text;

    public string Word { get; private set; } = "";

    [Header("Audio Names")] 
    public string enemyDyingAudio;
    public string enemyHitAudio;

    #endregion

    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        wordLoader = GetComponentInParent<WordLoader>();
        _audioManager = GetComponentInParent<AudioManager>();

        _enemyMovement = GetComponent<EnemyMovement>();
        text = canvasPanel.GetComponentInChildren<TextMeshProUGUI>();

        Assert.IsNotNull(_audioManager, "The Enemy don't seem to have a AudioManager");
        Assert.IsNotNull(healthBar, "The Enemy don't seem to have a HealthBar");
        Assert.IsNotNull(canvasPanel, "The Enemy must have a Canvas Panel reference");
        Assert.IsNotNull(text, "The Enemy must have a Canvas Text reference");
        Assert.IsNotNull(enemyManager, "The Enemy must have an EnemyManager reference");
        Assert.IsNotNull(enemyTemplate, "The Enemy must have an EnemyTemplate reference");
        Assert.IsNotNull(wordLoader, "The Enemy must have an WordLoader reference");
        Assert.IsNotNull(wordLoader, "The Enemy must have an EnemyMovement reference");
    }

    private void Start()
    {
        if (enemyTemplate)
        {
            string[] availableLetters = enemyManager.GetAvailableLetters();
            
            if(availableLetters != null)
            {
                string letter = availableLetters[Random.Range(0, availableLetters.Length)];
                enemy = ScriptableObject.CreateInstance<Enemy>().Init(enemyTemplate, letter, wordLoader);
            }
        }
        
        Assert.IsNotNull(enemy, "The Enemy must have an EnemyObject reference");

        InitializeEnemy();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_audioManager)
            return;
        
        if(other.gameObject.layer==LayerMask.NameToLayer(GlobalVariables.BULLET_LAYER) && 
           !other.gameObject.GetComponent<BulletController>().lastBullet)
            _audioManager.Play(enemyHitAudio);
    }

    private bool CheckForRun()
    {
        return enemy && canvasPanel && text;
    }

    #region Initializing Methods

    private void InitializeEnemy()
    {
        if (CheckForRun())
        {
            FillEnemyWord();
            SetEnemyScale();
            
            if(_enemyMovement)
                _enemyMovement.StartEnemyPositionMovement();
        }
    }
    
    public void FillEnemyWord()
    {
        Word = enemy.word.text.ToUpper();
        text.text = Word;
        if (healthBar)
            healthBar.SetHealthBarAmount(100);
    }

    private void SetEnemyScale()
    {
        Transform enemyTransform = transform;
        
        enemyTransform.localScale = enemy.scale * Vector3.one;

        if (canvasPanel && enemy)
        {
            Image panelImage = canvasPanel.GetComponent<Image>();
            if(panelImage)
            {
                panelImage.sprite = enemy.sprite;
                panelImage.SetNativeSize();
            }
        }
    }

    #endregion

    #region Action Methods

    public bool ConsumeLetter(string letter)
    {
        if (!Word.StartsWith(letter))
            return false;

        Word = Word.Substring(1);
        if(healthBar)
        {
            float healthAmount = Word.Length / (float) enemy.word.text.Length;
            healthBar.SetHealthBarAmount(healthAmount);
        }

        if (Word == "")
            enemyManager.RemoveEnemy(gameObject);
        
        text.text = Word;
        
        return true;
    }

    #endregion

    public void ToDestroy()
    {
        if (!_audioManager)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = 100 * Vector2.one;
        Destroy(gameObject, _audioManager.Play(enemyDyingAudio));
    }
}
