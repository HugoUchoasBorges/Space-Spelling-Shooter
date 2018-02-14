using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : Singleton<GlobalVariables> {

    protected GlobalVariables() { } // guarantee this will be always a singleton only - can't use the constructor!

    // Prefabs
    public enum ENUM_PREFAB
    {
        canvas,
        inimigoPadrao,
        background1,
        nave1Player,
        nave2Player,
        nave3Player,
        audioSource,
    }
    public static Dictionary<ENUM_PREFAB, GameObject> prefab_dict = new Dictionary<ENUM_PREFAB, GameObject>(){
        { ENUM_PREFAB.canvas, Resources.Load<GameObject>("Prefabs/inimigos/Canvas") },
        { ENUM_PREFAB.inimigoPadrao, Resources.Load<GameObject>("Prefabs/inimigos/InimigoPadrao") },
        { ENUM_PREFAB.background1, Resources.Load<GameObject>("Prefabs/background/background1") },
        { ENUM_PREFAB.nave1Player, Resources.Load<GameObject>("Prefabs/player/Nave1Player") },
        { ENUM_PREFAB.nave2Player, Resources.Load<GameObject>("Prefabs/player/Nave2Player") },
        { ENUM_PREFAB.nave3Player, Resources.Load<GameObject>("Prefabs/player/Nave3Player") },
        { ENUM_PREFAB.audioSource, Resources.Load<GameObject>("Prefabs/audio/AudioSource") },
    };

    // Layers
    public const int LAYER_INIMIGOS = 8;

    // Movimentação Geral
    public static float angularDrag = 0.8f;
    public static float linearDrag = 0.3f;
    public static float impulseThreshold = 50;
    public static float rotationThreshold = 8;

    // Player
    public static int totalVidas = 3;
    public static Color corInvulneravel = Color.red;
    public static float tempoRespawn = 3f;
    public static float tempoInvulneravel = 3f;
    public static bool playerAtivo = true;

    // Inimigos
    public static float inputImpulse = 2f;
    public static float inputRotation = 0f;
    public static Color corInimigo = Color.white;
    public static Color corInimigoAlvo = Color.red;

    // Sistema de Digitação
    public static Dictionary<char, bool> letrasUsadas = new Dictionary<char, bool>();
    public static List<string> TAGS = new List<string>();
    public static float tempoVerificaTeclas = 0.005f;

    // Spawn de inimigos
    public static float tempoGeraInimigo = 3f;

    public static void addTag(string tag)
    {
        if (!TAGS.Contains(tag))
        {
            TAGS.Add(tag);
        }
    }

    public static void addLetraUsada(char letra)
    {
        letrasUsadas[letra] = true;
    }

    public static void rmvLetraUsada(char letra)
    {
        letrasUsadas[letra] = false;
    }

    public static void GameOVer() {
        print("GameOver");
        Application.Quit();
    }
}
