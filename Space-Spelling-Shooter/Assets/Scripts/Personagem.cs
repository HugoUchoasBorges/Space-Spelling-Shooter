using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour {

    public Dictionary<GlobalVariables.ENUM_AUDIO, GameObject> audioSources;
    public Dictionary<GlobalVariables.ENUM_AUDIO, AudioController> audioControllers;

    // Use this for initialization
    protected virtual void Start () {
        
    }

    protected virtual void inicializaAudios(Dictionary<GlobalVariables.ENUM_AUDIO, AudioClip> dict)
    {
        audioSources = new Dictionary<GlobalVariables.ENUM_AUDIO, GameObject>();
        audioControllers = new Dictionary<GlobalVariables.ENUM_AUDIO, AudioController>();

        foreach (GlobalVariables.ENUM_AUDIO e in dict.Keys)
        {
            audioSources.Add(e, Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.audioSource]));
            audioSources[e].transform.SetParent(transform);

            audioControllers.Add(e, audioSources[e].GetComponent<AudioController>());
            audioControllers[e].SetAudio(dict[e]);
        }
    }

    public virtual void PlayAudio(GlobalVariables.ENUM_AUDIO audio)
    {
        audioControllers[audio].Play();
    }
}
