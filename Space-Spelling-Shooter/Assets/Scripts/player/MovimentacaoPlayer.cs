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

        velocidade = GlobalVariables.playerSpeed;
        // Centraliza o player no cenário
        Centraliza();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ControllPlayer();
    }

    private void ControllPlayer()
    {
        // Captures user input
        float axisY = Input.GetAxis("Vertical");
        float axisX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(axisX, axisY);

        // Normalizes diagonal speed
        if (movement.magnitude > 1)
            movement = movement.normalized;
        
        // Applies speed to the movement
        movement *= velocidade;

        // Move the player
        transform.Translate(movement * Time.deltaTime, Space.World);

        // Rotates the player
        if (movement.magnitude != 0)
        {
            Quaternion angle = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, movement));
            if (movement.x > 0)
            {
                angle.z = -angle.z;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, angle, 7f * Time.deltaTime);
        }
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
