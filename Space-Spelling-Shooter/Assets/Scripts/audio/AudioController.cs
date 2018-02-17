using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void SetAudio(AudioClip audio)
    {
        audioSource.clip = audio;
    }

    public void Play()
    {
        audioSource.volume = GlobalVariables.VOLUME;
        audioSource.Play();
    }

    public float getAudioLength() {
        return audioSource.clip.length;
    }
}
