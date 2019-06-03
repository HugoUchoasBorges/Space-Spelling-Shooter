using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;


[Serializable]
public class WordCollection
{
    #region Variables
    
    [Serializable]
    public class SubCollection
    {
        public string letter;
        public List<Word> words;
    }
    
    // List of Words
    public Word[] words;
    public List<SubCollection> subCollections = new List<SubCollection>();
    
    // Letters
    public string[] allLetters;
    
    #endregion

    // Constructor
    public WordCollection(Word[] words)
    {
        this.words = words;
        FillSubCollections();
    }
    
    public override string ToString()
    {
        string result = "Words";

        foreach (Word word in words)
        {
            result += "Word: " + word.text + "\n";
        }
        
        return result;
    }
    
    private void FillSubCollections()
    {
        Dictionary<string, List<Word>> mappedWords = new Dictionary<string, List<Word>>();
        List<string> letters = new List<string>();
        
        foreach (Word word in words)
        {
            string initialLetter = word.text.Substring(0, 1);
            if (!mappedWords.ContainsKey(initialLetter))
            {
                mappedWords[initialLetter] = new List<Word>();
                letters.Add(initialLetter);
            }

            mappedWords[initialLetter].Add(word);
        }

        allLetters = letters.ToArray();
        
        IDictionaryEnumerator enumerator = mappedWords.GetEnumerator();

        while (enumerator.MoveNext())
        {
            string key = (string)enumerator.Key;
            List<Word> value = (List<Word>) enumerator.Value;
            
            if (key == null || value == null)
                continue;

            if (value.Count == 0)
                continue;

            subCollections.Add(new SubCollection {letter = key, words = value});
        }
    }
    
    #region Words Handling Methods

    public Word[] GetAllWords()
    {
        return words;
    }

    public Word[] GetAllWordsWithLetter(string letter)
    {
        SubCollection subCollection = FindSubCollectionByLetter(letter);
        return subCollection?.words.ToArray();
    }

    private SubCollection FindSubCollectionByLetter(string letter)
    {
        SubCollection sub = null;
        
        foreach (SubCollection subCollection in subCollections)
        {
            if (subCollection.letter == letter)
            {
                sub = subCollection;
                break;
            }
        }

        return sub;
    }
    
    public Word GetRandomWord()
    {
        return words[Random.Range(0, words.Length)];
    }

    public Word GetRandomWordWithLetter(string letter)
    {
        if (letter == "")
            return GetRandomWord();
        
        Word[] letterWords = GetAllWordsWithLetter(letter);
        return letterWords[Random.Range(0, letterWords.Length)];
    }

    #endregion
}
