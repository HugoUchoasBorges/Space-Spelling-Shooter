using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Personagem {

    public SistemaDigitacao sistemaDigitacao;
    public MovimentacaoPlayer movimentacao;

    // Use this for initialization
    protected override void Start () {

        base.Start();

        movimentacao = gameObject.AddComponent<MovimentacaoPlayer>();
        sistemaDigitacao = gameObject.AddComponent<SistemaDigitacao>();

        inicializaAudios(GlobalVariables.audio_player);

    }
}
