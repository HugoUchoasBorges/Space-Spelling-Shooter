using System.Collections;
using UnityEngine;

public class SistemaDigitacao : MonoBehaviour {

    public GameObject inimigoAlvo = null;

    // Use this for initialization
    void Start () {
        StartCoroutine(verificaTeclas());
    }
	
	// Update is called once per frame
	void Update () {

	}

    private IEnumerator verificaTeclas()
    {
        while (!GerenciadorJogo.JOGO_PAUSADO)
        {
            foreach (char c in Input.inputString)
            {
                switch (c)
                {
                    case '\b': // backspace/delete
                        print("Backspace");
                        break;

                    case '\n': // enter
                        print("Enter");
                        break;

                    case '\r': // return
                        print("Return");
                        break;

                    default:
                        print(c);
                        break;
                }
            }
            // Espera um tempo para verificar novamente as teclas
            yield return new WaitForSeconds(GlobalVariables.tempoVerificaTeclas);
        }
    }
}
