using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Personagem {

    public SistemaDigitacao sistemaDigitacao;
    public MovimentacaoPlayer movimentacao;

    private static int vidas;
    public static int Vidas
    {
        get { return vidas; }
        set
        {
            vidas = value;

            GUIController.atualizaGUI();
        }
    }

    // Use this for initialization
    protected override void Start () {

        base.Start();

        Vidas = GlobalVariables.totalVidas;

        movimentacao = gameObject.AddComponent<MovimentacaoPlayer>();
        sistemaDigitacao = gameObject.AddComponent<SistemaDigitacao>();

        inicializaAudios(GlobalVariables.audio_player);

        PlayAudio(GlobalVariables.ENUM_AUDIO.game_start);
    }
}
