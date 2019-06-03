using System.IO;
using UnityEngine;


public class WordLoader : MonoBehaviour
{
    #region Variables

    private string _jsonPath = "";
    private readonly string _jsonFileName = "wordCollection.json";
    
    // Components
    public static WordCollection WordCollection;

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
            string json = JsonUtility.ToJson(WordCollection);

            stream.Write(json);
        }
    }

    private void GenerateSampleWords()
    {
        Word[] sampleWords = new Word[3];
        sampleWords[0] = new Word {text = "Inimigo"};
        sampleWords[1] = new Word {text = "Amigo"};
        sampleWords[2] = new Word {text = "Americano"};

        WordCollection = new WordCollection(sampleWords);
    }
    
    [ContextMenu("Load Words")]
    private bool LoadWords()
    {
        using (StreamReader stream = new StreamReader(_jsonPath + _jsonFileName))
        {
            string json = stream.ReadToEnd();
            WordCollection = JsonUtility.FromJson<WordCollection>(json);
        }

        return WordCollection != null;
    }

    #endregion
}
