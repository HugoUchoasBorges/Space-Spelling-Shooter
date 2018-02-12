using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class MovimentacaoInimigos : Movimentacao {

    public Palavra palavra;
    protected Text texto;

    protected void Spawn()
    {
        // Inserindo uma palavra no inimigo
        List<Palavra> palavras = GeradorPalavras.requisitaPalavras(1);

        if (palavras != null)
        {
            palavra = palavras[0];
            texto.text = palavra.texto;
        }
        else { return; }

        //Valores gerados para movimentação do inimigo
        inputImpulse = GlobalVariables.inputImpulse;
        inputRotation = GlobalVariables.inputRotation;

        //Definindo uma posição, direção e sentido iniciais
        SetaPosicaoDirecaoInicial();
    }

    // Use this for initialization
    protected override void Start () {

        base.Start();
        texto = gameObject.GetComponentInChildren<Text>();
        Spawn();
    }

    protected void SetaPosicaoDirecaoInicial()
    {
        transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
        transform.position = 
            new Vector3(
                Random.Range(GeradorDeArestas.bottomLeftCorner.x, GeradorDeArestas.bottomRightCorner.x),
                Random.Range(GeradorDeArestas.bottomRightCorner.y, GeradorDeArestas.upperRightCorner.y),
                0);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
