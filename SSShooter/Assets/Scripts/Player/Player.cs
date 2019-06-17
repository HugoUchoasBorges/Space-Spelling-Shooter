using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;


public class Player : MonoBehaviour
{
    #region Variables
    
    // Components
    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _playerMovement;
    private TypingSystem _typingSystem;
    private GuiController _guiController;

    [Header("Base Configuration________________")]
    [SerializeField]
    private int lives = 3;
    
    public Color respawnColor = Color.red;
    [Range(0.5f, 3f)] public float respawnTimeSec = 3f;
    [Range(0.5f, 3f)] public float respawnIntangibleTimeSec = 3f;
    public LayerMask intangibleLayer;

    [Header("Player State Variables__________________")]
    public bool isDead;
    
    #endregion

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        _typingSystem = GetComponent<TypingSystem>();

        Assert.IsNotNull(_spriteRenderer);
        Assert.IsNotNull(_playerMovement);
        Assert.IsNotNull(_typingSystem);
    }

    private void Start()
    {
        _guiController = GlobalVariables.GuiController;
        Assert.IsNotNull(_guiController);

        UpdateGuiInfoPlayer();
    }

    public void Death()
    {
        isDead = true;
        
        lives = Mathf.Max(0, lives-1);
        UpdateGuiInfoPlayer();
            
        if (lives == 0)
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
        if (!_guiController)
            return;
        
        _guiController.UpdateGuiInfo(lives:lives.ToString());
    }
}
