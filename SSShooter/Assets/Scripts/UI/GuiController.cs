using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;


[Serializable]
public class GuiController
{
    #region Variables
    
    private BindingFlags _bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    public string name;
    public TextMeshProUGUI textMesh;
    public string value;

    #endregion

    public static void InvokeMulti(Object obj, IEnumerable<GuiController> guiControllers)
    {
        foreach (GuiController guiController in guiControllers)
            guiController.Invoke(obj);
    }
    
    private void Invoke(Object obj)
    {
        var val = obj.GetType().GetField(value, _bindingFlags)?.GetValue(obj);
        var valStr = val?.ToString();
        
//        var newVal = valStr != "" ? valStr : value;
        var newVal = valStr;
        UpdateText(textMesh, newVal, obj);
    }
    
    private void UpdateText(TextMeshProUGUI text, string val, object obj)
    {
        Assert.IsNotNull(text);
        Assert.IsNotNull(val, "The field " + value + " does not exist in the class " + obj.GetType());

        if (!text)
            return;
        
        text.text = val;
    }
}