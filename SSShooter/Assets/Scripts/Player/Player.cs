using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Variables
    
    // Components
    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _playerMovement;
    private TypingSystem _typingSystem;
    private AudioManager _audioManager;

    private int _lives = 3;
    
    public Color respawnColor = Color.red;
    [Range(0.5f, 3f)] public float respawnTimeSec = 3f;
    [Range(0.5f, 3f)] public float respawnIntangibleTimeSec = 3f;
    public LayerMask intangibleLayer;

    [Header("GUI Elements__________________")]
    public GuiController[] guiControllers;

    [Header("Player State Variables__________________")]
    [HideInInspector]public bool isDead;
    
    [Header("Audio Variables__________________")]
    public string playerDyingAudio;
    
    #endregion

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        _typingSystem = GetComponent<TypingSystem>();
        _audioManager = GetComponent<AudioManager>();

        Assert.IsNotNull(_spriteRenderer);
        Assert.IsNotNull(_playerMovement);
        Assert.IsNotNull(_typingSystem);
        Assert.IsNotNull(_audioManager);
    }

    private void Start()
    {
        UpdateGuiInfoPlayer();
    }

    public void Death()
    {
        if (_audioManager)
            _audioManager.Play(playerDyingAudio);
        
        isDead = true;
        
        _lives = Mathf.Max(0, _lives-1);
        UpdateGuiInfoPlayer();
            
        if (_lives == 0)
        {
            GameOver();
        }

        if (!_spriteRenderer)
            return;

        StartCoroutine(Respawn());
        _typingSystem.DeselectEnemy();
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private IEnumerator Respawn()
    {
        GameObject gObject = gameObject;
        
        int oldLayer = gObject.layer;
        gObject.layer = (int) (Mathf.Log(intangibleLayer.value) / Mathf.Log(2));
        
        Color oldColor = _spriteRenderer.color;
        _spriteRenderer.enabled = false;

        yield return new WaitForSeconds(respawnTimeSec);
        
        if (_playerMovement)
        {
            _playerMovement.SetStartPosition();   
        }  
        
        // ReSharper disable once Unity.InefficientPropertyAccess
        _spriteRenderer.enabled = true;
        _spriteRenderer.color = respawnColor;

        yield return new WaitForSeconds(respawnIntangibleTimeSec);

        gObject.layer = oldLayer;
        // ReSharper disable once Unity.InefficientPropertyAccess
        _spriteRenderer.color = oldColor;
        isDead = false;
    }

    private void UpdateGuiInfoPlayer()
    {
        GuiController.InvokeMulti(this, guiControllers);
    }
}
