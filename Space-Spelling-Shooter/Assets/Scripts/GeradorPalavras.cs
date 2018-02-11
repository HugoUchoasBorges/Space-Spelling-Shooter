using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GeradorPalavras : Singleton<GeradorPalavras> {

    protected static List<Palavra> palavras { get; private set; }

    // Construtor protegido para classe Singleton
	protected GeradorPalavras() { }

    private static void preenchePalavras(string path = "Assets\\Resources\\Words\\")
    {
        palavras = new List<Palavra>();

        // Pega o nome de todos os arquivos TXT que estão no PATH
        string[] filenames = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();

        foreach (string filename in filenames)
        {
            // Lê todas as strings desses arquvos
            string fullPath = path + filename;
            string[] dicionario = System.IO.File.ReadAllLines(@fullPath);

            foreach (string palavra in dicionario)
            {
                palavras.Add(new Palavra(palavra, filename));
            }
        }

        for (char a = 'A'; a <= 'Z'; a++)
        {
            GlobalVariables.letrasUsadas[a] = false;
        }
    }

	public static Palavra getPalavra() {
        if(palavras == null)
        {
            preenchePalavras();
        }
            
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
