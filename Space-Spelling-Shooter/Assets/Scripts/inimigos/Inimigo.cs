using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour {

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

        canvas = Instantiate(Resources.Load(GlobalVariables.prefab_canvas)) as GameObject;
        canvas.transform.SetParent(transform);

        texto = canvas.GetComponentInChildren<Text>();
        vida = canvas.GetComponentInChildren<Slider>();

        painel = texto.transform.parent.gameObject;

        texto.text = palavra.texto;
        movimentacao = gameObject.AddComponent<MovimentacaoInimigos>();

        painel.AddComponent<FollowObject>();
    }

    // Use this for initialization
    void Start () {
        Spawn();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
