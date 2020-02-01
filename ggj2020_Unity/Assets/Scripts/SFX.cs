using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SFX : MonoBehaviour
{
    public AudioClip BackgroundMusic;

    public AudioClip TimerBeep;

    [SerializeField]
    private AudioClip[] _randomBeeps;

    public AudioClip GetRandomBeep()
    {
        System.Random rnd = new System.Random();
        int rndInt = rnd.Next(_randomBeeps.Length);
        return _randomBeeps[rndInt];
    }
}
