using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    #region Variables

    // Components
    private GameObject _wavesPanel;
    private Text _wavesValue;

    #endregion

    private void Start()
    {
        _wavesPanel = GlobalVariables.GuiController.wavesPanel;
        _wavesValue = GlobalVariables.GuiController.wavesValue;

        if (_wavesPanel && _wavesValue)
        {
            _wavesPanel.SetActive(true);
        }
        
        Assert.IsNotNull(_wavesPanel);
        Assert.IsNotNull(_wavesValue);
    }
}
