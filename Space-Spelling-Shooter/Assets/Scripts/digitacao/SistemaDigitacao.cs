using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDigitacao : MonoBehaviour {

    public GameObject inimigoAlvo = null;
    private Text texto;
    private string palavra;
    private Player player;

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
        while (!GerenciadorJogo.JOGO_PAUSADO)
        {
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

    private void colocaAlvo(GameObject alvo)
    {
        // Setando o novo alvo
        inimigoAlvo = alvo;
        texto = inimigoAlvo.GetComponentInChildren<Text>();
        palavra = texto.text;

        // Características do alvo recém adicionado
        texto.color = GlobalVariables.corInimigoAlvo;
    }

    private void retiraAlvo()
    {
        print("RETIRANDO ALVO");

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
                GerenciadorJogo.destroiInimigo(inimigoAlvo, palavra[0]);
                retiraAlvo();

                print("Inimigo Destruído!!!");
            }
            return true;
        }
        return false;
    }
}
