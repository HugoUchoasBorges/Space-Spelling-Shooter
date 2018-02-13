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

                    if (!GlobalVariables.letrasUsadas.ContainsKey(newPalavra.texto[0]))
                        GlobalVariables.letrasUsadas[newPalavra.texto[0]] = false;
                }
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

        // Caso não existam letras disponíveis
        if (!GlobalVariables.letrasUsadas.Values.Contains(false))
        {
            print("Não existem letras disponíveis.");
            return null;
        }

        List<Palavra> palavrasRange = new List<Palavra>();
        palavrasRange.AddRange(palavras);

        // Tratando o caso de ser requisitada uma palavra com TAG
        if(tags != null)
        {
            List<Palavra> listaPalavras = new List<Palavra>();
            foreach (string tag in tags)
            {
                listaPalavras.AddRange(palavrasTags[tag]);
            }

            palavrasRange = new List<Palavra>();
            palavrasRange.AddRange(listaPalavras);
        }

        // Tratando palavras com letras iniciais repetidas
        List<Palavra> palavrasRepetidas = new List<Palavra>();

        // Retira palavras com letras repetidas
        foreach (Palavra p in palavrasRange)
        {
            if (GlobalVariables.letrasUsadas[p.texto[0]])
            {
                palavrasRepetidas.Add(p);
            }
        }
        foreach (Palavra p in palavrasRepetidas)
        {
            palavrasRange.Remove(p);
        }

        if (palavrasRange.Count < 1)
        {
            Debug.LogError("Não existem palavras disponíveis.");
            return null;
        }

        int i = Random.Range(0, palavrasRange.Count);
        char letraUsada = palavrasRange[i].texto[0];
        GlobalVariables.addLetraUsada(letraUsada);

        return palavrasRange[i];
    }

}
