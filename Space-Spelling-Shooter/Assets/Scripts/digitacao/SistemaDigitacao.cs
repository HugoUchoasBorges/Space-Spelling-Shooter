using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDigitacao : MonoBehaviour {

    public static GameObject inimigoAlvo = null;
    private static Text texto;
    public static string palavra;
    private static Player player;

    // Use this for initialization
    void Start () {
        player = gameObject.GetComponent<Player>();
        StartCoroutine(verificaTeclas());
    }
	
	// Update is called once per frame
	void Update () {

	}

    private IEnumerator verificaTeclas()
    {
        while (true)
        {
            // Espera se o jogo estiver pausado
            if (GerenciadorJogo.JOGO_PAUSADO)
                yield return new WaitUntil(() => GerenciadorJogo.JOGO_PAUSADO == false);

            // Espera o player Respawnar
            if (!GlobalVariables.playerAtivo)
                yield return new WaitUntil(() => GlobalVariables.playerAtivo == true);

            foreach (char c in Input.inputString.ToUpper())
            {
                switch (c)
                {
                    case '\b': // backspace/delete
                        print("Backspace");
                        player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_space);
                        break;

                    case '\n': // enter
                        print("Enter");
                        break;

                    case '\r': // return
                        print("Return");
                        if (inimigoAlvo)
                        {
                            retiraAlvo();
                            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
                        }
                        break;

                    default:
                        buscaAlvo(c);
                        break;
                }
            }
            // Espera um tempo para verificar novamente as teclas
            yield return new WaitForSeconds(GlobalVariables.tempoVerificaTeclas);
        }
    }

    private void buscaAlvo(char c)
    {
        if (!inimigoAlvo)
        {

            GameObject alvo;

            if (alvo = GerenciadorJogo.buscaAlvo(c))
            {
                colocaAlvo(alvo);
            }
        }

        if (inimigoAlvo)
        {
            if (consomeLetra(c))
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
            }
            else
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_lock);
            }
        }
        else
        {
            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_lock);
        }
    }

    private static void colocaAlvo(GameObject alvo)
    {
        // Setando o novo alvo
        inimigoAlvo = alvo;
        texto = inimigoAlvo.GetComponentInChildren<Text>();
        palavra = texto.text;

        // Características do alvo recém adicionado
        texto.color = GlobalVariables.corInimigoAlvo;
    }

    public static void retiraAlvo()
    {
        if (texto)
        {
            texto.color = GlobalVariables.corInimigo;
            texto.text = palavra;
        }

        inimigoAlvo = null;
        texto = null;
        palavra = null;
    }

    private bool consomeLetra(char c)
    {
        Text texto = inimigoAlvo.GetComponentInChildren<Text>();

        if (texto.text[0] == c)
        {
            texto.text = texto.text.Remove(0, 1);

            if (texto.text == "")
            {
                StartCoroutine(GerenciadorJogo.destroiInimigo(inimigoAlvo, palavra[0]));
                retiraAlvo();
            }
            return true;
        }
        return false;
    }

    public static void destroiInimigo(GameObject inimigo)
    {
        colocaAlvo(inimigo);
        GlobalVariables.Instance.StartCoroutine(GerenciadorJogo.destroiInimigo(inimigoAlvo, palavra[0]));
        retiraAlvo();
    }
}
