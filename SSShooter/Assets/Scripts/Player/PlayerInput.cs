using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    #region Variables

    private string input = "";

    // Movement Button Names
    public string inputNameAxisX = "Horizontal";
    public string inputNameAxisY = "Vertical";

    private float inputAxisX;
    private float inputAxisY;

    private

    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ReadMovementInput()
    {
        inputAxisX = Input.GetAxis(inputNameAxisX);
        inputAxisY = Input.GetAxis(inputNameAxisY);


        if (Mathf.Abs(inputAxisX) > 0.2f)
        {
            if (inputAxisX > 0)
            {
                Debug.Log(inputNameAxisX);
            }
            else if (inputAxisX < 0)
            {
                Debug.Log("-" + inputNameAxisX);
            }
        }

        if (Mathf.Abs(inputAxisY) > 0.2f)
        {
            if (inputAxisY > 0)
            {
                Debug.Log(inputNameAxisY);
            }
            else if (inputAxisY < 0)
            {
                Debug.Log("-" + inputNameAxisY);
            }
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
