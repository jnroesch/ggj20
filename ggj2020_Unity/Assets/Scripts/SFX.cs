using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SFX : MonoBehaviour
{
    public static SFX Instance;

    public AudioSource audioSource;

    public AudioClip StartUp;
    public AudioClip BackgroundMusic;
    public AudioClip TimerBeep;

    [SerializeField]
    private AudioClip[] _randomBeeps;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayOneShot(StartUp);
    }

    public void StartBackgroundMusic()
    {
        audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        audioSource.Stop();
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public AudioClip GetRandomBeep()
    {
        System.Random rnd = new System.Random();
        int rndInt = rnd.Next(_randomBeeps.Length);
        return _randomBeeps[rndInt];
    }
}
