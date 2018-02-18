using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciaWaves : MonoBehaviour {

    // Informações da Wave
    public static int wave;
    public static bool waveAtiva;

    // Informações dos inimigos
    public static List<int> totalInimigosWave;
    public static List<int> maxInimigosTela;
    public static List<int> inimigosDerrotados;
    public static int inimigosRestantes;

    // Informações do player
    public static List<int> pontuacao;
    public static List<float> ppm;
    public static List<float> acuracia;

	// Use this for initialization
	void Awake () {
        wave = 0;
        waveAtiva = false;

        inicializaVariaveis();

        StartCoroutine(gerenciaWaves());
    }

    private void inicializaVariaveis()
    {
        totalInimigosWave = new List<int>();
        maxInimigosTela = new List<int>();
        inimigosDerrotados = new List<int>();
        pontuacao = new List<int>();
        ppm = new List<float>();
        acuracia = new List<float>();
    }

    private IEnumerator gerenciaWaves()
    {
        while (true)
        {
            // Espera começar a Wave
            yield return new WaitUntil(() => waveAtiva == true);

            novaWave();
            waveAtiva = false;
        }
    }

    private void novaWave()
    {
        wave++;

        int totalInimigos = (wave + 3) * 3;
        totalInimigosWave.Add(totalInimigos);

        int inimigosTela = wave + 2;
        maxInimigosTela.Add(inimigosTela);
        inimigosDerrotados.Add(0);
        inimigosRestantes = totalInimigos;

        pontuacao.Add(0);
        ppm.Add(0f);
        acuracia.Add(0f);

        StartCoroutine(GerenciadorJogo.GeraInimigos());
    }

    public static void OnInimigosChange(List<GameObject> inimigos)
    {
    }

    public static void pausaWaves()
    {
        GerenciadorJogo.JOGO_PAUSADO = true;
        waveAtiva = false;
    }

    public static void iniciaWaves()
    {
        GerenciadorJogo.JOGO_PAUSADO = false;
        waveAtiva = true;
    }

    public static bool permiteNovoInimigo()
    {
        print("Permite novo inimigo????");
        print("Wave: " + wave);
        print("MaxInimigos: " + maxInimigosTela[wave - 1]);
        print("Inimigos Restantes: " + inimigosRestantes);
        if ((GerenciadorJogo.Inimigos.Count < maxInimigosTela[wave - 1]) && 
            (inimigosRestantes > 0))
            return true;
        return false;
    }

    // Update is called once per frame
    void Update () {

	}
}
