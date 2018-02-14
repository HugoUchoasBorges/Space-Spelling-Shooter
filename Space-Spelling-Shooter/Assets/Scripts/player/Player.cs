using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Personagem {

    public GerenciadorJogo gerenciadorJogo;
    public SistemaDigitacao sistemaDigitacao;
    public MovimentacaoPlayer movimentacao;

    // Use this for initialization
    protected override void Start () {

        base.Start();

        movimentacao = gameObject.AddComponent<MovimentacaoPlayer>();
        gerenciadorJogo = gameObject.AddComponent<GerenciadorJogo>();
        sistemaDigitacao = gameObject.AddComponent<SistemaDigitacao>();

        inicializaAudios(GlobalVariables.audio_player);

    }
}
