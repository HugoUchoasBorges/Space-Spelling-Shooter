using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class MovimentacaoInimigos : Movimentacao {

    Inimigo inimigo;

    protected override void Awake()
    {
        //Definindo uma posição, direção e sentido iniciais
        SetaPosicaoDirecaoInicial();
    }

    // Use this for initialization
    protected override void Start () {

        inimigo = gameObject.GetComponent<Inimigo>();

        base.Start();

        //Valores gerados para movimentação do inimigo
        inputImpulse = GlobalVariables.inputImpulse;
        inputRotation = GlobalVariables.inputRotation;

        float additionalImpulse = (Mathf.Min(inimigo.texto.text.Length, 12)) / 6.67f;

        // Velocidade do inimigo diminui conforme tamanho da palavra
        inputImpulse -= additionalImpulse;
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

        rigidBody2D.AddRelativeForce(
            inputImpulse * (Vector2.up * impulseThreshold) * deltaTime
            );
        rigidBody2D.AddTorque(
            inputRotation * (rotationThreshold) * deltaTime
            );
    }
}
