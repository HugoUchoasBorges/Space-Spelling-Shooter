using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
    #region Variables

    // Components
    [Header("Main Variables______________________")]
    public Word word;

    [Range(0f, 2f)]
    public float speed = 0.6f;

    [Range(0f, 2f)]
    public float maxInitialTorque = 0.5f;
        
    [Header("Main Components______________________")]
    [HideInInspector]
    public EnemyManager enemyManager;
    [HideInInspector]
    public WordLoader wordLoader;
    private EnemyMovement _enemyMovement;
    private AudioManager _audioManager;

    [Header("Canvas Components___________________________")]
    public GameObject canvasPanel;
    public HealthBar healthBar;
    public TextMeshProUGUI text;

    public string Word { get; private set; } = "";

    [Header("Audio Names___________________________")] 
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
        Assert.IsNotNull(wordLoader, "The Enemy must have an WordLoader reference");
    }

    private void Start()
    {
        string[] availableLetters = enemyManager.GetAvailableLetters();
        
        if(availableLetters != null)
        {
            string letter = availableLetters[Random.Range(0, availableLetters.Length)];
            LoadRandomWord(letter);
        }
        
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
        return canvasPanel && text;
    }

    #region Initializing Methods

    private void InitializeEnemy()
    {
        if (CheckForRun())
        {            
            FillEnemyWord();
//            SetEnemyScale();
            
            if(_enemyMovement)
                _enemyMovement.StartEnemyPositionMovement();
        }
    }
    
    public void FillEnemyWord()
    {
        Word = word.text.ToUpper();
        text.text = Word;
        if (healthBar)
            healthBar.SetHealthBarAmount(100);
    }

//    private void SetEnemyScale()
//    {
//        Transform enemyTransform = transform;
//        
//        enemyTransform.localScale = scale * Vector3.one;
//
//        if (canvasPanel && enemy)
//        {
//            Image panelImage = canvasPanel.GetComponent<Image>();
//            if(panelImage)
//            {
//                panelImage.sprite = enemy.sprite;
//                panelImage.SetNativeSize();
//            }
//        }
//    }

    #endregion

    #region Action Methods

    public bool ConsumeLetter(string letter)
    {
        if (!Word.StartsWith(letter))
            return false;

        Word = Word.Substring(1);
        if(healthBar)
        {
            float healthAmount = Word.Length / (float) word.text.Length;
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

    public int CalculatePontuation()
    {
        int points = word.text.Length;

        return points;
    }

    public int GetWordLength()
    {
        return word.text.Length;
    }

    private void LoadRandomWord(string letter="")
    {
        if (wordLoader != null) 
            word = wordLoader.wordCollection.GetRandomWordWithLetter(letter);
    }

}
