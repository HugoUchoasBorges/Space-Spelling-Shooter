using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public static Text lifes;
    public static Text wave;
    public static Text score;
    public static Text defeated;
    public static Text remaining;

    public void OnEnable()
    {
        lifes = GameObject.FindGameObjectWithTag("GUIVidas").GetComponent<Text>();
        wave = GameObject.FindGameObjectWithTag("GUIWave").GetComponent<Text>();
        score = GameObject.FindGameObjectWithTag("GUIPontos").GetComponent<Text>();
        defeated = GameObject.FindGameObjectWithTag("GUIDerrotados").GetComponent<Text>();
        remaining = GameObject.FindGameObjectWithTag("GUIRestantes").GetComponent<Text>();
    }

    public static void RefreshGUI()
    {
        lifes.text = Player.Lifes.ToString();
        wave.text = WaveManager.Wave.ToString();
        score.text = GlobalVariables.ScoreCount.ToString();
        defeated.text = GlobalVariables.DefeatedEnemiesCount.ToString();
        remaining.text = WaveManager.RemainingEnemies.ToString();
    }
}
