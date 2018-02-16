using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoPlayer : Movimentacao {

    private Player player;
    public float velocidade;

    // Use this for initialization
    protected override void Start () {

        base.Start();
        player = gameObject.GetComponent<Player>();

        velocidade = 3f;
        // Centraliza o player no cenário
        Centraliza();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //Captura de entrada para movimentação do player
        float axisY = Input.GetAxis("Vertical");
        float axisX = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(axisX, axisY).normalized * deltaTime * velocidade);

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (--player.vidas == 0)
        {
            GerenciadorJogo.GameOVer();
        }

        DeathSequence();
    }

    private void DeathSequence()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_dying);

        // Avisa o jogo que o player foi desativado
        GlobalVariables.playerAtivo = false;

        // Deixa o player invisível
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // Deixa player intangível
        gameObject.layer = GlobalVariables.LAYER_INIMIGOS;

        // Espera por 3 Segundos
        yield return new WaitForSeconds(GlobalVariables.tempoRespawn);

        // Centraliza o player no cenário
        Centraliza();

        // Deixa o player visível
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        // Muda a cor do player
        Color oldColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = GlobalVariables.corInvulneravel;

        //Espera por 3 segundos
        yield return new WaitForSeconds(GlobalVariables.tempoInvulneravel);

        // Deixa player Tangível
        GetComponent<CircleCollider2D>().enabled = true;
        gameObject.layer = 0;

        // Volta a cor original
        GetComponent<SpriteRenderer>().color = oldColor;

        // Avisa o jogo que o player está ativo
        GlobalVariables.playerAtivo = true;
    }

    protected void Centraliza()
    {
        // Seta o player ESTÁTICO no centro do cenário
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        rigidBody2D.angularVelocity = 0;
        rigidBody2D.velocity = Vector2.zero;
    }
}
