﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorJogo : MonoBehaviour {

    public static List<GameObject> inimigos { get; private set; }

    public static bool JOGO_PAUSADO = false;

	// Use this for initialization
	void Start () {
        // A colisão entre todos os objetos da Layer dos Inimigos serão ignoradas
        Physics2D.IgnoreLayerCollision(GlobalVariables.LAYER_INIMIGOS, GlobalVariables.LAYER_INIMIGOS);

        GeradorPalavras.preenchePalavras();

        inimigos = new List<GameObject>();
        StartCoroutine(GeraInimigos());
    }

    public static void destroiInimigo(GameObject inimigo, char letraInicial)
    {
        GlobalVariables.rmvLetraUsada(letraInicial);
        inimigos.Remove(inimigo);
        Destroy(inimigo);
    }

    public static GameObject buscaAlvo(char c)
    {
        foreach (GameObject inimigo in inimigos)
        {
            if (inimigo.GetComponentInChildren<Text>().text[0] == c)
            {
                return inimigo;
            }
        }
        return null;
    }

    private IEnumerator GeraInimigos()
    {
        while (!JOGO_PAUSADO)
        {
            while (!GlobalVariables.letrasUsadas.Values.Contains(false))
            {
                // Espera por 3 Segundos
                yield return new WaitForSeconds(GlobalVariables.tempoGeraInimigo);
            }

            // Gera um inimigo
            GameObject inimigo = GameObject.Instantiate(Resources.Load("Prefabs/inimigos/InimigoPadrao")) as GameObject;
            inimigos.Add(inimigo);

            // Espera por 3 Segundos
            yield return new WaitForSeconds(GlobalVariables.tempoGeraInimigo);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
