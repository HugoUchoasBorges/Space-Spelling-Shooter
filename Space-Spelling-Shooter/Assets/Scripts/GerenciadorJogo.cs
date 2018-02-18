﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorJogo : MonoBehaviour {

    private static List<GameObject> inimigos;
    public static List<GameObject> Inimigos
    {
        get { return inimigos; }
        private set
        {
            inimigos = value;
            GerenciaWaves.OnInimigosChange(Inimigos);
        }
    }

    public static Player player;

    public static bool JOGO_PAUSADO = true;

    // Objetos do menu de pausa
    public static GameObject[] pauseObjects;

    // Objetos do menu de morte
    public static GameObject[] deathObjects;

    // Gerenciador de Waves
    public static GerenciaWaves gerenciaWaves;

    // Use this for initialization
    void Start()
    {

        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();

        deathObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");
        hideDeath();

        gerenciaWaves = gameObject.AddComponent<GerenciaWaves>();

        iniciaJogo();
    }

    public void iniciaJogo(){

        gameObject.AddComponent<GeradorDeArestas>();

        // Carrega as palavras dos arquivos para o jogo
        GeradorPalavras.preenchePalavras();

        // Cria instancia do player
        player = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.nave1Player]).GetComponent<Player>();

        // A colisão entre todos os objetos da Layer dos Inimigos será ignorada
        Physics2D.IgnoreLayerCollision(GlobalVariables.LAYER_INIMIGOS, GlobalVariables.LAYER_INIMIGOS);

        Inimigos = new List<GameObject>();

        GerenciaWaves.iniciaWaves();
    }

    //Reloads the Level
    public static void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //controls the pausing of the scene
    public static void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //hides objects with ShowOnPause tag
    public static void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with ShowOnPause tag
    public static void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public static IEnumerator destroiInimigo(GameObject inimigo, char letraInicial)
    {
        GlobalVariables.rmvLetraUsada(letraInicial);
        Inimigos.Remove(inimigo);
        inimigo.transform.localScale = Vector3.zero;
        inimigo.GetComponent<CircleCollider2D>().enabled = false;
        float tamanhoAudio = inimigo.GetComponent<Inimigo>().PlayAudio(GlobalVariables.ENUM_AUDIO.enemy_dying);        
        yield return new WaitForSeconds(tamanhoAudio);
        Destroy(inimigo);
    }

    public static GameObject buscaAlvo(char c)
    {
        foreach (GameObject inimigo in Inimigos)
        {
            if (inimigo.GetComponentInChildren<Text>().text[0] == c)
            {
                return inimigo;
            }
        }
        return null;
    }

    public static IEnumerator GeraInimigos()
    {
        while (true)
        {
            // Espera
            yield return new WaitUntil(() => JOGO_PAUSADO == false);

            // Espera Existirem letras disponíveis
            if (!GlobalVariables.letrasUsadas.Values.Contains(false))
                yield return new WaitUntil(() => GlobalVariables.letrasUsadas.Values.Contains(false) == true);

            // Espera
            yield return new WaitForSeconds(GlobalVariables.tempoGeraInimigo);

            // Gera um inimigo
            if (GerenciaWaves.permiteNovoInimigo())
            {
                GameObject inimigo = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.inimigoPadrao]) as GameObject;
                Inimigos.Add(inimigo);
            }
            
        }
    }

    public static void GameOVer()
    {
        player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_dying);
        Time.timeScale = 0;
        showDeath();
    }

    //shows objects with ShowOnDeath tag
    public static void showDeath()
    {
        foreach (GameObject g in deathObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public static void hideDeath()
    {
        foreach (GameObject g in deathObjects)
        {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {

        // Uses the ESC Key to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }
}
