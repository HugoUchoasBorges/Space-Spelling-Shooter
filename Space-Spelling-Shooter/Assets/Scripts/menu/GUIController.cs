using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public static Text vidas;
    public static Text wave;

    public void OnEnable()
    {
        vidas = GameObject.FindGameObjectWithTag("GUIVidas").GetComponent<Text>();
        wave = GameObject.FindGameObjectWithTag("GUIWave").GetComponent<Text>();
    }

    public static void atualizaGUI()
    {

        vidas.text = Player.Vidas.ToString();
        wave.text = GerenciaWaves.Wave.ToString();
    }
}
