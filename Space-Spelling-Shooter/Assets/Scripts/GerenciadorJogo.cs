using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorJogo : MonoBehaviour {

    public static List<GameObject> inimigos { get; private set; }

    public static Player player;

    public static bool JOGO_PAUSADO = false;

    // Objetos do menu de pausa
    public static GameObject[] pauseObjects;

    public void iniciaJogo(){

        gameObject.AddComponent<GeradorDeArestas>();

        // Carrega as palavras dos arquivos para o jogo
        GeradorPalavras.preenchePalavras();

        // Cria instancia do player
        player = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.nave1Player]).GetComponent<Player>();

        // A colisão entre todos os objetos da Layer dos Inimigos será ignorada
        Physics2D.IgnoreLayerCollision(GlobalVariables.LAYER_INIMIGOS, GlobalVariables.LAYER_INIMIGOS);

        inimigos = new List<GameObject>();
        StartCoroutine(GeraInimigos());
    }

	// Use this for initialization
	void Start () {

        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();

        iniciaJogo();
    }

    //Reloads the Level
    public static void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //controls the pausing of the scene
    public static void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //hides objects with ShowOnPause tag
    public static void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with ShowOnPause tag
    public static void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public static IEnumerator destroiInimigo(GameObject inimigo, char letraInicial)
    {
        GlobalVariables.rmvLetraUsada(letraInicial);
        inimigos.Remove(inimigo);
        inimigo.transform.localScale = Vector3.zero;
        inimigo.GetComponent<CircleCollider2D>().enabled = false;
        float tamanhoAudio = inimigo.GetComponent<Inimigo>().PlayAudio(GlobalVariables.ENUM_AUDIO.enemy_dying);        
        yield return new WaitForSeconds(tamanhoAudio);
        Destroy(inimigo);
    }

    public static GameObject buscaAlvo(char c)
    {
        foreach (GameObject inimigo in inimigos)
        {
            if (inimigo.GetComponentInChildren<Text>().text[0] == c)
            {
                return inimigo;
            }
        }
        return null;
    }

    private IEnumerator GeraInimigos()
    {
        while (!JOGO_PAUSADO)
        {
            // Espera Existirem letras disponíveis
            if (!GlobalVariables.letrasUsadas.Values.Contains(false))
                yield return new WaitUntil(() => GlobalVariables.letrasUsadas.Values.Contains(false) == true);

            // Espera por 3 Segundos
            yield return new WaitForSeconds(GlobalVariables.tempoGeraInimigo);

            // Gera um inimigo
            GameObject inimigo = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.inimigoPadrao]) as GameObject;
            inimigos.Add(inimigo);
        }
    }

    public static void GameOVer()
    {
        print("GameOver");
        Application.Quit();
    }

    // Update is called once per frame
    void Update () {

        // Uses the ESC Key to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }
}
