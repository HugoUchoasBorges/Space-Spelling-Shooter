using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour {

    //Declaração das Variáveis

    public Rigidbody2D rigidBody2D;

    //Variáveis para controle do personagem
    public float impulseThreshold;
    public float rotationThreshold;
    private float inputImpulse;
    private float inputRotation;

    //Variável de ajuste para aceleração
    float deltaTime = Time.deltaTime;

    // Use this for initialization
    void Start () {

        //Definindo alguns valores iniciais de variáveis
        rigidBody2D.angularDrag = 0.8f;
        rigidBody2D.drag = 0.3f;
        impulseThreshold = 50;
        rotationThreshold = 8;
}

    // Update is called once per frame
    void Update()
    {
        //Captura de entrada para movimentação do player
        inputImpulse = Input.GetAxis("Vertical");
        inputRotation = -Input.GetAxis("Horizontal");

    }

    void FixedUpdate()
    {
        //Cálculo da movimentação do player
        rigidBody2D.AddRelativeForce(
            inputImpulse * (Vector2.up * impulseThreshold) * Time.deltaTime
            );
        rigidBody2D.AddTorque(
            inputRotation * (rotationThreshold) * Time.deltaTime
            );
    }
}
