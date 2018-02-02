﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoPlayer : Movimentacao {

    // Use this for initialization
    protected override void Start () {

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //Captura de entrada para movimentação do player
        inputImpulse = Input.GetAxis("Vertical");
        inputRotation = -Input.GetAxis("Horizontal");

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (--GlobalVariables.totalVidas == 0)
        {
            GlobalVariables.GameOVer();
        }

        DeathSequence();
        Respawn();
    }

    private void DeathSequence()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {

        // Deixa o player invisível
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // Deixa player intangível
        GetComponent<CircleCollider2D>().enabled = false;

        // Espera por 3 Segundos
        yield return new WaitForSeconds(3);

        // Centraliza o player no cenário
        Centraliza();

        // Deixa o player visível
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        // Muda a cor do player
        Color oldColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;

        //Espera por 3 segundos
        yield return new WaitForSeconds(3);

        // Deixa player Tangível
        GetComponent<CircleCollider2D>().enabled = true;

        // Volta a cor original
        GetComponent<SpriteRenderer>().color = oldColor;
    }

    protected void Centraliza()
    {
        // Seta o player ESTÁTICO no centro do cenário
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        rigidBody2D.angularVelocity = 0;
        rigidBody2D.velocity = Vector2.zero;
    }
}
