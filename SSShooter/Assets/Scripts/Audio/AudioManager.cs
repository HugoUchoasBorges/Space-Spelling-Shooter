using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioManager : MonoBehaviour
{
    #region Variables
    
    private AudioSource _audioSource;
    public List<Audio> audioList;

    #endregion
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!_audioSource)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }
        
        Assert.IsNotNull(_audioSource, "The GameObject must have an AudioSource component.");
    }

    public float Play(string audioName)
    {
        if (!_audioSource)
            return 0f;

        foreach (Audio audioInstance in audioList)
        {
            if (audioInstance.name == audioName)
            {
                if(!audioInstance.audioClip)
                    Debug.Log("Audio: " + audioName + " FOUND in GameObject " + gameObject.name + 
                              ". But there isn't any audioClip attached to it.");
                else
                {
                    _audioSource.clip = audioInstance.audioClip;
                    _audioSource.Play();
                    return _audioSource.clip.length;
                }
            }
        }
        Debug.Log("Audio: " + audioName + " not found in GameObject " + gameObject.name);
        return 0f;
    }
}

[Serializable]
public class Audio
{
    public string name;
    public AudioClip audioClip;
}
