using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class WordLoader : MonoBehaviour
{
    #region Variables

    private string _jsonPath = "";
    private readonly string _jsonFileName = "wordCollection.json";
    
    // Components
    private Dictionary<string, WordCollection> _wordCollectionDict;
    public static WordCollection WordCollection;

    #endregion

    private void Awake()
    {
        _jsonPath = Application.dataPath + "/Resources/";
        if(CreateJsonFile())
            WriteSampleWords();
        LoadWords();
    }

    #region JSon Handling Methods

    private bool CreateJsonFile()
    {
        string path = "Assets/Resources/" + _jsonFileName;

        if (Resources.Load<TextAsset>(_jsonFileName.Replace(".json", "")) != null)
            return false;
        
        string str = "";
        using (FileStream fs = new FileStream(path, FileMode.Create)){
            using (StreamWriter writer = new StreamWriter(fs)){
                writer.Write(str);
            }
        }
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh ();
#endif

        return true;
    }

    [ContextMenu("Load Words")]
    private void LoadWords()
    {
        using (StreamReader stream = new StreamReader(_jsonPath + _jsonFileName))
        {
            string json = stream.ReadToEnd();
            WordCollection = JsonUtility.FromJson<WordCollection>(json);
        }
    }

    [ContextMenu("Write Sample Words")]
    private void WriteSampleWords()
    {
        GenerateSameWords();
        using (StreamWriter stream = new StreamWriter(_jsonPath + _jsonFileName))
        {
            string json = JsonUtility.ToJson(WordCollection);
            
            stream.Write(json);
        }
    }

    private void GenerateSameWords()
    {
        Word[] sampleWords = new Word[2];
        sampleWords[0] = new Word {text = "Inimigo"};
        sampleWords[1] = new Word {text = "Amigo"};

        WordCollection = new WordCollection {words = sampleWords};

    }

    #endregion

    #region Words Handling Methods

    public Word[] GetAllWords()
    {
        return WordCollection.words;
    }

    public static Word GetRandomWord()
    {
        Word[] words = WordCollection.words;
        return words[Random.Range(0, words.Length)];
    }

    #endregion
    
}
