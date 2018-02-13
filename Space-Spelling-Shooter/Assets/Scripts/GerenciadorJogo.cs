using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GerenciadorJogo : MonoBehaviour {

    List<GameObject> inimigos;

    public static bool JOGO_PAUSADO = false;

	// Use this for initialization
	void Start () {
        // A colisão entre todos os objetos da Layer dos Inimigos serão ignoradas
        Physics2D.IgnoreLayerCollision(GlobalVariables.LAYER_INIMIGOS, GlobalVariables.LAYER_INIMIGOS);

        GeradorPalavras.preenchePalavras();

        inimigos = new List<GameObject>();
        StartCoroutine(GeraInimigos());
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
