﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : Singleton<GlobalVariables> {

    protected GlobalVariables() { } // guarantee this will be always a singleton only - can't use the constructor!

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

    // Inimigos
    public static float inputImpulse = 0.2f;
    public static float inputRotation = 0f;

    // Sistema de Digitação
    public static Dictionary<char, bool> letrasUsadas = new Dictionary<char, bool>();

    public static void GameOVer() {
        print("GameOver");
    }
}
