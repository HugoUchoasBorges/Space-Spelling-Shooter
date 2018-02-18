using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciaWaves : MonoBehaviour {

    // Informações da Wave
    private static int wave;
    public static int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            GUIController.atualizaGUI();
        }
    }
    public static bool waveAtiva;
    public static int totalInimigos;

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
        Wave = 0;
        desativaWaves();

        inicializaVariaveis();

        StartCoroutine(gerenciaWaves());
        StartCoroutine(GerenciadorJogo.GeraInimigos());
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
            desativaWaves();
        }
    }

    private void novaWave()
    {
        Wave += 1;

        int inimigosWave = (Wave + 1) * 3;
        totalInimigosWave.Add(inimigosWave);

        int inimigosTela = Wave + 2;
        maxInimigosTela.Add(inimigosTela);
        inimigosDerrotados.Add(0);
        inimigosRestantes = inimigosWave;

        inimigosTela = 0;
        totalInimigos = 0;
        

        pontuacao.Add(0);
        ppm.Add(0f);
        acuracia.Add(0f);

        GerenciadorJogo.despausaJogo();
    }

    public static void adicionaInimigo()
    {
        totalInimigos++;
    }

    public static void removeInimigo()
    {
        inimigosDerrotados[Wave - 1]++;
        inimigosRestantes--;

        incrementaInimigosTela();

        incrementaPontuacao();
        
        if(inimigosRestantes == 0)
            transicaoWaves();
    }

    private static void transicaoWaves()
    {
        GerenciadorJogo.pausaJogo();

        GerenciadorJogo.despausaJogo();
        ativaWaves();
    }

    private static void incrementaInimigosTela()
    {
        // Há cada totalInimigosWave/3 inimigos derrotados
        if (inimigosDerrotados[Wave - 1] % (totalInimigosWave[Wave - 1] / 3) == 0)
        {
            maxInimigosTela[Wave - 1]++;
        }
    }

    private static void incrementaPontuacao()
    {
        // Aumenta pontuação do jogador
        if(GlobalVariables.playerAtivo == true)
        {
            pontuacao[Wave - 1] += SistemaDigitacao.palavra.Length * 10;
            GlobalVariables.TotalPontuacao += pontuacao[Wave - 1];
        }

    }

    public static void desativaWaves()
    {
        waveAtiva = false;
    }

    public static void ativaWaves()
    {
        waveAtiva = true;
    }

    public static bool permiteNovoInimigo()
    {
        print("Permite novo inimigo????");
        print("Wave: " + Wave);
        print("MaxInimigosTela: " + maxInimigosTela[Wave - 1]);
        print("Inimigos Restantes: " + inimigosRestantes);
        print("Total Inimigos: " + totalInimigos);
        print("TotalInimigosWave: " + totalInimigosWave[Wave - 1]);
        if ((GerenciadorJogo.Inimigos.Count < maxInimigosTela[Wave - 1]) && 
            (inimigosRestantes > 0) && totalInimigos < totalInimigosWave[Wave - 1])
            return true;
        return false;
    }

    // Update is called once per frame
    void Update () {

	}
}
