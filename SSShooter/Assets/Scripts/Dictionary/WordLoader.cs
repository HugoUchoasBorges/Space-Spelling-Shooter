using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class WordLoader : MonoBehaviour
{
    #region Variables

    private string _jsonPath = "";
    private readonly string _jsonFileName = "wordCollection.json";
    
    // Components
    private static WordCollection _wordCollection;

    #endregion

    private void Awake()
    {
        _jsonPath = Application.dataPath + "/Resources/";

        CreateJsonFile("Assets/Resources/", _jsonFileName);

        if (!LoadWords())
            WriteSampleWords();

    }

    #region JSon Handling Methods

    private void CreateJsonFile(string path, string filename)
    {
        if (Resources.Load<TextAsset>(filename.Replace(".json", "")) != null)
            return;
        
        const string str = "";
        using (FileStream fs = new FileStream(path + filename, FileMode.Create)){
            using (StreamWriter writer = new StreamWriter(fs)){
                writer.Write(str);
            }
        }
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh ();
#endif
    }

    [ContextMenu("Write Sample Words")]
    private void WriteSampleWords()
    {
        GenerateSampleWords();
        using (StreamWriter stream = new StreamWriter(_jsonPath + _jsonFileName))
        {
            string json = JsonUtility.ToJson(_wordCollection);

            stream.Write(json);
        }
    }

    private void GenerateSampleWords()
    {
        Word[] sampleWords = new Word[3];
        sampleWords[0] = new Word {text = "Inimigo"};
        sampleWords[1] = new Word {text = "Amigo"};
        sampleWords[2] = new Word {text = "Americano"};

        _wordCollection = new WordCollection(sampleWords);
    }
    
    [ContextMenu("Load Words")]
    private bool LoadWords()
    {
        using (StreamReader stream = new StreamReader(_jsonPath + _jsonFileName))
        {
            string json = stream.ReadToEnd();
            _wordCollection = JsonUtility.FromJson<WordCollection>(json);
        }

        return _wordCollection != null;
    }

    #endregion

    #region Words Handling Methods

    public Word[] GetAllWords()
    {
        return _wordCollection.words;
    }

    public static Word GetRandomWord()
    {
        Word[] words = _wordCollection.words;
        return words[Random.Range(0, words.Length)];
    }

    #endregion
    
}
