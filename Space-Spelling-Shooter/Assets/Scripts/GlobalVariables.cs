using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : Singleton<GlobalVariables> {

    protected GlobalVariables() { } // guarantee this will be always a singleton only - can't use the constructor!

    public static int totalVidas = 3;

    public static Color corInvulneravel = Color.red;

    public static float tempoRespawn = 3f;

    public static float tempoInvulneravel = 3f;


    public static void GameOVer() {
        print("GameOver");
    }
}
