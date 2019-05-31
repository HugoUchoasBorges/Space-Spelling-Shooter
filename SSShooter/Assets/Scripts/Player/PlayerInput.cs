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

        Assert.IsNotNull(playerMovement, "No PlayerMovement script found within the Player.");
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

            Debug.Log("Input: " + _input);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReadMovementInput();
        ReadTypingInput();
    }
}
