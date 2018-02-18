using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public static Text vidas;
    public static Text wave;
    public static Text pontos;
    public static Text derrotados;
    public static Text restantes;

    public void OnEnable()
    {
        vidas = GameObject.FindGameObjectWithTag("GUIVidas").GetComponent<Text>();
        wave = GameObject.FindGameObjectWithTag("GUIWave").GetComponent<Text>();
        pontos = GameObject.FindGameObjectWithTag("GUIPontos").GetComponent<Text>();
        derrotados = GameObject.FindGameObjectWithTag("GUIDerrotados").GetComponent<Text>();
        restantes = GameObject.FindGameObjectWithTag("GUIRestantes").GetComponent<Text>();
    }

    public static void atualizaGUI()
    {
        vidas.text = Player.Vidas.ToString();
        wave.text = GerenciaWaves.Wave.ToString();
        pontos.text = GlobalVariables.TotalPontuacao.ToString();
        derrotados.text = GlobalVariables.TotalInimigosDerrotados.ToString();
        restantes.text = GerenciaWaves.InimigosRestantes.ToString();
    }
}
