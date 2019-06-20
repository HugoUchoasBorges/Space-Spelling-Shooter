using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Variables

    [Header("Components_______________")]
    public Image healthBarImage;
    
    [Header("Control Variables_______________")]
    [Range(0f, 1f)]
    public float updateSpeedSeconds;
    private int _currentHealth;

    [Header("Colors_______________")] 
    public Color fullHealth = Color.green;
    public Color midHealth = Color.yellow;
    public Color lowHealth = Color.red;

    // empty delegate avoids null exceptions
//    public event Action<float> OnHealthPercChanged = delegate {  };

    #endregion

    private void Awake()
    {
        
        Assert.IsNotNull(healthBarImage, "You must provide a HealthBar Foreground Image.");
    }

    public void SetHealthBarAmount(float amount)
    {
        if (!healthBarImage)
            return;

        amount = amount > 1 ? amount / 100f : amount;
        StartCoroutine(ChangeHealthBarAmount(amount));
    }

    private IEnumerator ChangeHealthBarAmount(float amount)
    {
        float currentFillAmount = healthBarImage.fillAmount;
        float timeElapsed = 0f;

        while (timeElapsed < updateSpeedSeconds)
        {
            timeElapsed += Time.deltaTime;
            healthBarImage.fillAmount = Mathf.Lerp(currentFillAmount, amount, timeElapsed / updateSpeedSeconds);
            
            // This waits until next clock cicle
            yield return null;
        }

        healthBarImage.fillAmount = amount;
        if (amount > 0.75f)
            healthBarImage.color = fullHealth;
        else if (amount > 0.45f)
            healthBarImage.color = midHealth;
        else
            healthBarImage.color = lowHealth;
    }
}
