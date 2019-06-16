using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    #region Variables
    
    // Panels info
    [Header("Player Info Info ____________")]
    public Text livesValue; 
    
    [Space]
    [Header("Pontuation Info ____________")]
    public Text pointsValue;
    public Text enemiesDefeatedValue;
    public Text charsTypedValue;

    [Space] 
    [Header("Wave Manager Info ____________")]
    public GameObject wavesPanel;
    public Text wavesValue;
    
    #endregion

    private void Awake()
    {
        GlobalVariables.GuiController = this;
        
        Assert.IsNotNull(livesValue);
        Assert.IsNotNull(pointsValue);
        Assert.IsNotNull(enemiesDefeatedValue);
        Assert.IsNotNull(charsTypedValue);
        
        Assert.IsNotNull(wavesPanel);
        Assert.IsNotNull(wavesValue);
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
