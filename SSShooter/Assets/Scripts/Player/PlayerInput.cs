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

    private string input = "";

    // Movement Button Names
    public string inputNameAxisX = "Horizontal";
    public string inputNameAxisY = "Vertical";

    private float inputAxisX;
    private float inputAxisY;

    #endregion

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        Assert.IsNotNull(playerMovement, "No PlayerMovement script found within the Player.");
    }

    public void ReadMovementInput()
    {
        inputAxisX = Input.GetAxis(inputNameAxisX);
        inputAxisY = Input.GetAxis(inputNameAxisY);

        Vector2 inputAxis = new Vector2(inputAxisX, inputAxisY);

        if (playerMovement)
        {
            playerMovement.Move(inputAxis);
        }
    }

    public void ReadTypingInput()
    {
        if (Input.anyKeyDown)
        {
            input = Input.inputString;
            if (input == "")
                return;

            Debug.Log("Input: " + input);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReadMovementInput();
        ReadTypingInput();
    }
}
