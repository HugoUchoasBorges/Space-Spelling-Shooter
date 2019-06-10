using System.IO;
using UnityEngine;


public class WordLoader : MonoBehaviour
{
    #region Variables

//    private string _jsonPath = "";
    private const string JsonFileName = "wordCollection";

    // Components
    public WordCollection wordCollection;
    
    [HideInInspector] public TextAsset textAsset;

    #endregion

    private void Awake()
    {
//        _jsonPath = Application.dataPath + "/Resources/";
//
//        CreateJsonFile("Assets/Resources/", JsonFileName);

        if (!LoadWords())
            WriteToJsonFile();

    }

    #region JSon Handling Methods

    private void CreateJsonFile(string path, string filename, bool force=false)
    {
        if (force || (Resources.Load<TextAsset>(filename.Replace(".json", "")) != null))
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
    private void WriteToJsonFile(bool sample=true)
    {
        if(sample)
            GenerateSampleWords();

        string json = JsonUtility.ToJson(wordCollection);

        textAsset = new TextAsset(json);
        
//        using (StreamWriter stream = new StreamWriter(_jsonPath + JsonFileName))
//        {
//            string json = JsonUtility.ToJson(wordCollection);
//
//            stream.Write(json);
//        }
    }

    private void GenerateSampleWords()
    {
        Word[] sampleWords = new Word[3];
        sampleWords[0] = new Word {text = "Inimigo"};
        sampleWords[1] = new Word {text = "Amigo"};
        sampleWords[2] = new Word {text = "Americano"};

        wordCollection = new WordCollection(sampleWords);
    }
    
    [ContextMenu("Load Words")]
    private bool LoadWords()
    {
        textAsset = Resources.Load<TextAsset>(JsonFileName);
        wordCollection = JsonUtility.FromJson<WordCollection>(textAsset.text);
            
//        using (StreamReader stream = new StreamReader(_jsonPath + JsonFileName))
//        {
//            string json = stream.ReadToEnd();
//            wordCollection = JsonUtility.FromJson<WordCollection>(json);
//        }

        if (wordCollection == null)
            return false;

        int subCollectionWordCount = 0;
        foreach (WordCollection.SubCollection subCollection in wordCollection.subCollections)
        {
            subCollectionWordCount += subCollection.words.Count;
        }

        if (subCollectionWordCount != wordCollection.words.Length)
        {
            wordCollection.FillSubCollections();
            WriteToJsonFile(false);
        }
        return true;
    }

    #endregion
}
