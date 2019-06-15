using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    #region Variables
    
    // Panels info
    public Text livesValue; 
    public Text pointsValue;
    public Text enemiesDefeatedValue;
    public Text charsTypedValue;

    #endregion

    private void Awake()
    {
        Assert.IsNotNull(livesValue);
        Assert.IsNotNull(pointsValue);
        Assert.IsNotNull(enemiesDefeatedValue);
        Assert.IsNotNull(charsTypedValue);
    }

    public void UpdateGuiInfo(string lives="", string points="", string enemiesDefeated="", string charsTyped="")
    {
        if(livesValue)
            livesValue.text = lives!="" ? lives : livesValue.text;
        if(pointsValue)
            pointsValue.text = points!="" ? points : pointsValue.text;
        if(enemiesDefeatedValue)
            enemiesDefeatedValue.text = enemiesDefeated!="" ? enemiesDefeated : enemiesDefeatedValue.text;
        if(charsTypedValue)
            charsTypedValue.text = charsTyped!="" ? charsTyped : charsTypedValue.text;
    }

}
