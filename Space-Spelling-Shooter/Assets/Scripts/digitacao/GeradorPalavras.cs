using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GeradorPalavras : Singleton<GeradorPalavras> {

    // Lista com TODAS as palavras
    public static List<Palavra> palavras { get; private set; }

    //Lista com TODAS as palavras separadas por tags
    public static Dictionary<string, List<Palavra>> palavrasTags { get; private set; }

    // Construtor protegido para classe Singleton
	protected GeradorPalavras() { }

    public static void preenchePalavras(string path = "Assets\\Resources\\Words\\")
    {
        palavras = new List<Palavra>();
        palavrasTags = new Dictionary<string, List<Palavra>>();

        try
        {
            // Pega o nome de todos os arquivos TXT que estão no PATH
            string[] filenames = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();

            foreach (string filename in filenames)
            {
                // Lê todas as strings desses arquvos
                string fullPath = path + filename;
                string[] dicionario = System.IO.File.ReadAllLines(@fullPath);

                // Adiciona a nova tag na lista de tags
                GlobalVariables.addTag(filename);

                foreach (string palavra in dicionario)
                {
                    Palavra newPalavra = new Palavra(palavra.ToUpper(), filename);
                    addPalavra(newPalavra);
                }
            }

            for (char a = 'A'; a <= 'Z'; a++)
            {
                GlobalVariables.letrasUsadas[a] = false;
            }
        }
        catch (System.Exception e)
        {
            print("Nenhum dicionário encontrado na pasta: " + path + "\n" + e);
        }
    }

    // Método para adicionar uma palavra nos dicionários
    private static void addPalavra(string texto, string[] tags)
    {
        Palavra palavra = new Palavra(texto, tags.ToList());
        addPalavra(palavra);
    }

    // Método para adicionar uma palavra nos dicionários
    private static void addPalavra(Palavra palavra)
    {
        palavras.Add(palavra);

        foreach(string tag in palavra.tags)
        {
            List<Palavra> list;

            if (palavrasTags.ContainsKey(tag))
            {
                list = palavrasTags[tag];
            }
            else
            {
                list = new List<Palavra>();
            }

            list.Add(palavra);

            palavrasTags[tag] = list;
        }
        
    }

    public static List<Palavra> requisitaPalavras(int quantidade, string[] tags = null)
    {
        List<Palavra> lista = new List<Palavra>();

        for (int i = 0; i < quantidade; i++)
        {
            if(tags != null)
            {
                lista.Add(getPalavra(tags));
                continue;
            }

            lista.Add(getPalavra());
        }

        return lista;
    }

    // Método que retorna uma palavra do dicionário de acordo com as tags do parâmetro
    private static Palavra getPalavra(string[] tags = null)
    {
        if (palavras == null)
        {
            preenchePalavras();
        }

        List<Palavra> palavrasRange = palavras;

        if(tags != null)
        {
            List<Palavra> listaPalavras = new List<Palavra>();
            foreach (string tag in tags)
            {
                listaPalavras.AddRange(palavrasTags[tag]);
            }
            palavrasRange = listaPalavras;
        }

        int i = Random.Range(0, palavrasRange.Count);
        return palavrasRange[i];
    }

}
