using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class MovimentacaoInimigos : Movimentacao {

    public Palavra palavra;

    protected GameObject painel;
    protected Text texto;
    protected Slider vida;

    protected void Spawn()
    {
        // Inserindo uma palavra no inimigo
        List<Palavra> palavras = GeradorPalavras.requisitaPalavras(1);

        if (palavras[0] == null)
            return;
        
        palavra = palavras[0];
        texto.text = palavra.texto;

        //Valores gerados para movimentação do inimigo
        inputImpulse = GlobalVariables.inputImpulse;
        inputRotation = GlobalVariables.inputRotation;

        float additionalImpulse = (Mathf.Min(texto.text.Length, 12)) / 60;

        // Velocidade do inimigo diminui conforme tamanho da palavra
        inputImpulse -= additionalImpulse;

        //Definindo uma posição, direção e sentido iniciais
        SetaPosicaoDirecaoInicial();
    }

    // Use this for initialization
    protected override void Start () {

        base.Start();

        painel = gameObject.GetComponentInChildren<CanvasRenderer>().transform.gameObject;
        texto = gameObject.GetComponentInChildren<Text>();
        vida = gameObject.GetComponentInChildren<Slider>();

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

    protected void morte()
    {
        GlobalVariables.rmvLetraUsada(texto.text[0]);
        GerenciadorJogo.removeInimigo(gameObject);
        Destroy(gameObject);
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
