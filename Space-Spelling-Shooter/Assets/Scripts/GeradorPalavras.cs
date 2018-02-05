using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorPalavras {

	string[] palavras_positivas;
	string[] palavras_negativas;
	
	
	public GeradorPalavras() {
		palavras_positivas = System.IO.File.ReadAllLines(@"Assets\Resources\Words\positive.txt");
		palavras_negativas = System.IO.File.ReadAllLines(@"Assets\Resources\Words\negative.txt");
	}


}
