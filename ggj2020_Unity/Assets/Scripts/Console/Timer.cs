﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game.Console
{
    public class Timer : MonoBehaviour
    {
        private int timeLeft = 20;
        private Console console;

        private void Awake()
        {
            console = FindObjectOfType<Console>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        public void StartTimer()
        {
            StartCoroutine(TimerRoutine());
        }

        private IEnumerator TimerRoutine()
        {
            while (timeLeft > 0)
            {
                console.Log(timeLeft.ToString());
                yield return new WaitForSeconds(1);
                timeLeft--;
            }

        }
    }

}