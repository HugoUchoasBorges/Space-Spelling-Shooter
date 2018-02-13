using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorJogo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // A colisão entre todos os objetos da Layer dos Inimigos serão ignoradas
        Physics2D.IgnoreLayerCollision(GlobalVariables.LAYER_INIMIGOS, GlobalVariables.LAYER_INIMIGOS);

        GeradorPalavras.preenchePalavras();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
