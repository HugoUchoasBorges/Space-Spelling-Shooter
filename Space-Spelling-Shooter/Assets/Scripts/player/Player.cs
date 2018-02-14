using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GerenciadorJogo gerenciadorJogo;
    public SistemaDigitacao sistemaDigitacao;
    public MovimentacaoPlayer movimentacao;

	// Use this for initialization
	void Start () {

        movimentacao = gameObject.AddComponent<MovimentacaoPlayer>();
        gerenciadorJogo = gameObject.AddComponent<GerenciadorJogo>();
        sistemaDigitacao = gameObject.AddComponent<SistemaDigitacao>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
