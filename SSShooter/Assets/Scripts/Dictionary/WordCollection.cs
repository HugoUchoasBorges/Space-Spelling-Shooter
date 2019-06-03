using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

[Serializable]
public class WordCollection
{

    [Serializable]
    public class SubCollection
    {
        public string letter;
        public List<Word> words;
    }
    
    #region Variables
    
    // List of Words
    public Word[] words;
    public List<SubCollection> subCollections = new List<SubCollection>();
    
    #endregion

    public WordCollection(Word[] words)
    {
        this.words = words;
        FillSubCollection();
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
    
    private void FillSubCollection()
    {
        Dictionary<string, List<Word>> mappedWords = new Dictionary<string, List<Word>>();
        
        foreach (Word word in words)
        {
            string initialLetter = word.text.Substring(0, 1);
            if (!mappedWords.ContainsKey(initialLetter))
            {
                mappedWords[initialLetter] = new List<Word>();
            }

            mappedWords[initialLetter].Add(word);
        }
        
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

        Debug.Print("SUCCESS");
    }
}
