using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour {

    //Declaração das Variáveis

    // Variáveis com a nova posição do player ao chegar no limite do cenário
    float newX;
    float newY;

    //É o tamanho da zona fora da tela onde objetos podem se movimentar
    float deadZone;

    public Rigidbody2D rigidBody2D;

    //Variáveis para controle do personagem
    public float impulseThreshold;
    public float rotationThreshold;
    private float inputImpulse;
    private float inputRotation;

    //Variável de ajuste para aceleração
    float deltaTime;

    // Use this for initialization
    void Start () {

        deltaTime = Time.deltaTime;

        //Definindo alguns valores iniciais de variáveis
        rigidBody2D.angularDrag = 0.8f;
        rigidBody2D.drag = 0.3f;
        impulseThreshold = 50;
        rotationThreshold = 8;

        deadZone = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        //Captura de entrada para movimentação do player
        inputImpulse = Input.GetAxis("Vertical");
        inputRotation = -Input.GetAxis("Horizontal");

        //Tratamento das bordas do cenário
        verificaBordas();

    }

    void verificaBordas()
    {
        Vector2 newPosition = transform.position;

        // Lidando com os limites do cenário
        if (Mathf.Abs(transform.position.x) > GeradorDeArestas.bottomRightCorner.x + deadZone)
            if(transform.position.x > 0)
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

    void FixedUpdate()
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
