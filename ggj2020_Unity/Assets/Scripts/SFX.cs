using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SFX : MonoBehaviour
{
    public static SFX Instance;

    public AudioSource audioSource;

    public AudioClip SubmitSound;
    public AudioClip StartUp;
    public AudioClip BackgroundMusic;
    public AudioClip TimerBeep;
    public AudioClip GameboyText;

    public AudioClip[] keyboardSounds;

    [SerializeField]
    private AudioClip[] _randomBeepsWin;

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

    public void Keyboard()
    {
        System.Random rnd = new System.Random();
        int rndInt = rnd.Next(keyboardSounds.Length);
        audioSource.PlayOneShot(keyboardSounds[rndInt]);
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayOneShot(AudioClip audioClip, float volumescale)
    {
        audioSource.PlayOneShot(audioClip, volumescale);
    }

    public AudioClip GetRandomWinBeep()
    {
        System.Random rnd = new System.Random();
        int rndInt = rnd.Next(_randomBeepsWin.Length);
        return _randomBeepsWin[rndInt];
    }
}
