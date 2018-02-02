using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoInimigos : Movimentacao {

    // Use this for initialization
    protected override void Start () {

        base.Start();

        Physics.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), 
            GameObject.FindGameObjectWithTag("Inimigo").GetComponent<CircleCollider2D>());

        //Valores gerados para movimentação do inimigo
        inputImpulse = 0.2f;
        inputRotation = 0;

        //Definindo uma posição, direção e sentido iniciais
        SetaPosicaoDirecaoInicial();

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
