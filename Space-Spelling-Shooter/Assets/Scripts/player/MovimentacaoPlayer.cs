using System.Collections;
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
    }
}
