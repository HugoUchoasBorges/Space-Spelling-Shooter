using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    #region Variables
    
    // Wave Variables
    [Header("Wave Variables ___________")] 
    [SerializeField]
    private int waveNumber;
    private int _enemiesLeftToInvoke;
    private int _maxEnemiesOnScreen;
    

    // Components
    private GameObject _wavesPanel;
    private Text _wavesValue;
    
    // Relational Components
    private EnemyManager _enemyManager;
    private GuiController _guiController;

    #endregion

    private void Start()
    {
        _wavesPanel = GlobalVariables.GuiController.wavesPanel;
        _wavesValue = GlobalVariables.GuiController.wavesValue;

        _enemyManager = GlobalVariables.EnemyManager;
        _guiController = GlobalVariables.GuiController;

        if (_wavesPanel && _wavesValue)
        {
            _wavesPanel.SetActive(true);

            if (_enemyManager)
            {
                NextWave();
            }
        }
        
        Assert.IsNotNull(_enemyManager, 
            "EnemyManager not found in the Scene. You must provide one in order to Spawn the Enemies");
        Assert.IsNotNull(_guiController, 
            "GUIController not found in the Scene. You must provide one in order to display Information");
        
        Assert.IsNotNull(_wavesPanel);
        Assert.IsNotNull(_wavesValue);
    }

    private void StartWave()
    {
        // Calculates the wave level
        int waveEnemies = waveNumber * 3;
        float waveEnemiesSpawnRate = 2f / waveNumber;
        int maxScreenEnemies = waveNumber * 2;
        
        // Start invoking enemies
        InvokeEnemies(waveEnemies, waveEnemiesSpawnRate, maxScreenEnemies);
    }

    private void InvokeEnemies(int enemiesTotal, float repeatRate, int maxEnemiesOnScreen)
    {
        if (!_enemyManager)
            return;

        _maxEnemiesOnScreen = maxEnemiesOnScreen;
        _enemiesLeftToInvoke = enemiesTotal;
        InvokeRepeating(nameof(InvokeEnemy), repeatRate, repeatRate);
    }

    private void InvokeEnemy()
    {
        if (_enemyManager.activeEnemies.Count >= _maxEnemiesOnScreen)
            return;
        
        if (!_enemyManager.SpawnEnemy())
            return;
        
        _enemiesLeftToInvoke--;

        if (_enemiesLeftToInvoke <= 0)
            StopInvokeEnemy();
    }

    private void StopInvokeEnemy()
    {
        CancelInvoke(nameof(InvokeEnemy));
        InvokeRepeating(nameof(CheckForEnemiesLeft), 0, 0.5f);
    }

    private void CheckForEnemiesLeft()
    {
        if (_enemyManager.activeEnemies.Count > 0)
            return;

        CancelInvoke(nameof(CheckForEnemiesLeft));
        NextWave();

    }

    private void NextWave()
    {
        waveNumber++;
        _guiController.UpdateGuiInfo(wave:waveNumber.ToString());
        
        StartWave();
    }
}
