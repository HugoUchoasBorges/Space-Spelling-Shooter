using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo : Personagem {

    public Palavra palavra { get; protected set; }

    public GameObject canvas { get; protected set; }
    public GameObject painel { get; protected set; }
    public Text texto { get; protected set; }
    public Slider vida { get; protected set; }

    protected MovimentacaoInimigos movimentacao;

    protected void Spawn()
    {
        // Inserindo uma palavra no inimigo
        palavra = GeradorPalavras.requisitaPalavra();

        if (palavra == null)
        {
            Destroy(gameObject);
            return;
        }

        canvas = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.canvas]) as GameObject;
        canvas.transform.SetParent(transform);

        texto = canvas.GetComponentInChildren<Text>();
        vida = canvas.GetComponentInChildren<Slider>();

        painel = texto.transform.parent.gameObject;

        texto.text = palavra.texto;
        movimentacao = gameObject.AddComponent<MovimentacaoInimigos>();

        painel.AddComponent<FollowObject>();
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();

        Spawn();
        inicializaAudios(GlobalVariables.audio_enemy);
    }
	
	// Update is called once per frame
	void Update () {
		
        atualizaVida();
	}

    private void atualizaVida(){
        
        Image fill = vida.transform.GetChild(1).GetComponentInChildren<Image>();
        vida.value = texto.text.Length / ((float)palavra.texto.Length);

        if (vida.value < 0.35f)
        {
            fill.color = Color.red;
        }
        else if(vida.value < 0.7f)
        {
            fill.color = Color.yellow;
        }else{
            fill.color = Color.green;
        }
    }
}
