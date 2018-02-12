using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeArestas : MonoBehaviour {

    public static Vector2 bottomLeftCorner;
    public static Vector2 upperLeftCorner;
    public static Vector2 upperRightCorner;
    public static Vector2 bottomRightCorner;

    // Executed before Start
    private void Awake()
    {
        //Pegando uma referência para a câmera ativa no jogo
        Camera cam = Camera.main;

        //Pontos da Câmera
        bottomLeftCorner = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        upperLeftCorner = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        upperRightCorner = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        bottomRightCorner = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
    }

    // Use this for initialization
    void Start () {

        // A colisão entre todos os objetos da Layer dos Inimigos serão ignoradas
        Physics2D.IgnoreLayerCollision(GlobalVariables.LAYER_INIMIGOS, GlobalVariables.LAYER_INIMIGOS);

        GeradorPalavras.preenchePalavras();

        print("TESTANDO GERAR PALAVRAS POR TAGS SEPARADAS");
        for (int i = 0; i < GlobalVariables.TAGS.Count; i++)
        {

            print("TAG: " + GlobalVariables.TAGS[i]);

            List<Palavra> palavras = GeradorPalavras.requisitaPalavras(5, new string[] { GlobalVariables.TAGS[i] });

            foreach (Palavra palavra in palavras)
            {
                print(palavra.texto);
            }
        }

        print("TESTANDO GERAR PALAVRAS QUAISQUER");
        List<Palavra> palavrasQuaisquer = GeradorPalavras.requisitaPalavras(5);

        foreach (Palavra palavra in palavrasQuaisquer)
        {
            print(palavra.texto);
        }

        print("TESTANDO GERAR PALAVRAS COM TODAS AS TAGS JUNTAS");
        List<Palavra> palavrasTags = GeradorPalavras.requisitaPalavras(5, GlobalVariables.TAGS.ToArray());

        foreach (Palavra palavra in palavrasTags)
        {
            print(palavra.texto);
        }
    }

    void geraCollider()
    {
        //Procurando por um EdgeCollider2D no MESMO GameObject que este script está anexado
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();

        //Definindo os pontos do EdgeCollider2D
        collider.points = new Vector2[5]
        {
            bottomLeftCorner,
            upperLeftCorner,
            upperRightCorner,
            bottomRightCorner,
            bottomLeftCorner
        };
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
