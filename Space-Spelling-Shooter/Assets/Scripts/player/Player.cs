using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Personagem {

    public SistemaDigitacao sistemaDigitacao;
    public MovimentacaoPlayer movimentacao;

    public int vidas;

    // Use this for initialization
    protected override void Start () {

        base.Start();

        vidas = GlobalVariables.totalVidas;

        movimentacao = gameObject.AddComponent<MovimentacaoPlayer>();
        sistemaDigitacao = gameObject.AddComponent<SistemaDigitacao>();

        inicializaAudios(GlobalVariables.audio_player);

        PlayAudio(GlobalVariables.ENUM_AUDIO.game_start);
    }
}
