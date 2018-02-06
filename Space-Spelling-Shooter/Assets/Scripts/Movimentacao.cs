﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour {

    //Declaração das Variáveis

    //É o tamanho da zona fora da tela onde objetos podem se movimentar
    protected float deadZone;

    protected Rigidbody2D rigidBody2D;

    //Variáveis para controle do personagem
    public float impulseThreshold;
    public float rotationThreshold;
    protected float inputImpulse;
    protected float inputRotation;

    //Variável de ajuste para aceleração
    protected float deltaTime;

    // Use this for initialization
    protected virtual void Start () {

        deltaTime = Time.deltaTime;

        rigidBody2D = GetComponent<Rigidbody2D>();

        //Definindo alguns valores iniciais de variáveis
        rigidBody2D.angularDrag = GlobalVariables.angularDrag;
        rigidBody2D.drag = GlobalVariables.linearDrag;
        impulseThreshold = GlobalVariables.impulseThreshold;
        rotationThreshold = GlobalVariables.rotationThreshold;

        deadZone = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    protected virtual void Update () {

        //Tratamento das bordas do cenário
        verificaBordas();
    }

    private void verificaBordas()
    {
        Vector2 newPosition = transform.position;

        // Lidando com os limites do cenário
        if (Mathf.Abs(transform.position.x) > GeradorDeArestas.bottomRightCorner.x + deadZone)
            if (transform.position.x > 0)
                newPosition.x = GeradorDeArestas.bottomLeftCorner.x - deadZone / 2;
            else
                newPosition.x = GeradorDeArestas.bottomRightCorner.x + deadZone / 2;

        if (Mathf.Abs(transform.position.y) > GeradorDeArestas.upperRightCorner.y + deadZone)
            if (transform.position.y > 0)
                newPosition.y = GeradorDeArestas.bottomRightCorner.y - deadZone / 2;
            else
                newPosition.y = GeradorDeArestas.upperRightCorner.y + deadZone / 2;

        transform.position = newPosition;

    }

    protected virtual void FixedUpdate()
    {

        //Cálculo da movimentação do player
        rigidBody2D.AddRelativeForce(
            inputImpulse * (Vector2.up * impulseThreshold) * deltaTime
            );
        rigidBody2D.AddTorque(
            inputRotation * (rotationThreshold) * deltaTime
            );
    }
}
