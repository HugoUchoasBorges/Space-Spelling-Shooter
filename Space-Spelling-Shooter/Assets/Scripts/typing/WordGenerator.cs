using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class WordGenerator : Singleton<WordGenerator> {

    // List containing all words
    public static List<Word> words { get; private set; }

    // List containing all words by TAG
    public static Dictionary<string, List<Word>> taggedWords { get; private set; }

    // Singleton Constructor
	protected WordGenerator() { }

    public static void FillWords(string path = "Assets/Resources/Words/")
    {
        words = new List<Word>();
        taggedWords = new Dictionary<string, List<Word>>();

        try
        {
            // list containing all TXT filenames on the path
            string[] filenames = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();

            foreach (string filename in filenames)
            {
                // Reads all words from these files
                string fullPath = path + filename;
                string[] dicionario = File.ReadAllLines(@fullPath);

                // Adds new TAG to the tags list
                GlobalVariables.AddTag(filename);

                foreach (string word in dicionario)
                {
                    Word newWord = new Word(word.ToUpper(), filename);
                    AddWord(newWord);

                    if (!GlobalVariables.usedChars.ContainsKey(newWord.text[0]))
                        GlobalVariables.usedChars[newWord.text[0]] = false;
                }
            }
        }
        catch (System.Exception e)
        {
            print("No dicts found in the path: " + path + "\n" + e);
        }
    }

    private static void AddWord(string text, string[] tags)
    {
        Word palavra = new Word(text, tags.ToList());
        AddWord(palavra);
    }

    // Método para adicionar uma palavra nos dicionários
    private static void AddWord(Word word)
    {
        words.Add(word);

        foreach(string tag in word.tags)
        {
            List<Word> list;

            if (taggedWords.ContainsKey(tag))
            {
                list = taggedWords[tag];
            }
            else
            {
                list = new List<Word>();
            }

            list.Add(word);

            taggedWords[tag] = list;
        }
        
    }

    public static Word RequestsWord(string[] tags = null)
    {
        Word word = RequestsWords(1, tags)[0];

        return word;
    }

    public static List<Word> RequestsWords(int amount, string[] tags = null)
    {
        List<Word> list = new List<Word>();

        for (int i = 0; i < amount; i++)
        {
            if(tags != null)
            {
                list.Add(GetWord(tags));
                continue;
            }

            list.Add(GetWord());
        }

        return list;
    }

    private static Word GetWord(string[] tags = null)
    {
        if (words == null)
        {
            FillWords();
        }

        // If there isn't any available character
        if (!GlobalVariables.usedChars.Values.Contains(false))
        {
            print("There is no characters available");
            return null;
        }

        List<Word> wordsRange = new List<Word>();
        wordsRange.AddRange(words);

        if(tags != null)
        {
            List<Word> wordList = new List<Word>();
            foreach (string tag in tags)
            {
                wordList.AddRange(taggedWords[tag]);
            }

            wordsRange = new List<Word>();
            wordsRange.AddRange(wordList);
        }

        List<Word> repeatedWords = new List<Word>();

        foreach (Word p in wordsRange)
        {
            if (GlobalVariables.usedChars[p.text[0]])
            {
                repeatedWords.Add(p);
            }
        }
        foreach (Word p in repeatedWords)
        {
            wordsRange.Remove(p);
        }

        if (wordsRange.Count < 1)
        {
            Debug.LogError("There is no available words.");
            return null;
        }

        int i = Random.Range(0, wordsRange.Count);
        char usedChar = wordsRange[i].text[0];
        GlobalVariables.AddUsedChar(usedChar);

        return wordsRange[i];
    }

}
