using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorPalavras {

	List<Palavra> palavras = new List<Palavra>();

	public GeradorPalavras() {
		string[] palavrasPositivas = System.IO.File.ReadAllLines(@"Assets\Resources\Words\positive.txt");
		string[] palavrasNegativas = System.IO.File.ReadAllLines(@"Assets\Resources\Words\negative.txt");

		foreach (string p in palavrasPositivas) {
			palavras.Add(new Palavra(p, tipoPalavra.positiva));
		}

		foreach (string p in palavrasNegativas) {
			palavras.Add(new Palavra(p, tipoPalavra.negativa));
		}

		for (char a = 'A'; a <= 'Z'; a++) {
			GlobalVariables.letrasUsadas[a] = false;
		}
	}

	public Palavra getPalavra() {
		int i = Random.Range(0, palavras.Count);
		return palavras[i]; // Por enquanto não será feita verificação de letras repetidas.
		/*
		if (!GlobalVariables.letrasUsadas[palavras[i].texto[0]]) {
			return palavras[i];
		}
		else {
			return null;
		}
		*/
	}

}
