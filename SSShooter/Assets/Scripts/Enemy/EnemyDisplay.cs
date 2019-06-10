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
    public EnemyManager enemyManager;
    public WordLoader wordLoader;

    [Space]
    [Header("Canvas Components")]
    public GameObject panel;
    private Text _text;
    private Canvas _canvas;

    public string Word { get; private set; } = "";

    #endregion

    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        wordLoader = GetComponentInParent<WordLoader>();
        
        if (enemyTemplate)
        {
            string[] availableLetters = enemyManager.GetAvailableLetters();
            
            string letter = availableLetters[Random.Range(0, availableLetters.Length)];
            enemy = ScriptableObject.CreateInstance<Enemy>().Init(enemyTemplate, letter, wordLoader);
        }
        
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
        Assert.IsNotNull(enemyManager, "The Enemy " + gameObject.name + " must have an EnemyManager reference");
        Assert.IsNotNull(enemyTemplate, "The Enemy " + gameObject.name + " must have an EnemyTemplate reference");
        Assert.IsNotNull(wordLoader, "The Enemy " + gameObject.name + " must have an WordLoader reference");
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

    public void FillEnemyWord()
    {
        Word = enemy.word.text.ToUpper();
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
            enemyManager.DestroyEnemy(gameObject);
        }
        
        _text.text = Word;
        
        return true;
    }

    #endregion
    
}
