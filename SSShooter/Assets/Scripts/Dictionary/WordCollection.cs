using System;

[Serializable]
public class WordCollection
{
    #region Variables

    // List of Words
    public Word[] words;
    
    #endregion

    public override string ToString()
    {
        string result = "Words";

        foreach (Word word in words)
        {
            result += "Word: " + word.text + "\n";
        }
        
        return result;
    }
}
