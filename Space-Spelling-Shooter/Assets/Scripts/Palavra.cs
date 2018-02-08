using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palavra {

	public string texto;
	public int tamanho;
	public tipoPalavra tipo;
	public int pontos;

	public Palavra() {
		texto = "";
		tamanho = 0;
		pontos = 0;
	}

	public Palavra(string p, tipoPalavra tipo) {
		texto = p;
		tamanho = p.Length;
		this.tipo = tipo;
		pontos = tamanho * 10;
	}
}
