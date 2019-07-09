using UnityEngine;
using UnityEngine.Assertions;

public class PlayerInput : MonoBehaviour
{

    #region Variables

    // Components
    [HideInInspector]
    public PlayerMovement playerMovement;
    private TypingSystem _typingSystem;
    private Player _player;
    private PlayerWeapon _playerWeapon;
    private AudioManager _audioManager;

    private string _input = "";

    // Movement Button Names
    public string inputNameAxisX = "Horizontal";
    public string inputNameAxisY = "Vertical";

    [Header("Audio Variables __________________")]
    public string inputKeyAudio;
    public string inputKeyLockAudio;

    private float _inputAxisX;
    private float _inputAxisY;

    #endregion

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        _typingSystem = GetComponent<TypingSystem>();
        _player = GetComponent<Player>();
        _playerWeapon = GetComponent<PlayerWeapon>();
        _audioManager = GetComponent<AudioManager>();

        Assert.IsNotNull(playerMovement, "No PlayerMovement script found within the Player.");
        Assert.IsNotNull(_typingSystem, "No TypingSystem script found within the Player.");
        Assert.IsNotNull(_player, "No Player script found within the Player.");
        Assert.IsNotNull(_player, "No PlayerWeapon script found within the Player.");
        Assert.IsNotNull(_audioManager, "No AudioManager script found within the Player.");
    }

    private void HandleMovementInput()
    {
        _inputAxisX = Input.GetAxis(inputNameAxisX);
        _inputAxisY = Input.GetAxis(inputNameAxisY);

        Vector2 inputAxis = new Vector2(_inputAxisX, _inputAxisY);

        if (!playerMovement)
            return;
        
        playerMovement.Move(inputAxis);
        
    }

    private void HandleTypingInput()
    {
        if (_player && _player.isDead)
            return;
        
        if (Input.anyKeyDown)
        {
            _input = Input.inputString;
            if (_input == "")
                return;

            if (!_typingSystem)
                return;

            Enemy enemyTarget = _typingSystem.TypeChar(_input.ToUpper());
            if (enemyTarget && _playerWeapon)
            {
                _playerWeapon.Shoot(enemyTarget.transform);
                if (_audioManager)
                    _audioManager.Play(inputKeyAudio);
            } else if (_audioManager)
                _audioManager.Play(inputKeyLockAudio);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        HandleTypingInput();
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
    }
}
