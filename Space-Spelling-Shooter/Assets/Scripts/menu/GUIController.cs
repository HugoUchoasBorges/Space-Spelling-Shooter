using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public static Text vidas;

    public void OnEnable()
    {
        vidas = GameObject.FindGameObjectWithTag("GUIVidas").GetComponent<Text>();
    }

    public static void atualizaGUI()
    {
        vidas.text = Player.Vidas.ToString();
    }
}
