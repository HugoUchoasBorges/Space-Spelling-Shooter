using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerInput : MonoBehaviour
{

    #region Variables

    // Components
    [HideInInspector]
    public PlayerMovement playerMovement;
    private TypingSystem _typingSystem;

    private string _input = "";

    // Movement Button Names
    public string inputNameAxisX = "Horizontal";
    public string inputNameAxisY = "Vertical";

    private float _inputAxisX;
    private float _inputAxisY;

    #endregion

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        _typingSystem = GetComponent<TypingSystem>();

        Assert.IsNotNull(playerMovement, "No PlayerMovement script found within the Player.");
        Assert.IsNotNull(_typingSystem, "No TypingSystem script found within the Player.");
    }

    private void ReadMovementInput()
    {
        _inputAxisX = Input.GetAxis(inputNameAxisX);
        _inputAxisY = Input.GetAxis(inputNameAxisY);

        Vector2 inputAxis = new Vector2(_inputAxisX, _inputAxisY);

        if (playerMovement)
        {
            playerMovement.Move(inputAxis);
        }
    }

    private void ReadTypingInput()
    {
        if (Input.anyKeyDown)
        {
            _input = Input.inputString;
            if (_input == "")
                return;

            if (!_typingSystem)
                return;

            _typingSystem.TypeChar(_input.ToUpper());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        ReadTypingInput();
    }

    private void FixedUpdate()
    {
        ReadMovementInput();
    }
}
